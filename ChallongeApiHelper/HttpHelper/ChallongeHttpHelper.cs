using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ChallongeApiHelper.HttpHelper
{
    public static partial class ChallongeHttpHelper
    {
        private static HttpClient challHttpClient;

        static ChallongeHttpHelper()
        {
            challHttpClient = new HttpClient();
            challHttpClient.BaseAddress = new Uri("https://api.challonge.com/v1/");
            setAuthorizationHeader(ConfigurationManager.AppSettings["ApiUsername"], ConfigurationManager.AppSettings["ApiPassword"]);
        }

        public static bool setAuthorizationHeader(string userName, string password)
        {
            if(challHttpClient.DefaultRequestHeaders.Contains("Authorization"))
            {
                challHttpClient.DefaultRequestHeaders.Remove("Authorization");
            }

            string encodedHeader = Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes($"{userName}:{password}"));

            challHttpClient.DefaultRequestHeaders.Add("Authorization", $"Basic {encodedHeader}");

            return true;
        }

        public static string BasicGet(string route)
        {
            return challHttpClient.GetAsync(route).Result.Content.ReadAsStringAsync().Result;
        }

        public static string Post(string route, object body)
        {
            string bodyJson = JsonConvert.SerializeObject(body);
            var httpContent = new StringContent(bodyJson, Encoding.UTF8, "application/json");

            return challHttpClient.PostAsync(route, httpContent).Result.Content.ReadAsStringAsync().Result;
        }

        private static bool CheckHeaders()
        {
            return challHttpClient.DefaultRequestHeaders.Contains("Authorization");
        }
    }
}
