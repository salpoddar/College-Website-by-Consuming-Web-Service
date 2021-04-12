using Project3_FinalExam.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project3_FinalExam.Services
{
    public interface IGetPeople
    {
        Task<List<People>> GetAllPeople(string t);
    }
}