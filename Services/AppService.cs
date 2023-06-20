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
                var url = $"{Setting.BaseUrl}/get-session";

                var serializedStr = JsonConvert.SerializeObject(loginModel);

                var response = await client.PostAsync(url, new StringContent(serializedStr, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    string contentStr = await response.Content.ReadAsStringAsync();
                    returnResponse = JsonConvert.DeserializeObject<SessionModel>(contentStr);
                }
            }
            return returnResponse;
        }


        public async Task<UserInfo> GetUserInfo()
        {

				var returnResponse = new UserInfo();

				using (var client = new HttpClient())
				{
					var url = $"{Setting.BaseUrl}/session-user";

					var session = await SecureStorage.GetAsync("session");

					client.DefaultRequestHeaders.Add("SessionId", session);

					var response = await client.GetAsync(url);

					if (response.IsSuccessStatusCode)
					{
						string contentStr = await response.Content.ReadAsStringAsync();
						returnResponse = JsonConvert.DeserializeObject<UserInfo>(contentStr);
					}
				}
				return returnResponse;
		
        }

        public async Task<bool> Logout()
        {
            var returnResponse = false;

            var session = await SecureStorage.GetAsync("session");

            if (string.IsNullOrEmpty(session))
            {
                return returnResponse;
            }

            using (var client = new HttpClient())
            {
                var url = $"{Setting.BaseUrl}/logout";

                client.DefaultRequestHeaders.Add("SessionId", session);

                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string contentStr = await response.Content.ReadAsStringAsync();
                    returnResponse = JsonConvert.DeserializeObject<bool>(contentStr);

                    SecureStorage.Remove("session");
                    Setting.Session = null;
				}
            }
            return returnResponse;
        }


        public async Task<bool> CheckSession()
        {
            var returnResponse = false;
            var session = await SecureStorage.GetAsync("session");

            if (string.IsNullOrEmpty(session))
            {
                SecureStorage.Remove("session");
                Setting.Session = null;
                return returnResponse;
            }

            using (var client = new HttpClient())
            {
                var url = $"{Setting.BaseUrl}/check-session";

                client.DefaultRequestHeaders.Add("SessionId", session);

                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string contentStr = await response.Content.ReadAsStringAsync();
                    returnResponse = JsonConvert.DeserializeObject<bool>(contentStr);
                }
            }
            return returnResponse;
        }
    }
}
