using ArtberryApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtberryApp.Services
{
    public class AppService
    {


        public async Task<SessionModel> Login(LoginModel loginModel)
        {
            var returnResponse = new SessionModel();
            using (var client = new HttpClient())
            {
                var url = $"";

                var serializedStr = JsonConvert.SerializeObject(loginModel);

                var response = await client.PostAsync(url, new StringContent(serializedStr, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    string contentStr = await response.Content.ReadAsStringAsync();
                    returnResponse = JsonConvert.DeserializeObject<SessionModel>(contentStr);


                    await SecureStorage.SetAsync("session", "");
                }
            }
            return returnResponse;
        }


        public async Task<SessionModel> GetUserInfo()
        {
            var returnResponse = new SessionModel();
            using (var client = new HttpClient())
            {
                var url = $"";

                var session = await SecureStorage.GetAsync("session");

                var serializedStr = JsonConvert.SerializeObject(session);

                var response = await client.PostAsync(url, new StringContent(serializedStr, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    string contentStr = await response.Content.ReadAsStringAsync();
                    returnResponse = JsonConvert.DeserializeObject<SessionModel>(contentStr);


                    await SecureStorage.SetAsync("session", "");
                }
            }
            return returnResponse;
        }

        public async Task<SessionModel> Logout(LoginModel loginModel)
        {
            var returnResponse = new SessionModel();
            using (var client = new HttpClient())
            {
                var url = $"";

                var serializedStr = JsonConvert.SerializeObject(loginModel);

                var response = await client.PostAsync(url, new StringContent(serializedStr, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    string contentStr = await response.Content.ReadAsStringAsync();
                    returnResponse = JsonConvert.DeserializeObject<SessionModel>(contentStr);


                    await SecureStorage.SetAsync("session", "");
                }
            }
            return returnResponse;
        }
    }
}
