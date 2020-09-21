using RegistrationAndLogin.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace RegistrationAndLogin.Service_References
{
    public abstract class BaseHttpClient
    {
        protected HttpClient ServiceClient;

        protected BaseHttpClient()
        {
            ServiceClient = Create();
        }

        public static HttpClient Create()
        {
            var handler = new WebRequestHandler
            {
                AllowAutoRedirect = false,
                UseProxy = false,
                Credentials = CredentialCache.DefaultNetworkCredentials
            };

            var client = AppConfig.WebApiUsesWindowsAuthentication
                ? new HttpClient(handler)
                : new HttpClient();

            client.BaseAddress = new Uri(AppConfig.WebApiEndPointAddress);

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(Constants.JsonHttpHeader));
            client.Timeout = AppConfig.ClientTimeOut;

            return client;
        }
    }
}