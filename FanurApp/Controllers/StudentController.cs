using FanurApp.ViewModels.Student;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;
using System.Net;
using Telegram.Bot;

namespace FanurApp.Controllers
{
    public class StudentController : Controller
    {
        private readonly IConfiguration configuration;

        public StudentController(IConfiguration _configuration)
        {
            configuration = _configuration;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ContactUs()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ContactUsAsync(ContactUsVM viewModel)
        {
            if (ModelState.IsValid)
            {
                string token = configuration.GetValue<string>("Telegram:Token");
                string adminId = configuration.GetValue<string>("Telegram:AdminId");

                var bot = new TelegramBotClient(token);
                string message =    $"Ismi: <b>{viewModel.ContactName}</b> \n" +
                                    $"Telefon nomeri: <b>{viewModel.ContactPhone}</b> \n" +
                                    $"Yuboradigan mavzusi: <b>{viewModel.Subject}</b> \n" +
                                    $"Habar: <b>{viewModel.Message}</b>";

                await bot.SendTextMessageAsync(adminId, message, parseMode:Telegram.Bot.Types.Enums.ParseMode.Html);
            }
            return View(viewModel);
        }
        public IActionResult PrivacyPolicy()
        {
            return View();
        }
    }
}
