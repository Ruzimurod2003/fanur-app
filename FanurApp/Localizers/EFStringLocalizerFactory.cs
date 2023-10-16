using FanurApp.Data;
using FanurApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace FanurApp.Localizers;

public class EFStringLocalizerFactory : IStringLocalizerFactory
{
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
        ApplicationContext _db = new ApplicationContext();
        
        // инициализация базы данных
        if (!_db.Cultures.Any())
        {
            _db.AddRange(
                new Culture
                {
                    Name = "en",
                    Resources = new List<Resource>() 
                    {
                        new Resource 
                        {
                            Key = "the_login_page", 
                            Value = "The Login page", 
                            CreatedDate = DateTime.Now, 
                            UpdatedDate = DateTime.Now
                        },
                        new Resource 
                        {
                            Key = "enter_your_email", 
                            Value = "Enter your email...", 
                            CreatedDate = DateTime.Now, 
                            UpdatedDate = DateTime.Now
                        },
                        new Resource 
                        {
                            Key = "email_is_not_like_that", 
                            Value = "Email is not like that 🥱", 
                            CreatedDate = DateTime.Now, 
                            UpdatedDate = DateTime.Now
                        },
                        new Resource 
                        {
                            Key = "enter_your_password", 
                            Value = "Enter your password...", 
                            CreatedDate = DateTime.Now,
                            UpdatedDate = DateTime.Now
                        },
                        new Resource 
                        {
                            Key = "have_you_forgotten_your_password",
                            Value = "Have you forgotten your password 🙄",
                            CreatedDate = DateTime.Now,
                            UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                            Key = "password_is_not_like_that",
                            Value = "Password is not like that 🥱",
                            CreatedDate = DateTime.Now,
                            UpdatedDate = DateTime.Now
                        },
                        new Resource 
                        {
                            Key = "do_you_remember", 
                            Value = "Remember me?",
                            CreatedDate = DateTime.Now,
                            UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                            Key = "login_button", 
                            Value = "Login", 
                            CreatedDate = DateTime.Now,
                            UpdatedDate = DateTime.Now
                        },
                        new Resource 
                        {
                            Key = "is_there_an_account",
                            Value = "Is ther an account 😏",
                            CreatedDate = DateTime.Now,
                            UpdatedDate = DateTime.Now
                        },
                        new Resource 
                        {
                            Key = "to_the_account_registration_page",
                            Value = "To the registration page",
                            CreatedDate = DateTime.Now,
                            UpdatedDate = DateTime.Now
                        },
                        new Resource 
                        {
                            Key = "this_user_is_already_registered",
                            Value = "This user is already registered", 
                            CreatedDate = DateTime.Now,
                            UpdatedDate = DateTime.Now
                        },
                        new Resource 
                        {
                            Key = "you_entered_the_wrong_password_or_email", 
                            Value = "You entered the wrong password or email",
                            CreatedDate = DateTime.Now,
                            UpdatedDate = DateTime.Now
                        },
                        new Resource 
                        {
                            Key = "course_settings", 
                            Value = "COURSE SETTINGS",
                            CreatedDate = DateTime.Now,
                            UpdatedDate = DateTime.Now
                        },
                        new Resource 
                        {
                            Key = "courses", 
                            Value = "Courses",
                            CreatedDate = DateTime.Now,
                            UpdatedDate = DateTime.Now
                        },
                        new Resource 
                        {
                            Key = "topics", 
                            Value = "Topics",
                            CreatedDate = DateTime.Now,
                            UpdatedDate = DateTime.Now
                        },
                        new Resource 
                        {
                            Key = "videos", 
                            Value = "Videos",
                            CreatedDate = DateTime.Now,
                            UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                            Key = "descriptions",
                            Value = "Descriptions",
                            CreatedDate = DateTime.Now,
                            UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                            Key = "roles",
                            Value = "Roles",
                            CreatedDate = DateTime.Now,
                            UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                            Key = "user_settings",
                            Value = "USER SETTINGS",
                            CreatedDate = DateTime.Now,
                            UpdatedDate = DateTime.Now
                        }
                    }
                },
                new Culture
                {
                    Name = "ru",
                    Resources = new List<Resource>() 
                    {
                        new Resource 
                        {
                            Key = "the_login_page", 
                            Value = "Страница входа",
                            CreatedDate = DateTime.Now,
                            UpdatedDate = DateTime.Now
                        },
                        new Resource 
                        {
                            Key = "enter_your_email",
                            Value = "Введите адрес электронной почты...", 
                            CreatedDate = DateTime.Now,
                            UpdatedDate = DateTime.Now
                        },
                        new Resource 
                        {
                            Key = "email_is_not_like_that",
                            Value = "Электронная почта не такая 🥱",
                            CreatedDate = DateTime.Now,
                            UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                            Key = "enter_your_password",
                            Value = "Введите ваш пароль...",
                            CreatedDate = DateTime.Now,
                            UpdatedDate = DateTime.Now
                        },
                        new Resource 
                        {
                            Key = "have_you_forgotten_your_password",
                            Value = "Вы забыли свой пароль 🙄",
                            CreatedDate = DateTime.Now, 
                            UpdatedDate = DateTime.Now
                        },
                        new Resource 
                        {
                            Key = "password_is_not_like_that", 
                            Value = "Пароль не такой 🥱", 
                            CreatedDate = DateTime.Now,
                            UpdatedDate = DateTime.Now
                        },
                        new Resource 
                        {
                            Key = "do_you_remember",
                            Value = "Запомнить меня?",
                            CreatedDate = DateTime.Now,
                            UpdatedDate = DateTime.Now
                        },
                        new Resource 
                        {
                            Key = "login_button", 
                            Value = "Авторизоваться", 
                            CreatedDate = DateTime.Now, 
                            UpdatedDate = DateTime.Now
                        },
                        new Resource 
                        {
                            Key = "is_there_an_account", 
                            Value = "Есть аккаунт 😏",
                            CreatedDate = DateTime.Now, 
                            UpdatedDate = DateTime.Now
                        },
                        new Resource 
                        {
                            Key = "to_the_account_registration_page",
                            Value = "На страницу регистрации",
                            CreatedDate = DateTime.Now,
                            UpdatedDate = DateTime.Now
                        },
                        new Resource 
                        {
                            Key = "this_user_is_already_registered",
                            Value = "Этот пользователь уже зарегистрирован", 
                            CreatedDate = DateTime.Now,
                            UpdatedDate = DateTime.Now
                        },
                        new Resource 
                        {
                            Key = "you_entered_the_wrong_password_or_email", 
                            Value = "Вы ввели неверный пароль или адрес электронной почты",
                            CreatedDate = DateTime.Now,
                            UpdatedDate = DateTime.Now
                        },
                        new Resource 
                        {
                            Key = "course_settings", 
                            Value = "НАСТРОЙКИ КУРСА",
                            CreatedDate = DateTime.Now,
                            UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                            Key = "courses",
                            Value = "Курсы",
                            CreatedDate = DateTime.Now,
                            UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                            Key = "topics",
                            Value = "Темы",
                            CreatedDate = DateTime.Now,
                            UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                            Key = "videos",
                            Value = "Ролики",
                            CreatedDate = DateTime.Now,
                            UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                            Key = "descriptions",
                            Value = "Детали",
                            CreatedDate = DateTime.Now,
                            UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                            Key = "roles",
                            Value = "Роли",
                            CreatedDate = DateTime.Now,
                            UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                            Key = "user_settings",
                            Value = "ПОЛЬЗОВАТЕЛЬСКИЕ НАСТРОЙКИ",
                            CreatedDate = DateTime.Now,
                            UpdatedDate = DateTime.Now
                        }
                    }
                },
                new Culture
                {
                    Name = "uz",
                    Resources = new List<Resource>() 
                    {
                        new Resource 
                        {
                            Key = "the_login_page", 
                            Value = "Kirish sahifasi",
                            CreatedDate = DateTime.Now,
                            UpdatedDate = DateTime.Now
                        },
                        new Resource 
                        {
                            Key = "enter_your_email", 
                            Value = "Elektron pochtangizni kiriting...",
                            CreatedDate = DateTime.Now, 
                            UpdatedDate = DateTime.Now
                        },
                        new Resource 
                        {
                            Key = "email_is_not_like_that",
                            Value = "Elektron pochta bunday emas 🥱",
                            CreatedDate = DateTime.Now, 
                            UpdatedDate = DateTime.Now
                        },
                        new Resource 
                        {
                            Key = "enter_your_password", 
                            Value = "Parolingizni kiriting...",
                            CreatedDate = DateTime.Now,
                            UpdatedDate = DateTime.Now
                        },
                        new Resource 
                        {
                            Key = "have_you_forgotten_your_password",
                            Value = "Parolni unutdingizmi 🙄", 
                            CreatedDate = DateTime.Now,
                            UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                            Key = "password_is_not_like_that", 
                            Value = "Parol bunday emas 🥱", 
                            CreatedDate = DateTime.Now, 
                            UpdatedDate = DateTime.Now
                        },
                        new Resource 
                        {
                            Key = "do_you_remember",
                            Value = "Meni eslaysizmi?", 
                            CreatedDate = DateTime.Now, 
                            UpdatedDate = DateTime.Now
                        },
                        new Resource 
                        {
                            Key = "login_button", 
                            Value = "Tizimga kirish",
                            CreatedDate = DateTime.Now,
                            UpdatedDate = DateTime.Now
                        },
                        new Resource 
                        {
                            Key = "is_there_an_account", 
                            Value = "Hisob bormi 😏", 
                            CreatedDate = DateTime.Now, 
                            UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                            Key = "to_the_account_registration_page",
                            Value = "Ro'yxatdan o'tish sahifasiga ",
                            CreatedDate = DateTime.Now, 
                            UpdatedDate = DateTime.Now
                        },
                        new Resource 
                        {
                            Key = "this_user_is_already_registered",
                            Value = "Bu foydalanuvchi allaqachon roʻyxatdan oʻtgan",
                            CreatedDate = DateTime.Now, 
                            UpdatedDate = DateTime.Now
                        },
                        new Resource 
                        {
                            Key = "you_entered_the_wrong_password_or_email", 
                            Value = "Siz noto'g'ri parol yoki elektron pochta manzilini kiritdingiz", 
                            CreatedDate = DateTime.Now, 
                            UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                            Key = "course_settings",
                            Value = "KURSLARNING SOZLAMALARI",
                            CreatedDate = DateTime.Now,
                            UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                            Key = "courses",
                            Value = "Kurslar",
                            CreatedDate = DateTime.Now,
                            UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                            Key = "topics",
                            Value = "Mavzular",
                            CreatedDate = DateTime.Now,
                            UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                            Key = "videos",
                            Value = "Videolar",
                            CreatedDate = DateTime.Now,
                            UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                            Key = "descriptions",
                            Value = "Tafsilotlar",
                            CreatedDate = DateTime.Now,
                            UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                            Key = "roles",
                            Value = "Rollar",
                            CreatedDate = DateTime.Now,
                            UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                            Key = "user_settings",
                            Value = "FOYDALANUVCHI SOZLAMALARI",
                            CreatedDate = DateTime.Now,
                            UpdatedDate = DateTime.Now
                        }
                    }
                }
            );
            _db.SaveChanges();
        }
        return new EFStringLocalizer(_db);
    }
}