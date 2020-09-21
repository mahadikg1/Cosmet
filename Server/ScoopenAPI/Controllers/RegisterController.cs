using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ScoopenAPIBLL;
using ScoopenAPIModals.Account;
using ScoopenAPIModals.Notifications;
using ScoopenAPIBLL.Utility;
using ScoopenNotifications;
using System.Web.Helpers;
using ScoopenAPIDAL;

namespace APIAuthentication.Controllers
{
    public class RegisterController : ApiController
    {
        // Uncomment below line if you want to use SMS Notifications
        //SMSNotifications notification = new SMSNotifications();
        EmailNotifications emailNotification = new EmailNotifications();

        AccountControllerBLL bll = new AccountControllerBLL(new AccountControllerDAL());

        [HttpPost]
        public HttpResponseMessage RegisterUser([FromBody] UserInfo userInfo)
        {
            try
            {
                if (userInfo != null)
                {
                    string otp = new LoginHelper().GenerateRandomOtp();

                    int result = bll.RegisterUser(userInfo.FirstName, userInfo.LastName, userInfo.Mobile, userInfo.Email, otp);

                    OtpRequest request = new OtpRequest() { Mobile = userInfo.Mobile, Email = userInfo.Email, Otp = otp };

                    // Uncomment below line if you want to send sms with otp
                    //OtpResponse response = notification.SendOTP(request);
                    OtpResponse emailresponse = emailNotification.SendOTP(request);

                    bll.SaveOtpInDatabase(userInfo.Mobile, userInfo.Email, otp);

                    return Request.CreateResponse(HttpStatusCode.OK, emailresponse);
                }

                return Request.CreateResponse(HttpStatusCode.BadRequest, "");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPut]
        public HttpResponseMessage ActivateRegisteredUser([FromBody] OtpRequest request)
        {
            try
            {
                if (request != null)
                {
                    // Uncomment below line if you want to verify otp sent over sms
                    //OtpResponse smsResponse = notification.VerifyOTP(request);
                    OtpResponse emailResponse = emailNotification.VerifyOTP(request);

                    if (emailResponse.Status == "Success")
                    {

                        string password = new LoginHelper().GeneratePassword(8);

                        int result = bll.ActivateRegisteredUser(request.Mobile, password, request.Email, request.Otp);

                        // Uncomment below lines if you want to send first time login details over sms

                        //LoginDetails req = new LoginDetails()
                        //{
                        //    From = "VHAASH",
                        //    To = request.Mobile,
                        //    TemplateName = "ScoopenLoginAccount",
                        //    VAR1 = "User",
                        //    VAR2 = request.Mobile,
                        //    VAR3 = password
                        //};

                        //OtpResponse smsResponseLoginDetails = notification.SendLoginDetails(req);

                        LoginDetails emailreq = new LoginDetails()
                        {
                            From = "VHAASH",
                            To = request.Email,
                            TemplateName = "ScoopenLoginAccount",
                            VAR1 = "User",
                            VAR2 = request.Mobile,
                            VAR3 = password
                        };

                        OtpResponse emailResponseLoginDetails = emailNotification.SendLoginDetails(emailreq);

                        return Request.CreateResponse(HttpStatusCode.OK, emailResponseLoginDetails);
                    }

                    return Request.CreateErrorResponse(HttpStatusCode.NonAuthoritativeInformation, "OTP Not Matched");
                }

                return Request.CreateResponse(HttpStatusCode.BadRequest, "");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}
