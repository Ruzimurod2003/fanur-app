using FanurApp.Data;
using FanurApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace FanurApp.Configurations;

public class EFStringLocalizerFactory : IStringLocalizerFactory
{
    string _connectionString;
    public EFStringLocalizerFactory(string connection)
    {
        _connectionString = connection;
    }

    public IStringLocalizer Create(Type resourceSource)
    {
        return CreateStringLocalizer();
    }

    public IStringLocalizer Create(string baseName, string location)
    {
        return CreateStringLocalizer();
    }

    private IStringLocalizer CreateStringLocalizer()
    {
        ApplicationContext _db = new ApplicationContext(
            new DbContextOptionsBuilder<ApplicationContext>()
                .UseSqlite(_connectionString)
                .Options);
        // инициализация базы данных
        if (!_db.Cultures.Any())
        {
            _db.AddRange(
                new Culture
                {
                    Name = "en",
                    Resources = new List<Resource>()
                    {
                        new Resource { Key = "the_login_page", Value = "The Login page" },
                        new Resource { Key = "enter_your_email", Value = "Enter your email..." },
                        new Resource { Key = "email_is_not_like_that", Value = "Email is not like that 🥱" },
                        new Resource { Key = "enter_your_password", Value = "Enter your password..." },
                        new Resource { Key = "have_you_forgotten_your_password", Value = "Have you forgotten your password 🙄" },
                        new Resource { Key = "password_is_not_like_that", Value = "Password is not like that 🥱" },
                        new Resource { Key = "do_you_remember", Value = "Remember me?" },
                        new Resource { Key = "login_button", Value = "Login" },
                        new Resource { Key = "is_there_an_account", Value = "Is ther an account 😏" },
                        new Resource { Key = "to_the_account_registration_page", Value = "To the registration page" },
                        new Resource { Key = "this_user_is_already_registered", Value = "This user is already registered" },
                        new Resource { Key = "you_entered_the_wrong_password_or_email", Value = "You entered the wrong password or email" }
                    }
                },
                new Culture
                {
                    Name = "ru",
                    Resources = new List<Resource>()
                    {
                        new Resource { Key = "the_login_page", Value = "Страница входа" },
                        new Resource { Key = "enter_your_email", Value = "Введите адрес электронной почты..." },
                        new Resource { Key = "email_is_not_like_that", Value = "Электронная почта не такая 🥱" },
                        new Resource { Key = "enter_your_password", Value = "Введите ваш пароль..." },
                        new Resource { Key = "have_you_forgotten_your_password", Value = "Вы забыли свой пароль 🙄" },
                        new Resource { Key = "password_is_not_like_that", Value = "Пароль не такой 🥱" },
                        new Resource { Key = "do_you_remember", Value = "Запомнить меня?" },
                        new Resource { Key = "login_button", Value = "Авторизоваться" },
                        new Resource { Key = "is_there_an_account", Value = "Есть аккаунт 😏" },
                        new Resource { Key = "to_the_account_registration_page", Value = "На страницу регистрации" },
                        new Resource { Key = "this_user_is_already_registered", Value = "Этот пользователь уже зарегистрирован" },
                        new Resource { Key = "you_entered_the_wrong_password_or_email", Value = "Вы ввели неверный пароль или адрес электронной почты" }
                    }
                },
                new Culture
                {
                    Name = "uz",
                    Resources = new List<Resource>()
                    {
                        new Resource { Key = "the_login_page", Value = "Kirish sahifasi" },
                        new Resource { Key = "enter_your_email", Value = "Elektron pochtangizni kiriting..."},
                        new Resource { Key = "email_is_not_like_that", Value = "Elektron pochta bunday emas 🥱" },
                        new Resource { Key = "enter_your_password", Value = "Parolingizni kiriting..."},
                        new Resource { Key = "have_you_forgotten_your_password", Value = "Parolni unutdingizmi 🙄" },
                        new Resource { Key = "password_is_not_like_that", Value = "Parol bunday emas 🥱" },
                        new Resource { Key = "do_you_remember", Value = "Meni eslaysizmi?" },
                        new Resource { Key = "login_button", Value = "Tizimga kirish" },
                        new Resource { Key = "is_there_an_account", Value = "Hisob bormi 😏" },
                        new Resource { Key = "to_the_account_registration_page", Value = "Ro'yxatdan o'tish sahifasiga "},
                        new Resource { Key = "this_user_is_already_registered", Value = "Bu foydalanuvchi allaqachon roʻyxatdan oʻtgan" },
                        new Resource { Key = "you_entered_the_wrong_password_or_email", Value = "Siz noto'g'ri parol yoki elektron pochta manzilini kiritdingiz" }
                    }
                }
            );
            _db.SaveChanges();
        }
        return new EFStringLocalizer(_db);
    }
}