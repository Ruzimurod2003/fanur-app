using FanurApp.Repositories;
using FanurApp.ViewModels.Main;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;

namespace FanurApp.Controllers
{
    public class MainController : Controller
    {
        private readonly IMainRepository repository;
        private readonly IConfiguration configuration;

        public MainController(IMainRepository _repository, IConfiguration _configuration)
        {
            repository = _repository;
            configuration = _configuration;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Course(int id)
        {
            var data = repository.GetCourseById(id);
            return View(data);
        }
        public IActionResult Beginner()
        {
            return View();
        }
        public IActionResult Elementary()
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
                string message = $"Ismi: <b>{viewModel.ContactName}</b> \n" +
                                    $"Telefon nomeri: <b>{viewModel.ContactPhone}</b> \n" +
                                    $"Yuboradigan mavzusi: <b>{viewModel.Subject}</b> \n" +
                                    $"Habar: <b>{viewModel.Message}</b>";

                await bot.SendTextMessageAsync(adminId, message, parseMode: Telegram.Bot.Types.Enums.ParseMode.Html);
                return RedirectToAction("Index", "Main", null);
            }
            return View(viewModel);
        }
        public IActionResult PrivacyPolicy()
        {
            return View();
        }
        public IActionResult AboutUs()
        {
            return View();
        }
    }
}
