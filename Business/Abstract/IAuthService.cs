using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Entities.DTOs.AuthDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    interface IAuthService
    {
        Task<IResult> RegisterAsync(RegisterDTO model);
        Task<IDataResult<Token>> LoginAsync(LoginDTO loginDTO);
        Task<IDataResult<string>> UpdateRefreshTokenAsync(string refreshToken, AppUser appUser);
        Task<IDataResult<Token>> RefreshTokenLoginAsync(string refreshToken);
        Task<IResult> Logout(string userId);
        Task<IResult> AssignRoleToUserAsync(string userId, string roleName);
        Task<IResult> UserEmailConfirmed(string email, string otp);
    }
}
