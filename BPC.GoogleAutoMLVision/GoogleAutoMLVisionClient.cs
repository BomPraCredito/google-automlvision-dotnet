using Google.Apis.Auth.OAuth2;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BPC.GoogleAutoMLVision
{
    public class GoogleAutoMLVisionClient
    {
        private ICredential _credential;

        public GoogleAutoMLVisionClient()
        {
            _credential = GetCredential();
        }

        public GoogleAutoMLVisionClient(string jsonCredentialsPath)
        {
            _credential = GetCredential(jsonCredentialsPath);
        }

        public async Task<PredictResults> Predict(string endPointUrl, Stream imageStream)
        {
            var bearer = await _credential.GetAccessTokenForRequestAsync();
            imageStream.Position = 0;
            var requestBody = new
            {
                payload = new
                {
                    image = new
                    {
                        imageBytes = Utils.StreamToBase64(imageStream)
                    }
                }
            };

            using (var client = new HttpClient())
            {
                var contentStr = JsonConvert.SerializeObject(requestBody);
                var content = new StringContent(contentStr, Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", bearer);
                var result = await client.PostAsync(endPointUrl, content);
                if (!result.IsSuccessStatusCode)
                    throw new Exception($"{result.StatusCode} - {result.ReasonPhrase}");
                var strResult = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<PredictResults>(strResult);
            }
        }

        private ICredential GetCredential(string jsonCredentialsPath=null)
        {
            GoogleCredential cred;

            if (string.IsNullOrEmpty(jsonCredentialsPath))
                cred = GoogleCredential.GetApplicationDefault();
            else
            {
                var json = File.ReadAllText(jsonCredentialsPath);
                cred = GoogleCredential.FromJson(json);
            }
            
            return cred.CreateScoped("https://www.googleapis.com/auth/cloud-platform", 
                "https://www.googleapis.com/auth/cloud-vision").UnderlyingCredential;
        }



    }
}
