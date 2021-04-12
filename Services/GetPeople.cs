using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project3_FinalExam.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace Project3_FinalExam.Services
{
    public class GetPeople : IGetPeople
    {

        public async Task<List<People>> GetAllPeople(string t)
        {
            //making httprequest
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://www.ist.rit.edu/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    //retrieving data from RESTAPI
                    HttpResponseMessage response = await client.GetAsync("api/people/"+t, HttpCompletionOption.ResponseHeadersRead);
                    response.EnsureSuccessStatusCode();
                    var data = await response.Content.ReadAsStringAsync();
                    var rtnResults = JsonConvert.DeserializeObject<Dictionary<string, List<People>>>(data);
                    List<People> facultyList = new List<People>();
                    People faculty = new People();

                    // parsing data and storing in list
                    foreach (KeyValuePair<string, List<People>> kvp in rtnResults)
                    {
                        foreach (var item in kvp.Value)
                        {
                            facultyList.Add(item);
                        }
                    }

                    return facultyList;
                }
                //exceptions if try block fails
                catch (HttpRequestException hre)
                {
                    var msg = hre.Message;
                    List<People> facultyList = new List<People>();
                    return facultyList;

                }
                catch (Exception ex)
                {
                    var msg = ex.Message;
                    List<People> facultyList = new List<People>();
                    return facultyList;

                }
            }
        }
    }
}
