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
                           Key = "resources_deleted_unsuccessfully",
                           Value = "Resources deleted unsuccessfully",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "resources_updated_unsuccessfully",
                           Value = "Resources updated unsuccessfully",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "resources_created_unsuccessfully",
                           Value = "Resources created unsuccessfully",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "resources_deleted_successfully",
                           Value = "Resources deleted successfully",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "resources_updated_successfully",
                           Value = "Resources updated successfully",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "resources_created_successfully",
                           Value = "Resources created successfully",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "definitions_deleted_unsuccessfully",
                           Value = "Definitions deleted unsuccessfully",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "definitions_updated_unsuccessfully",
                           Value = "Definitions updated unsuccessfully",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "definitions_created_unsuccessfully",
                           Value = "Definitions created unsuccessfully",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "definitions_deleted_successfully",
                           Value = "Definitions deleted successfully",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "definitions_updated_successfully",
                           Value = "Definitions updated successfully",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "definitions_created_successfully",
                           Value = "Definitions created successfully",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "roles_deleted_unsuccessfully",
                           Value = "Roles deleted unsuccessfully",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "roles_updated_unsuccessfully",
                           Value = "Roles updated unsuccessfully",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "roles_created_unsuccessfully",
                           Value = "Roles created unsuccessfully",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "roles_deleted_successfully",
                           Value = "Roles deleted successfully",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "roles_updated_successfully",
                           Value = "Roles updated successfully",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "roles_created_successfully",
                           Value = "Roles created successfully",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "videos_deleted_unsuccessfully",
                           Value = "Videos deleted unsuccessfully",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "videos_updated_unsuccessfully",
                           Value = "Videos updated unsuccessfully",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "videos_created_unsuccessfully",
                           Value = "Videos created unsuccessfully",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "videos_deleted_successfully",
                           Value = "Videos deleted successfully",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "videos_updated_successfully",
                           Value = "Videos updated successfully",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "videos_created_successfully",
                           Value = "Videos created successfully",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "topics_deleted_unsuccessfully",
                           Value = "Topics deleted unsuccessfully",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "topics_updated_unsuccessfully",
                           Value = "Topics updated unsuccessfully",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "topics_created_unsuccessfully",
                           Value = "Topics created unsuccessfully",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "topics_deleted_successfully",
                           Value = "Topics deleted successfully",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "topics_updated_successfully",
                           Value = "Topics updated successfully",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "topics_created_successfully",
                           Value = "Topics created successfully",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "courses_deleted_unsuccessfully",
                           Value = "Courses deleted unsuccessfully",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "courses_updated_unsuccessfully",
                           Value = "Courses updated unsuccessfully",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "courses_created_unsuccessfully",
                           Value = "Courses created unsuccessfully",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "courses_deleted_successfully",
                           Value = "Courses deleted successfully",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "courses_updated_successfully",
                           Value = "Courses updated successfully",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "courses_created_successfully",
                           Value = "Courses created successfully",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "create_new_video",
                           Value = "Create new video",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "enter_caption_of_video",
                           Value = "Enter caption of video",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "enter_author_of_video",
                           Value = "Enter author of video",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "enter_url_name_of_video",
                           Value = "Enter URL name of video",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "create_new_topic",
                           Value = "Create new topic",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "enter_description_of_topic",
                           Value = "Enter description of text",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "enter_author_of_topic",
                           Value = "Enter author of topic",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "create_new_role",
                           Value = "Create new role",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "enter_role_name",
                           Value = "Enter role name",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "create_new_resource",
                           Value = "Create new resource",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "enter_value_of_resource",
                           Value = "Enter value of resource",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "enter_key_of_resource",
                           Value = "Enter key of resource",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "enter_culture_name",
                           Value = "Enter culture name",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "create_new_definition",
                           Value = "Create new definition",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "enter_author_of_definition",
                           Value = "Enter author of definition",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "enter_html_text_of_definition",
                           Value = "Enter definition text from HMTL",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "enter_name_of_topic",
                           Value = "Enter name of topic",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "go_to_main_page",
                           Value = "Go to main page",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "save",
                           Value = "Save",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "enter_description_of_course",
                           Value = "Enter description of course",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "enter_author_of_course",
                           Value = "Enter author of course",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "enter_name_of_course",
                           Value = "Enter name of course",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "deleting",
                           Value = "Deleting",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "editing",
                           Value = "Editing",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "refresh_page",
                           Value = "Refresh page",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "create_new_course",
                           Value = "Create a new course",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
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
                        },
                        new Resource
                        {
                            Key = "language_settings",
                            Value = "Language settings",
                            CreatedDate = DateTime.Now,
                            UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                            Key = "additional_settings",
                            Value = "Additional settings",
                            CreatedDate = DateTime.Now,
                            UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                            Key = "access_denied",
                            Value = "Access denied",
                            CreatedDate = DateTime.Now,
                            UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                            Key = "you_dont_have_permission_to_view_this_site",
                            Value = "You dont have permission to view this site",
                            CreatedDate = DateTime.Now,
                            UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                            Key = "go_to_home",
                            Value = "Go to home",
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
                           Key = "resources_deleted_unsuccessfully",
                           Value = "Ресурсы удалены неудачно",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "resources_updated_unsuccessfully",
                           Value = "Ресурсы обновлены неудачно",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "resources_created_unsuccessfully",
                           Value = "Ресурсы созданы неудачно",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "resources_deleted_successfully",
                           Value = "Ресурсы успешно удалены",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "resources_updated_successfully",
                           Value = "Ресурсы успешно обновлены",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "resources_created_successfully",
                           Value = "Ресурсы успешно созданы",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "definitions_deleted_unsuccessfully",
                           Value = "Определение удалены неудачно",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "definitions_updated_unsuccessfully",
                           Value = "Определение обновлены неудачно",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "definitions_created_unsuccessfully",
                           Value = "Определение созданы неудачно",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "definitions_deleted_successfully",
                           Value = "Определение успешно удалены",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "definitions_updated_successfully",
                           Value = "Определение успешно обновлены",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "definitions_created_successfully",
                           Value = "Определение успешно созданы",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "roles_deleted_unsuccessfully",
                           Value = "Pоль удалены неудачно",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "roles_updated_unsuccessfully",
                           Value = "Pоль обновлены неудачно",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "roles_created_unsuccessfully",
                           Value = "Pоль созданы неудачно",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "roles_deleted_successfully",
                           Value = "Pоль успешно удалены",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "roles_updated_successfully",
                           Value = "Pоль успешно обновлены",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "roles_created_successfully",
                           Value = "Pоль успешно созданы",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "videos_deleted_unsuccessfully",
                           Value = "Bидео удалены неудачно",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "videos_updated_unsuccessfully",
                           Value = "Bидео обновлены неудачно",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "videos_created_unsuccessfully",
                           Value = "Bидео созданы неудачно",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "videos_deleted_successfully",
                           Value = "Bидео успешно удалены",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "videos_updated_successfully",
                           Value = "Bидео успешно обновлены",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "videos_created_successfully",
                           Value = "Bидео успешно созданы",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "topics_deleted_unsuccessfully",
                           Value = "Tемы удалены неудачно",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "topics_updated_unsuccessfully",
                           Value = "Tемы обновлены неудачно",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "topics_created_unsuccessfully",
                           Value = "Tемы созданы неудачно",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "topics_deleted_successfully",
                           Value = "Tемы успешно удалены",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "topics_updated_successfully",
                           Value = "Tемы успешно обновлены",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "topics_created_successfully",
                           Value = "Tемы успешно созданы",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "courses_deleted_unsuccessfully",
                           Value = "Курсы удалены неудачно",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "courses_updated_unsuccessfully",
                           Value = "Курсы обновлены неудачно",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "courses_created_unsuccessfully",
                           Value = "Курсы созданы неудачно",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "courses_deleted_successfully",
                           Value = "Курсы успешно удалены",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "courses_updated_successfully",
                           Value = "Курсы успешно обновлены",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "courses_created_successfully",
                           Value = "Курсы успешно созданы",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "create_new_video",
                           Value = "Создать новое видео",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "enter_caption_of_video",
                           Value = "Введите заголовок видео",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "enter_author_of_video",
                           Value = "Введите автора видео",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "enter_url_name_of_video",
                           Value = "Введите URL-адрес видео",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "create_new_topic",
                           Value = "Создать новую тему",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "enter_description_of_topic",
                           Value = "Введите описание текста",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "enter_author_of_topic",
                           Value = "Введите автора темы",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "create_new_role",
                           Value = "Создать новую роль",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "enter_role_name",
                           Value = "Введите имя роли",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "create_new_resource",
                           Value = "Создать новый ресурс",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "enter_value_of_resource",
                           Value = "Введите стоимость ресурса",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "enter_key_of_resource",
                           Value = "Введите ключ ресурса",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "enter_culture_name",
                           Value = "Введите название культуры",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "create_new_definition",
                           Value = "Создать новое описание",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "enter_author_of_definition",
                           Value = "Введите автора описания",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "enter_html_text_of_definition",
                           Value = "Напишите описание в HTML-форме",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "enter_name_of_topic",
                           Value = "Введите название темы",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "go_to_main_page",
                           Value = "Перейти на главную страницу",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "save",
                           Value = "Сохранять",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "enter_description_of_course",
                           Value = "Введите описание курса",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "enter_author_of_course",
                           Value = "Введите автора курса",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "enter_name_of_course",
                           Value = "Введите название курса",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "deleting",
                           Value = "Удаление",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "editing",
                           Value = "Редактирование",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "refresh_page",
                           Value = "Обновить страницу",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "create_new_course",
                           Value = "Создать новый курс",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
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
                        },
                        new Resource
                        {
                            Key = "language_settings",
                            Value = "Языковые настройки",
                            CreatedDate = DateTime.Now,
                            UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                            Key = "additional_settings",
                            Value = "Дополнительные настройки",
                            CreatedDate = DateTime.Now,
                            UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                            Key = "access_denied",
                            Value = "Доступ запрещен",
                            CreatedDate = DateTime.Now,
                            UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                            Key = "you_dont_have_permission_to_view_this_site",
                            Value = "У вас нет разрешения на просмотр этого сайта",
                            CreatedDate = DateTime.Now,
                            UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                            Key = "go_to_home",
                            Value = "Иди домой",
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
                           Key = "resources_deleted_unsuccessfully",
                           Value = "Resursni o'chirish mumkin emas",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "resources_updated_unsuccessfully",
                           Value = "Resursni yangilash mumkin emas",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "resources_created_unsuccessfully",
                           Value = "Resursni yaratish mumkin emas",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "resources_deleted_successfully",
                           Value = "Resursni muvofaqiyatli o'chirildi",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "resources_updated_successfully",
                           Value = "Resursni muvofaqiyatli o'zgartirildi",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "resources_created_successfully",
                           Value = "Resursni muvofaqiyatli yaratildi",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "definitions_deleted_unsuccessfully",
                           Value = "Tafsifni o'chirish mumkin emas",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "definitions_updated_unsuccessfully",
                           Value = "Tafsifni yangilash mumkin emas",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "definitions_created_unsuccessfully",
                           Value = "Tafsifni yaratish mumkin emas",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "definitions_deleted_successfully",
                           Value = "Tafsifni muvofaqiyatli o'chirildi",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "definitions_updated_successfully",
                           Value = "Tafsifni muvofaqiyatli o'zgartirildi",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "definitions_created_successfully",
                           Value = "Tafsifni muvofaqiyatli yaratildi",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "roles_deleted_unsuccessfully",
                           Value = "Rolni o'chirish mumkin emas",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "roles_updated_unsuccessfully",
                           Value = "Rolni yangilash mumkin emas",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "roles_created_unsuccessfully",
                           Value = "Rolni yaratish mumkin emas",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "roles_deleted_successfully",
                           Value = "Rolni muvofaqiyatli o'chirildi",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "roles_updated_successfully",
                           Value = "Rolni muvofaqiyatli o'zgartirildi",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "roles_created_successfully",
                           Value = "Rolni muvofaqiyatli yaratildi",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "videos_deleted_unsuccessfully",
                           Value = "Videoni o'chirish mumkin emas",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "videos_updated_unsuccessfully",
                           Value = "Videoni yangilash mumkin emas",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "videos_created_unsuccessfully",
                           Value = "Videoni yaratish mumkin emas",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "videos_deleted_successfully",
                           Value = "Videoni muvofaqiyatli o'chirildi",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "videos_updated_successfully",
                           Value = "Videoni muvofaqiyatli o'zgartirildi",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "videos_created_successfully",
                           Value = "Videoni muvofaqiyatli yaratildi",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "topics_deleted_unsuccessfully",
                           Value = "Mavzuni o'chirish mumkin emas",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "topics_updated_unsuccessfully",
                           Value = "Mavzuni yangilash mumkin emas",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "topics_created_unsuccessfully",
                           Value = "Mavzuni yaratish mumkin emas",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "topics_deleted_successfully",
                           Value = "Mavzuni muvofaqiyatli o'chirildi",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "topics_updated_successfully",
                           Value = "Mavzuni muvofaqiyatli o'zgartirildi",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "topics_created_successfully",
                           Value = "Mavzuni muvofaqiyatli yaratildi",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "courses_deleted_unsuccessfully",
                           Value = "Kursni o'chirish mumkin emas",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "courses_updated_unsuccessfully",
                           Value = "Kursni yangilash mumkin emas",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "courses_created_unsuccessfully",
                           Value = "Kursni yaratish mumkin emas",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "courses_deleted_successfully",
                           Value = "Kursni muvofaqiyatli o'chirildi",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "courses_updated_successfully",
                           Value = "Kursni muvofaqiyatli o'zgartirildi",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "courses_created_successfully",
                           Value = "Kursni muvofaqiyatli yaratildi",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "create_new_video",
                           Value = "Yangi videoni yaratish",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "enter_caption_of_video",
                           Value = "Video uchun matn yozing",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "enter_author_of_video",
                           Value = "Videoni muallifini kiriting",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "enter_url_name_of_video",
                           Value = "Videoni URL manzilini kiriting",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "create_new_topic",
                           Value = "Yangi mavzuni yaratish",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "enter_description_of_topic",
                           Value = "Mavzuni tavsifini kiriting",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "enter_author_of_topic",
                           Value = "Mavzuni mualifini kiriting",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "create_new_role",
                           Value = "Yangi rolni yaratish",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "enter_role_name",
                           Value = "Rolni nomini kiriting",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "create_new_resource",
                           Value = "Yangi resursni yaratish",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "enter_value_of_resource",
                           Value = "Resurs uchun qiymatini kiriting",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "enter_key_of_resource",
                           Value = "Resurs uchun kalit so'zni kiriting",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "enter_culture_name",
                           Value = "Tilni nomini kiritng",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "create_new_definition",
                           Value = "Yangi tafsifni yaratish",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "enter_author_of_definition",
                           Value = "Tavsifni muallifini kiriting",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "enter_html_text_of_definition",
                           Value = "Tavsifni HTML ko'rinishida yozing",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "enter_name_of_topic",
                           Value = "Mavzuni nomini kiriting",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "go_to_main_page",
                           Value = "Asosiy sahifaga qaytish",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "save",
                           Value = "Saqlash",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "enter_description_of_course",
                           Value = "Kurs haqida batafsil yozing",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "enter_author_of_course",
                           Value = "Kurni muallifini kiriting",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "enter_name_of_course",
                           Value = "Kursni nomini kiriting",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "deleting",
                           Value = "O'chirish",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "editing",
                           Value = "Tahrirlash",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "refresh_page",
                           Value = "Sahifani yangilash",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                           Key = "create_new_course",
                           Value = "Yangi kursni yaratish",
                           CreatedDate = DateTime.Now,
                           UpdatedDate = DateTime.Now
                        },
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
                        },
                        new Resource
                        {
                            Key = "language_settings",
                            Value = "Til sozlamalari",
                            CreatedDate = DateTime.Now,
                            UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                            Key = "additional_settings",
                            Value = "Additional settings",
                            CreatedDate = DateTime.Now,
                            UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                            Key = "access_denied",
                            Value = "Ruxsat berilmadi",
                            CreatedDate = DateTime.Now,
                            UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                            Key = "you_dont_have_permission_to_view_this_site",
                            Value = "Sizda bu saytni ko'rish uchun ruxsat yo'q",
                            CreatedDate = DateTime.Now,
                            UpdatedDate = DateTime.Now
                        },
                        new Resource
                        {
                            Key = "go_to_home",
                            Value = "Bosh sahifaga qaytish",
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