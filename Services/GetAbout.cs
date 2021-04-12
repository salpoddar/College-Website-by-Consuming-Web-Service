using System;
using System.Threading.Tasks;
using Project3_FinalExam.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;


namespace Project3_FinalExam.Services
{
    public class GetAbout : IGetAbout
    {
        public async Task<About> GetAllAbout()
        {
            //making httprequest
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://www.ist.rit.edu/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    //retrieving data from the RESTAPI
                    HttpResponseMessage response = await client.GetAsync("api/about/", HttpCompletionOption.ResponseHeadersRead);
                    response.EnsureSuccessStatusCode();     // Will throw an exception if request is not successful
                    var data = await response.Content.ReadAsStringAsync();
                    var rtnResults = JsonConvert.DeserializeObject<About>(data);
                    return rtnResults;

                }

                //exceptions if try block fails

                catch (HttpRequestException hre)
                {
                    var msg = hre.Message;
                    About about = new About();
                    return about;
                }
                catch (Exception ex)
                {
                    var msg = ex.Message;
                    About about = new About();
                    return about;
                }
            }
        }

    }
}
