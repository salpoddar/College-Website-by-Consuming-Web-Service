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
    public class GetEmployment : IGetEmployment
    {
        public async Task<List<Employment>> GetAllEmployment(string t)
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
                    HttpResponseMessage response = await client.GetAsync("api/employment/" + t, HttpCompletionOption.ResponseHeadersRead);
                    response.EnsureSuccessStatusCode();
                    var data = await response.Content.ReadAsStringAsync();

                    var rtnResults = JsonConvert.DeserializeObject<Dictionary<string, List<Employment>>>(data);

                    List<Employment> employmentList = new List<Employment>();
                    Employment emp = new Employment();

                    // parsing data and storing in list
                    foreach (KeyValuePair<string, List<Employment>> kvp in rtnResults)
                    {
                        foreach (var item in kvp.Value)
                        {
                            employmentList.Add(item);
                        }
                    }

                    return employmentList;
                }
                //exceptions if try block fails
                catch (HttpRequestException hre)
                {
                    var msg = hre.Message;
                    List<Employment> employmentList = new List<Employment>();
                    return employmentList;

                }
                catch (Exception ex)
                {
                    var msg = ex.Message;
                    List<Employment> employmentList = new List<Employment>();
                    return employmentList;

                }
            }
        }
    }
}
