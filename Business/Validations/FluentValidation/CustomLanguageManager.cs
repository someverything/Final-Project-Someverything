using FluentValidation.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validations.FluentValidation
{
    public class CustomLanguageManager : LanguageManager
    {
        public CustomLanguageManager()
        {
            //Firstname

            AddTranslation("az", "FirstnameIsRequired", "Ad boş ola bilməz!");
            AddTranslation("ru-RU", "FirstnameIsRequired", "Имя не может быть пустым!");
            AddTranslation("en-US", "FirstnameIsRequired", "First name can't be empty!");
            AddTranslation("az", "FirstnameNonDigit", "Ad reqem dashiya bilmez!");
            AddTranslation("en-US", "FirstnameNonDigit", "Name cant contain digits!");
            AddTranslation("ru-RU", "FirstnameNonDigit", "В имени не могут присутсвовать числа!");


            //Lastname
            AddTranslation("az", "LastnameNonDigit", "Soyad reqem dashiya bilmez!");
            AddTranslation("az", "LastnameIsRequired", "Soyad boş ola bilməz!");
            AddTranslation("ru-RU", "LastnameIsRequired", "Фамилия не может быть пустым!");
            AddTranslation("en-US", "LastnameIsRequired", "Lastname can't be empty!");
            AddTranslation("ru-RU", "LastnameNonDigit", "В фамилии не могут присутсвовать числа!");
            AddTranslation("en-US", "LastnameNonDigit", "Lastname cant contain digits!");


            //Password
            AddTranslation("az", "PasswordIsRequired", "Şifrə tələb olunur");
            AddTranslation("ru-RU", "PasswordIsRequired", "Пароль обязателен");
            AddTranslation("en-US", "PasswordIsRequired", "Password is required");

            AddTranslation("az", "PasswordMinLength", "Şifrə ən azı 8 simvoldan ibarət olmalıdır");
            AddTranslation("ru-RU", "PasswordMinLength", "Пароль должен быть не менее 8 символов");
            AddTranslation("en-US", "PasswordMinLength", "Password must be at least 8 characters long");

            AddTranslation("az", "PasswordUppercase", "Şifrə ən azı bir böyük hərf içerməlidir");
            AddTranslation("ru-RU", "PasswordUppercase", "Пароль должен содержать хотя бы одну заглавную букву");
            AddTranslation("en-US", "PasswordUppercase", "Password must contain at least one uppercase letter");

            AddTranslation("az", "PasswordLowercase", "Şifrə ən azı bir kiçik hərf içerməlidir");
            AddTranslation("ru-RU", "PasswordLowercase", "Пароль должен содержать хотя бы одну строчную букву");
            AddTranslation("en-US", "PasswordLowercase", "Password must contain at least one lowercase letter");

            AddTranslation("az", "PasswordDigit", "Şifrə ən azı bir rəqəm içerməlidir");
            AddTranslation("ru-RU", "PasswordDigit", "Пароль должен содержать хотя бы одну цифру");
            AddTranslation("en-US", "PasswordDigit", "Password must contain at least one digit");

            AddTranslation("az", "PasswordSpecialCharacter", "Şifrə ən azı bir xüsusi simvol içerməlidir");
            AddTranslation("ru-RU", "PasswordSpecialCharacter", "Пароль должен содержать хотя бы один специальный символ");
            AddTranslation("en-US", "PasswordSpecialCharacter", "Password must contain at least one special character");

            //Email
            AddTranslation("az", "EmailIsRequired", "E-poçt tələb olunur");
            AddTranslation("ru-RU", "EmailIsRequired", "Электронная почта обязательна");
            AddTranslation("en-US", "EmailIsRequired", "Email is required");

            AddTranslation("az", "EmailInvalid", "Keçərli bir e-poçt ünvanı daxil edin");
            AddTranslation("ru-RU", "EmailInvalid", "Введите действительный адрес электронной почты");
            AddTranslation("en-US", "EmailInvalid", "Please enter a valid email address");

            //Username
            AddTranslation("az", "UsernameIsRequired", "İstifadəçi adı tələb olunur");
            AddTranslation("ru-RU", "UsernameIsRequired", "Имя пользователя обязательно");
            AddTranslation("en-US", "UsernameIsRequired", "Username is required");

            AddTranslation("az", "UsernameMinLength", "İstifadəçi adı ən azı 3 simvoldan ibarət olmalıdır");
            AddTranslation("ru-RU", "UsernameMinLength", "Имя пользователя должно содержать не менее 3 символов");
            AddTranslation("en-US", "UsernameMinLength", "Username must be at least 3 characters long");

            AddTranslation("az", "UsernameMaxLength", "İstifadəçi adı ən çox 50 simvoldan ibarət olmalıdır");
            AddTranslation("ru-RU", "UsernameMaxLength", "Имя пользователя должно содержать не более 50 символов");
            AddTranslation("en-US", "UsernameMaxLength", "Username must be at most 50 characters long");
        }
    }
}
