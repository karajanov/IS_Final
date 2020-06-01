using BankApplication.Data.DTOs;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BankApplicationTests.API.Setup
{
    public class AccountsServiceSetup
    {
        private static HttpClient httpClient;
        private const string Url = "https://localhost:5001/api/Accounts/";

        public AccountsServiceSetup()
        {
            httpClient = new HttpClient { BaseAddress = new Uri(Url) };
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<HttpResponseMessage> GetAsync(string path)
        {
            return await httpClient.GetAsync(path);
        }

        public async Task<HttpResponseMessage> PostAsync(string path, AccountDTO accountDto)
        {
            var json = JsonConvert.SerializeObject(accountDto);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            
            return await httpClient.PostAsync(path, data);
        }

        public async Task<HttpResponseMessage> PutAsync(string path, AccountDTO accountDto)
        {
            var json = JsonConvert.SerializeObject(accountDto);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            return await httpClient.PutAsync(path, data);
        }

        public async Task<HttpResponseMessage> DeleteAsync(string path)
        {
            return await httpClient.DeleteAsync(path);
        }
    }
}
