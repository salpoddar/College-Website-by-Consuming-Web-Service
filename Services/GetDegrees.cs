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
    public class GetDegrees : IGetDegrees
    {
        public async Task<List<Degrees>> GetAllDegrees(String t)
        {
            //making httprequest
            using (var client1 = new HttpClient())
            {
                client1.BaseAddress = new Uri("http://www.ist.rit.edu/");
                client1.DefaultRequestHeaders.Accept.Clear();
                client1.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    //retrieving data from RESTAPI

                    HttpResponseMessage response = await client1.GetAsync("api/degrees/"+t, HttpCompletionOption.ResponseHeadersRead);
                    response.EnsureSuccessStatusCode();
                    var data = await response.Content.ReadAsStringAsync();
                    var rtnResults = JsonConvert.DeserializeObject<Dictionary<string, List<Degrees>>>(data);
                    List<Degrees> underGradList = new List<Degrees>();
                    Degrees underGradMajors = new Degrees();

                    // parsing data and storing in list
                    foreach (KeyValuePair<string, List<Degrees>> kvp in rtnResults)
                    {
                        foreach (var item in kvp.Value)
                        {
                            underGradList.Add(item);
                        }
                    }

                    return underGradList;



                }
                //exceptions if try block fails
                catch (HttpRequestException hre)
                {
                    var msg = hre.Message;
                    List<Degrees> underGradMajorsList = new List<Degrees>();
                    return underGradMajorsList;
                }
                catch (Exception ex)
                {
                    var msg = ex.Message;
                    List<Degrees> underGradMajorsList = new List<Degrees>();
                    return underGradMajorsList;
                }
            }
        }        
    }
}
