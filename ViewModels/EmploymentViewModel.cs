using System.Collections.Generic;
using Project3_FinalExam.Models;


namespace Project3_FinalExam.ViewModels
{
    public class EmploymentViewModel
    {
        public List<Employment> coop { get; set; }

        public List<Employment> fulltime { get; set; }

        public string Title { get; set; }

        public string Title1 { get; set; }

        public string Title2 { get; set; }
    }
}
