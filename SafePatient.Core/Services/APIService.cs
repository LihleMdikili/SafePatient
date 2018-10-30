using Newtonsoft.Json;
using SafePatient.Core.Models;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace SafePatient.Core.Services {
    class ApiService : IApiService {
        private readonly string _baseUrl = "https://www.atozofmedicines.com/app/api.json/";
        private string _token;

        public async Task<Interaction> CheckInteractionsAsync(string[] medicationIds) {
            string medIds = medicationIds[0];
            for (int i = 1; i < medicationIds.Length; i++) {
                medIds += "|" + medicationIds[i];
            }
            string url = _baseUrl + $"Interactions?lst={medIds}";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(url));
            request.ContentType = "application/json";
            request.Method = "GET";
            WebHeaderCollection header = new WebHeaderCollection {
                { "Token", _token }
            };
            request.Headers = header;

            using (WebResponse response = await request.GetResponseAsync()) {
                using (Stream stream = response.GetResponseStream()) {
                    using (var streamReader = new StreamReader(stream)) {
                        return JsonConvert.DeserializeObject<Interaction>(streamReader.ReadToEnd());
                    }
                }
            }
        }

        public async Task GetTokenAsync() {
            string url = _baseUrl + "getToken?sys=web&usr=TESTING&pwd=API-TEST";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(url));
            request.ContentType = "application/json";
            request.Method = "GET";

            using (WebResponse response = await request.GetResponseAsync()) {
                using (Stream stream = response.GetResponseStream()) {
                    using (var streamReader = new StreamReader(stream)) {
                        _token = JsonConvert.DeserializeObject<string>(streamReader.ReadToEnd());
                    }
                }
            }
        }

        public async Task<ProviderDrugInfo> ProviderSDLAsync(string medicationId) {
            string url = _baseUrl + $"ProviderSDL?lst={medicationId}";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(url));
            request.ContentType = "application/json";
            request.Method = "GET";
            WebHeaderCollection header = new WebHeaderCollection {
                { "Token", _token }
            };
            request.Headers = header;

            using (WebResponse response = await request.GetResponseAsync()) {
                using (Stream stream = response.GetResponseStream()) {
                    using (var streamReader = new StreamReader(stream)) {
                        return JsonConvert.DeserializeObject<ProviderDrugInfo>(streamReader.ReadToEnd());
                    }
                }
            }
        }

        public async Task<Trade> SearchTradeAsync(string searchTerm) {
            string url = _baseUrl + $"searchTrade?med={searchTerm}";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(url));
            request.ContentType = "application/json";
            request.Method = "GET";
            WebHeaderCollection header = new WebHeaderCollection {
                { "Token", _token }
            };
            request.Headers = header;

            using (WebResponse response = await request.GetResponseAsync()) {
                using (Stream stream = response.GetResponseStream()) {
                    using (var streamReader = new StreamReader(stream)) {
                        return JsonConvert.DeserializeObject<Trade>(streamReader.ReadToEnd());
                    }
                }
            }
        }
    }
}
