using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GreenHouseMonitoringClient
{
    class CreateGetHttpResponse
    {
        string Username = "admin"; string Password = "public";
        public string Get(string url)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Method = "GET";
            request.CookieContainer = new CookieContainer();
            request.ContentType = "application/json;";
            var myClientHandler = new HttpClientHandler();
            myClientHandler.Credentials = new NetworkCredential(Username, Password);
            request.Credentials = myClientHandler.Credentials;
            request.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes($"{Username}:{Password}")));
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    string content = reader.ReadToEnd();
                    return content;
                }
            }
        }
    }
}
