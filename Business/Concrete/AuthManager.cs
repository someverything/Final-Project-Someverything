﻿using Business.Abstract;
using Business.Messages;
using Business.Validations.FluentValidation;
using Core.Entities.Concrete;
using Core.Utilities.Messages.Abstract;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.ErrorResults;
using Core.Utilities.Results.Concrete.SuccessResults;
using Core.Utilities.Security.Abstract;
using Entities.Common;
using Entities.DTOs.AuthDTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMessageService _messageService;
        private readonly ILogger<AuthManager> _logger;

        public AuthManager(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService, IMessageService messageService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _messageService = messageService;
        }

        public async Task<IResult> AssignRoleToUserAsync(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            await _userManager.AddToRoleAsync(user, roleName);
            return new SuccessResult(System.Net.HttpStatusCode.OK);
        }

        public string GenerateOtp()
        {
            byte[] data = new byte[4];
            using var rng = System.Security.Cryptography.RandomNumberGenerator.Create();
            rng.GetBytes(data);
            int value = BitConverter.ToInt32(data, 0);
            return Math.Abs(value % 900000).ToString("D6");
        }

        public async Task<IDataResult<Token>> LoginAsync(LoginDTO loginDTO)
        {
            var findUser = await _userManager.FindByEmailAsync(loginDTO.UsernameOrEmail);
            
            if (findUser == null)
                findUser = await _userManager.FindByNameAsync(loginDTO.UsernameOrEmail);

            if(findUser == null)
                return new ErrorDataResults<Token>(message: "User does not exist!", System.Net.HttpStatusCode.NotFound);

            if(findUser.EmailConfirmed == false)
                return new ErrorDataResults<Token>(message: "User not confirmed!", System.Net.HttpStatusCode.BadRequest);

            var result = await _signInManager.CheckPasswordSignInAsync(findUser, loginDTO.Password, false);
            var userRole = await _userManager.GetRolesAsync(findUser);

            if (result.Succeeded)
            {
                Token token = await _tokenService.CreateAccessTokenAsync(findUser, userRole.ToList());
                var response = await UpdateRefreshTokenAsync(token.RefreshToken, findUser);
                return new SuccessDataResult<Token>(data: token, statusCode: System.Net.HttpStatusCode.OK, message: response.Message);
            }
            else
            {
                _logger.LogError("Username or password is incorrect!");
                return new ErrorDataResults<Token>("Username or Password is incorrect!", System.Net.HttpStatusCode.BadRequest);
            }


        }

        public async Task<IResult> Logout(string userId)
        {
            var user = await _userManager.FindByNameAsync(userId);
            if(user is not null)
            {
                user.RefreshToken = null;
                user.RefreshTokenExpiredDate = null;
                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded) return new SuccessResult(statusCode: System.Net.HttpStatusCode.OK);
                
                else
                {
                    string response = string.Empty;
                    foreach (var error in result.Errors)
                    {
                        response += error.Description + ". ";
                    }
                    _logger.LogError(response);
                    return new ErrorResult(response, false, HttpStatusCode.BadRequest);
                }
            }
            _logger.LogError(AuthMessages.UserNotFound);
            return new ErrorResult(message: AuthMessages.UserNotFound, false, System.Net.HttpStatusCode.NotFound);
        }

        public async Task<IDataResult<Token>> RefreshTokenLoginAsync(string refreshToken)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.RefreshToken == refreshToken);
            var userRoles = await _userManager.GetRolesAsync(user);
            if(user != null && user.RefreshTokenExpiredDate > DateTime.Now)
            {
                Token token = await _tokenService.CreateAccessTokenAsync(user, userRoles.ToList());
                token.RefreshToken = refreshToken;
                return new SuccessDataResult<Token>(data: token, statusCode: System.Net.HttpStatusCode.OK);
            }
            else return new ErrorDataResults<Token>(statusCode: System.Net.HttpStatusCode.BadRequest, message: "Relogin");
        }

        public async Task<IResult> RegisterAsync(RegisterDTO model)
        {
            var validator = new RegisterValidation();
            var validatorResult = validator.Validate(model);
            if (!validatorResult.IsValid)
            {
                _logger.LogError(validatorResult.ToString());
                return new ErrorResult(message: validatorResult.ToString(), success: false, statusCode: HttpStatusCode.BadRequest);
            }
            User newUser = new()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Username
            };
            var result = await _userManager.CreateAsync(newUser, model.Password);
            if (result.Succeeded) return new SuccessResult(System.Net.HttpStatusCode.OK);
            else
            {
                string response = string.Empty;
                foreach (var error in result.Errors)
                {
                    response += error.Description + ". ";
                }
                _logger.LogError(response);
                return new ErrorResult(response, false, System.Net.HttpStatusCode.BadRequest);
            }

        }

        public async Task<IDataResult<string>> UpdateRefreshTokenAsync(string refreshToken, AppUser appUser)
        {
            if(appUser is not null)
            {
                appUser.RefreshToken = refreshToken;
                appUser.RefreshTokenExpiredDate = DateTime.UtcNow.AddMonths(1);
                var res = await _userManager.UpdateAsync(appUser);
                if (res.Succeeded) return new SuccessDataResult<string>(data: refreshToken ,System.Net.HttpStatusCode.OK);
                else
                {
                    string response = string.Empty;
                    foreach (var error in res.Errors)
                    {
                        response += error.Description + ". ";
                    }
                    _logger.LogError(response);
                    return new ErrorDataResults<string>(message: response, HttpStatusCode.BadRequest);
                }
            }
            else
            {
                _logger.LogError(HttpStatusCode.NotFound.ToString());
                return new ErrorDataResults<string>(HttpStatusCode.NotFound);
            }
        }

        public async Task<IResult> UserEmailConfirmed(string email, string otp)
        {
            var findUser = _userManager.Users.OfType<User>().FirstOrDefault(x => x.Email == email);
            if(findUser.OTP == otp && findUser.ExpiredDate > DateTime.Now)
            {
                findUser.EmailConfirmed = true;
                await _userManager.UpdateAsync(findUser);
                return new SuccessResult(HttpStatusCode.OK);
            }
            return new ErrorResult(HttpStatusCode.BadRequest);
        }
    }
}
