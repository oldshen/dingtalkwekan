using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Wekan.SDK.Api.Impl
{
    internal class WekanApi:IWekanApi
    {

        const string registerUrl = "/users/register";
        const string loginUrl = "/users/login";

        string wekanRoot;

        private readonly IConfiguration configuration;

        private readonly  IHttpClientFactory httpClientFactory;
        private readonly ILogger<WekanApi> logger;

        public WekanApi(IConfiguration configuration, IHttpClientFactory httpClientFactory,ILogger<WekanApi> logger)
        {
            this.configuration = configuration;
            this.httpClientFactory = httpClientFactory;
            this.logger = logger;
            wekanRoot = configuration["WekanRoot"];
        }


        public LoginResult Login(string username, string password)
        {
            HttpClient httpClient = httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(wekanRoot);

            HttpContent httpContent = new FormUrlEncodedContent(
                new Dictionary<string, string>()
                {
                    {"username",username},
                    {"password",password}
                }
            );

            var response = httpClient.PostAsync(loginUrl, httpContent).Result;
            var result= response.Content.ReadAsStringAsync().Result;
            IDictionary<string, object> res = JsonSerializer.Deserialize<IDictionary<string, object>>(result);
            if (res.ContainsKey("error"))
            {
                logger.LogInformation(res["reason"].ToString());
                return null;
            }
            return new LoginResult(res);
        }

        public bool Register(string username, string password, string email)
        {
            HttpClient httpClient = httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(wekanRoot);

            HttpContent httpContent = new FormUrlEncodedContent(
                new Dictionary<string, string>()
                {
                    {"username",username},
                    {"password",password},
                    {"email",email }
                }
            );

            var response = httpClient.PostAsync(registerUrl, httpContent).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            IDictionary<string, object> res = JsonSerializer.Deserialize<IDictionary<string, object>>(result);
            if (res.ContainsKey("error"))
            {
                logger.LogInformation(res["reason"].ToString());
                return false;
            }
            return true;
        }
    }
}
