using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Project3_FinalExam.Services;
using Project3_FinalExam.ViewModels;

namespace Project3_FinalExam.Controllers
{
    //Constructor to get repositories for all the views
    public class HomeController : Controller
    {
        private readonly IGetPeople _peopleRepository;
        private readonly IGetDegrees _degreesRepository;
        private readonly IGetMinors _minorsRepository;
        private readonly IGetResearch _researchRepository;
        private readonly IGetEmployment _employmentRepository;
        
        public HomeController(IGetPeople peopleRepository, IGetDegrees degreesRepository, IGetMinors minorsRepository,
            IGetResearch researchRepository, IGetEmployment employmentRepository)
        {
            _peopleRepository = peopleRepository;
            _degreesRepository = degreesRepository;
            _minorsRepository = minorsRepository;
            _researchRepository = researchRepository;
            _employmentRepository = employmentRepository;
        }
      
        //calling about service and passing result to Index/home view
        public async Task<IActionResult> Index()
        {
            var allabout = new GetAbout();
            var about = allabout.GetAllAbout().Result;
            var aboutViewModel = new AboutViewModel()
            {
                About = about,
                Title = "RIT IST Department"
            };
            return View(aboutViewModel);
        }

        //calling people service and passing result to people view
        public async Task<IActionResult> People()
        {
            var allfaculty = await _peopleRepository.GetAllPeople("faculty");
            var allstaff = await _peopleRepository.GetAllPeople("staff");
            var peopleViewModel = new PeopleViewModel()
            {
                Faculty = allfaculty,
                Staff = allstaff,
                Title = "People",
                Title1 = "Faculty",
                Title2 = "Staff"
            };
            return View(peopleViewModel);
        }

        //calling degrees service and passing result to degrees view
        public async Task<IActionResult> Degrees()
        {
            var under = await _degreesRepository.GetAllDegrees("undergraduate");
            var grad = await _degreesRepository.GetAllDegrees("graduate");
            var degreeViewModel = new DegreeViewModel()
            {
                UnderGrads = under,
                grads = grad,
                Title = "Degree Programs",
                Title1 = "Undergraduate Program",
                Title2 = "Graduate Program"
            };
            return View(degreeViewModel);
        }

        //calling minors service and passing result to minors view
        public async Task<IActionResult> Minors()
        {
            var minors = await _minorsRepository.GetAllMinors();
            var minorViewModel = new MinorViewModel()
            {
                minors = minors,
                Title = "UG Minors"
            };
            return View(minorViewModel);
        }

        //calling research service and passing result to research view
        public async Task<IActionResult> Research()
        {
            var rinterest = await _researchRepository.GetAllResearch("byInterestArea");
            var rfaculty = await _researchRepository.GetAllResearch("byFaculty");
            var researchViewModel = new ResearchViewModel()
            {
                byInterest = rinterest,
                byFaculty = rfaculty,
                Title = "Research",
                Title1 = "Research by Interest",
                Title2 = "Research by Faculty"
            };
            return View(researchViewModel);
        }

        //calling employment service and passing result to employment view
        public async Task<IActionResult> Employment()
        {
            var coopEmployment = await _employmentRepository.GetAllEmployment("coopTable/coopInformation");
            var fulltimeEmployment = await _employmentRepository.GetAllEmployment("employmentTable/professionalEmploymentInformation");
            var employmentViewModel = new EmploymentViewModel()
            {
                coop =  coopEmployment,
                fulltime = fulltimeEmployment,
                Title = "Employment",
                Title1 = "CO-OP Employment",
                Title2 = "Full-Time Employment"
            };
            return View(employmentViewModel);
        }
        public IActionResult Privacy()
        {
            return View();
        }

    }
}
