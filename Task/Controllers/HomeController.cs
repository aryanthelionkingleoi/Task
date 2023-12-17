using Microsoft.AspNetCore.Mvc;
using Task.Models;

namespace Task.Controllers
{
    public class HomeController : Controller
    {
        private readonly List<QuestionModel> questions = new List<QuestionModel>
        {
            new QuestionModel { Id = 1, Area = "Area 1", Section = "Section 1", Subsection = "Subsection 1", Question = "Question 1.1.1", QuestionNumber = 1 },
            new QuestionModel { Id = 2, Area = "Area 1", Section = "Section 1", Subsection = "Subsection 1", Question = "Question 1.1.2", QuestionNumber = 2 },
            new QuestionModel { Id = 3, Area = "Area 1", Section = "Section 1", Subsection = "Subsection 2", Question = "Question 1.1.3", QuestionNumber = 1 },
            new QuestionModel { Id = 4, Area = "Area 1", Section = "Section 2", Subsection = "Subsection 3", Question = "Question 1.2.1", QuestionNumber = 1 },
            new QuestionModel { Id = 5, Area = "Area 2", Section = "Section 3", Subsection = "Subsection 4", Question = "Question 2.3.1", QuestionNumber = 1 },
            new QuestionModel { Id = 6, Area = "Area 2", Section = "Section 3", Subsection = "Subsection 4", Question = "Question 2.3.2", QuestionNumber = 2 },
            new QuestionModel { Id = 7, Area = "Area 2", Section = "Section 3", Subsection = "Subsection 4", Question = "Question 2.3.3", QuestionNumber = 3 },
            new QuestionModel { Id = 8, Area = "Area 2", Section = "Section 3", Subsection = "Subsection 4", Question = "Question 2.3.4", QuestionNumber = 4 }
        };
        public HomeController(){}

        public IActionResult Index()
        {
            ViewBag.Areas = questions.Select(q => q.Area).Distinct();
            var firstQuestion = questions.FirstOrDefault();
            if (firstQuestion != null)
            {
                ViewBag.Sections = questions.Where(q => q.Area == firstQuestion.Area).Select(q => q.Section).Distinct();

                ViewBag.Subsections = questions
                    .Where(q => q.Area == firstQuestion.Area && q.Section == firstQuestion.Section)
                    .Select(q => q.Subsection)
                    .Distinct();
            }
            else
            {
                ViewBag.Sections = new List<string>();
                ViewBag.Subsections = new List<string>();
            }

            return View(questions);
        }

        [HttpPost]
        public IActionResult GetSections(string selectedArea)
        {
            var sections = questions.Where(q => q.Area == selectedArea).Select(q => q.Section).Distinct();
            return Json(sections);
        }

        [HttpPost]
        public IActionResult GetSubsections(string selectedArea, string selectedSection)
        {
            var subsections = questions
                .Where(q => q.Area == selectedArea && q.Section == selectedSection)
                .Select(q => q.Subsection)
                .Distinct();
            return Json(subsections);
        }

        [HttpPost]
        public IActionResult GetQuestions(string selectedArea, string selectedSection, string selectedSubsection)
        {
            var filteredQuestions = questions
                .Where(q => q.Area == selectedArea && q.Section == selectedSection && q.Subsection == selectedSubsection)
                .ToList();

            return Json(filteredQuestions);
        }
    }
}
