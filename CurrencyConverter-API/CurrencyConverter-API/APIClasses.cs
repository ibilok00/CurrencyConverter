using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CurrencyConverter_API
{
    public  class APIClasses
    {
        public class Root
        {
            public Rate rates { get; set; }
            public long timestamp;
            public string license;
        }

        public class Rate
        {
            // These names of properties must be the same like in API Get Response
            public double INR { get; set; }
            public double JPY { get; set; }
            public double USD { get; set; }
            public double NZD { get; set; }
            public double EUR { get; set; }
            public double CAD { get; set; }
            public double ISK { get; set; }
            public double PHP { get; set; }
            public double DKK { get; set; }
            public double CZK { get; set; }
        }

        public static async Task<Root> GetData<T>(string url)
        {
            var myRoot = new Root();
            try
            {
                using (var client = new HttpClient()) // HttpClient class provides a base class for sendind/receiving the HTTP request/response from a URL
                {
                    client.Timeout = TimeSpan.FromMinutes(1); // The TimeSpan to wait before the request times out.
                    HttpResponseMessage response = await client.GetAsync(url); // HttpResponseMessage is a way of returning a message/data from your action
                    if (response.StatusCode == System.Net.HttpStatusCode.OK) // Check API response status code ok
                    {
                        var ResponseString = await response.Content.ReadAsStringAsync(); // Serialize the Http content to a string as an asynchronous operation
                        var ResponseObject = JsonConvert.DeserializeObject<Root>(ResponseString); // JsonConvert.DeserializeObject to deserialize Json to a C#

                        return ResponseObject; // return API responce
                    }
                    return myRoot;
                }
                
            }
            catch
            {

                return myRoot;
            }
        }
    }
}
