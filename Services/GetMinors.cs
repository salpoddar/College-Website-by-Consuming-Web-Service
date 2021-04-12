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
    public class GetMinors : IGetMinors
    {
        public async Task<List<Minors>> GetAllMinors()
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
                    HttpResponseMessage response = await client1.GetAsync("api/minors", HttpCompletionOption.ResponseHeadersRead);
                    response.EnsureSuccessStatusCode();
                    var data = await response.Content.ReadAsStringAsync();
                    var rtnResults = JsonConvert.DeserializeObject<Dictionary<string, List<Minors>>>(data);
                    List<Minors> minorList = new List<Minors>();
                    Minors minor = new Minors();

                    // parsing data and storing in list
                    foreach (KeyValuePair<string, List<Minors>> kvp in rtnResults)
                    {
                        foreach (var item in kvp.Value)
                        {
                            minorList.Add(item);
                        }
                    }

                    return minorList;



                }
                //exception blocks if try fails
                catch (HttpRequestException hre)
                {
                    var msg = hre.Message;
                    List<Minors> minorList = new List<Minors>();
                    return minorList;
                }
                catch (Exception ex)
                {
                    var msg = ex.Message;
                    List<Minors> minorList = new List<Minors>();
                    return minorList;
                }
            }
        }
    }
}
