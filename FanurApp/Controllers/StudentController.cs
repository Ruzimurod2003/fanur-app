using FanurApp.Repositories;
using FanurApp.ViewModels.Student;
using Microsoft.AspNetCore.Mvc;

namespace FanurApp.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentRepository studentRepository;
        private readonly IAdministratorRepository administratorRepository;

        public StudentController(IStudentRepository _studentRepository, IAdministratorRepository _administratorRepository)
        {
            studentRepository = _studentRepository;
            administratorRepository = _administratorRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Listening()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Video(int? topicId)
        {
            VideoVM viewModel = new VideoVM();
            if (topicId == null || topicId <= 0)
            {
                topicId = 1;
            }
            var topic = administratorRepository.GetTopicById(topicId.Value);
            viewModel.PreviousTopicId = (topicId == 1) ? null : topicId - 1;
            viewModel.NextTopicId = topicId + 1;

            if (administratorRepository.GetAllTopics().Max(i => i.Id) < viewModel.NextTopicId) 
            {
                viewModel.NextTopicId = null;
            }
            viewModel.TopicName = topic.Name;
            return View(viewModel);
        }
        public IActionResult Quiz()
        {
            return View();
        }
        public IActionResult Introduction()
        {
            return View();
        }
        public IActionResult Payment()
        {
            return View();
        }
    }
}
