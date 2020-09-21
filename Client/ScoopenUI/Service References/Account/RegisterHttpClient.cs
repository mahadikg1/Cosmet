﻿using ScoopenAPIModals.Notifications;
using ScoopenModals.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;

namespace RegistrationAndLogin.Service_References.Account
{
    public class RegisterHttpClient : BaseHttpClient, IRegisterHttpClient
    {
        public HttpResponseMessage RegisterUser(UserInfo userInfo)
        {
            try
            {
                using (ServiceClient)
                {
                    var resource = string.Format("api/register");

                    var response = ServiceClient.PostAsJsonAsync(resource, userInfo).Result;

                    if (response.IsSuccessStatusCode)
                        return response;

                    return new HttpResponseMessage(HttpStatusCode.BadRequest);
                }
            }
            catch (AggregateException e)
            {
                throw new Exception(e.Message);
            }
            
        }

        public HttpResponseMessage ActivateRegisteredUser(OtpRequest request)
        {
            using (ServiceClient)
            {
                var resource = string.Format("api/register");

                var response = ServiceClient.PutAsJsonAsync(resource, request).Result;

                if (response.IsSuccessStatusCode)
                    return response;

                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        public HttpResponseMessage Authenticate(LoginRequest login)
        {
            using (ServiceClient)
            {
                var resource = string.Format("api/login");

                var response = ServiceClient.PostAsJsonAsync(resource, login).Result;

                if (response.IsSuccessStatusCode)
                    return response;

                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        public HttpResponseMessage ChangePasswordOnFirstLogin(LoginRequest login)
        {
            using (ServiceClient)
            {
                var resource = string.Format("api/passwordmanage");

                var response = ServiceClient.PostAsJsonAsync(resource, login).Result;

                if (response.IsSuccessStatusCode)
                    return response;

                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }
    }
}