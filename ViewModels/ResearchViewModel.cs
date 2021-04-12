using System.Collections.Generic;
using Project3_FinalExam.Models;

namespace Project3_FinalExam.ViewModels
{
    public class ResearchViewModel
    {
        public List<Research> byInterest { get; set; }

        public List<Research> byFaculty { get; set; }

        public string Title { get; set; }

        public string Title1 { get; set; }

        public string Title2 { get; set; }
    }
}
