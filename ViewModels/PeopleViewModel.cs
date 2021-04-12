using System.Collections.Generic;
using Project3_FinalExam.Models;

namespace Project3_FinalExam.ViewModels
{
    public class PeopleViewModel
    {
        public List<People> Faculty { get; set; }

        public List<People> Staff { get; set; }

        public string Title { get; set; }

        public string Title1 { get; set; }

        public string Title2 { get; set; }
    }
}
