using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScoopenAPIModals.Notifications;
using System.Net.Http;
using ScoopenAPIModals;
using System.Net.Http.Formatting;
using ScoopenAPIBLL.Utility;

namespace ScoopenNotifications
{
    public class SMSNotifications : INotifications
    {
        private string _apiKey = GlobalConstants.SmsGatewayApiKey;
        private string _apiurl = GlobalConstants.SmsGatewayApiUrl;

        public OtpResponse SendOTP(OtpRequest request)
        {
            OtpResponse otpresponse = new OtpResponse();

            using (var client = new HttpClient())
            {
                string url = _apiurl + _apiKey + "/SMS/+91" + request.Mobile + "/" + request.Otp;

                client.BaseAddress = new Uri(url);

                var responseTask = client.GetAsync("");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<OtpResponse>();
                    readTask.Wait();

                    otpresponse = readTask.Result;
                }
            }

            return otpresponse;
        }

        public OtpResponse VerifyOTP(OtpRequest request)
        {
            OtpResponse otpresponse = new OtpResponse();

            using (var client = new HttpClient())
            {
                string url = _apiurl + _apiKey + "/SMS/VERIFY/" + request.SessionId + "/" + request.Otp;

                client.BaseAddress = new Uri(url);

                var responseTask = client.GetAsync("");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<OtpResponse>();
                    readTask.Wait();

                    otpresponse = readTask.Result;
                }
            }

            return otpresponse;
        }

        public OtpResponse SendLoginDetails(LoginDetails request)
        {
            OtpResponse otpresponse = new OtpResponse();

            using (var client = new HttpClient())
            {
                string url = _apiurl + _apiKey + "/ADDON_SERVICES/SEND/TSMS";

                client.BaseAddress = new Uri(url);

                var responseTask = client.PostAsync<LoginDetails>("", request, new JsonMediaTypeFormatter(), null);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<OtpResponse>();
                    readTask.Wait();

                    otpresponse = readTask.Result;
                }
            }

            return otpresponse;
        }
    }
}
