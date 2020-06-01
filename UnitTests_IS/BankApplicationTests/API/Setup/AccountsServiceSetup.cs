using BankApplication.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BankApplicationTests.API.Setup
{
    public class AccountsServiceSetup
    {
        private static HttpClient httpClient;
        private const string Url = "https://localhost:44370/api/Accounts/";

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
    }
}
