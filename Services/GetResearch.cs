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
    public class GetResearch : IGetResearch
    {
        public async Task<List<Research>> GetAllResearch(string t)
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
                    HttpResponseMessage response = await client.GetAsync("api/research/" + t, HttpCompletionOption.ResponseHeadersRead);
                    response.EnsureSuccessStatusCode();
                    var data = await response.Content.ReadAsStringAsync();
                    var rtnResults = JsonConvert.DeserializeObject<Dictionary<string, List<Research>>>(data);
                    List<Research> researchList = new List<Research>();
                    Research res = new Research();

                    // parsing data and storing in list
                    foreach (KeyValuePair<string, List<Research>> kvp in rtnResults)
                    {
                        foreach (var item in kvp.Value)
                        {
                            researchList.Add(item);
                        }
                    }
                    return researchList;
                }
                //exception blocks if try fails
                catch (HttpRequestException hre)
                {
                    var msg = hre.Message;
                    List<Research> researchList = new List<Research>();
                    return researchList;

                }
                catch (Exception ex)
                {
                    var msg = ex.Message;
                    List<Research> researchList = new List<Research>();
                    return researchList;

                }
            }
        }
    }
}
