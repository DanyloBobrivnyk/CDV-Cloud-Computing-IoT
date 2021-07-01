using Firebase.Database;
using Firebase.Database.Query;
using IoT_Project_Food_Ordering.DTOs.API_Dto;
using IoT_Project_Food_Ordering.Helpers;
using IoT_Project_Food_Ordering.Models;
using Lab1_bobrivnyk.Rest.DTOs;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IoT_Project_Food_Ordering.Services
{
    public class UserService
    {
        FirebaseClient dbClient;
        HttpClient client;
        string connectionString = "https://dbobrivnykapi.azurewebsites.net";
        public UserService()
        {
            dbClient = new FirebaseClient("https://iotprojectfoodordering-default-rtdb.europe-west1.firebasedatabase.app/");
            client = new HttpClient();
        }

        public async Task<bool> IsUserExists(string uname)
        {
            var user = (await dbClient.Child("Users")
                .OnceAsync<User>()).Where(u => u.Object.Username == uname).FirstOrDefault();

            return (user != null);
        }

        public async Task<bool> RegisterUser(string uname, string passwd)
        {
            Uri uri = new Uri(connectionString + "/Users/register");

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = (JsonConvert.SerializeObject(new User
                {
                    Username = uname,
                    Password = passwd
                }));

                streamWriter.Write(json);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            if(httpResponse.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }

            return false;
        }

        public async Task<string> LoginUser(string uname, string passwd)
        {
            Uri uri = new Uri(connectionString + "/Users/authenticate");

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = (JsonConvert.SerializeObject(new User
                {
                    Username = uname,
                    Password = passwd
                }));

                streamWriter.Write(json);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            if (httpResponse.StatusCode == HttpStatusCode.OK)
            {
                string result = "";
                var token = ConnectionClass.GetResponseFromRequest(uri.ToString(), ref httpWebRequest);

                var data = JsonConvert.DeserializeObject<AuthenticateResponse>(token);
                if (token.Length > 0)
                {
                    result = data.JwtToken;
                }
                
                return result;
            }
            else
            {
                return "";
            }

            /*var user = (await dbClient.Child("Users")
                .OnceAsync<User>()).Where(u => u.Object.Username == uname)
                .Where(u => u.Object.Password == passwd).FirstOrDefault();

            return (user != null);*/
        }
    }
}
