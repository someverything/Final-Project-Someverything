using Entities.DTOs.AuthDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validations.FluentValidation
{
    public class RegisterValidation : AbstractValidator<RegisterDTO>
    {
        public RegisterValidation()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage(GetTranslation("FirstnameIsRequired"))
                .Must(NonDigit).WithMessage(GetTranslation("FirstnameNonDigit"))
                .WithName("FirstName");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage(GetTranslation("LastnameIsRequired"))
                .Must(NonDigit).WithMessage(GetTranslation("LastnameNonDigit"))
                .WithName("LastName");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(GetTranslation("PasswordIsRequired"))
                .MinimumLength(8).WithMessage(GetTranslation("PasswordMinLength"))
                .Matches(@"[A-Z]").WithMessage(GetTranslation("PasswordUppercase"))
                .Matches(@"[a-z]").WithMessage(GetTranslation("PasswordLowercase"))
                .Matches(@"[0-9]").WithMessage(GetTranslation("PasswordDigit"))
                .Matches(@"[\W]").WithMessage(GetTranslation("PasswordSpecialCharacter"))
                .WithName("Password");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(GetTranslation("EmailIsRequired"))
                .EmailAddress().WithMessage(GetTranslation("EmailInvalid"))
                .WithName("Email");

            RuleFor(x => x.Username)
                .NotEmpty().WithMessage(GetTranslation("UsernameIsRequired"))
                .MinimumLength(3).WithMessage(GetTranslation("UsernameMinLength"))
                .MaximumLength(50).WithMessage(GetTranslation("UsernameMaxLength"))
                .WithName("Username");
        }

        private bool NonDigit(string value)
        {
            return !value.Any(char.IsDigit);
        }

        private string GetTranslation(string key)
        {
            return ValidatorOptions.Global.LanguageManager.GetString(key, new CultureInfo(Thread.CurrentThread.CurrentUICulture.Name));
        }
    }
}
