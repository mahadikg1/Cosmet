using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScoopenAPIModals.Notifications;
using System.Net.Mail;
using System.Net;
using System.Configuration;
using ScoopenAPIModals;
using ScoopenAPIBLL.Utility;
using ScoopenAPIBLL;
using ScoopenAPIDAL;

namespace ScoopenNotifications
{
    public class EmailNotifications : INotifications
    {
        public OtpResponse SendOTP(OtpRequest request)
        {
            OtpResponse response = new OtpResponse();
            try
            {
                string fromEmail = GlobalConstants.FromEmail;
                string fromEmailPassword = GlobalConstants.FromEmailPasword;

                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(fromEmail);
                message.To.Add(new MailAddress(request.Email));
                message.Subject = "Test : OTP for registration";
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = "<h2>Please use below otp to confirm your registration.</h2> <br/> Your Otp Is: " + request.Otp;
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com"; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(fromEmail, fromEmailPassword);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);

                response.Status = "Success";
                response.Details = "Email sent successfully";
            }
            catch (Exception)
            {
                response.Status = "Failed";
                response.Details = "Email sent failed";
            }
            return response;
        }

        public OtpResponse VerifyOTP(OtpRequest request)
        {
            AccountControllerBLL bll = new AccountControllerBLL(new AccountControllerDAL());

            string otp = bll.GetOtpFromDatabase(request.Mobile, request.Email);

            if (otp == request.Otp)
            {
                return new OtpResponse() { Status = "Success", Details = "OTP Verified" };
            }
            return new OtpResponse() { Status = "Failed", Details = "Incorrect" };
        }

        public OtpResponse SendLoginDetails(LoginDetails request)
        {
            OtpResponse response = new OtpResponse();
            try
            {
                string fromEmail = GlobalConstants.FromEmail;
                string fromEmailPassword = GlobalConstants.FromEmailPasword;

                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(fromEmail);
                message.To.Add(new MailAddress(request.To));
                message.Subject = "First time login details";
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = "<h2>Please use below credentitals to login to system. Please change your password after login. </h2><br/> Username : " + request.VAR2 + "<br/>Password : " + request.VAR3;
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com"; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(fromEmail, fromEmailPassword);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);

                response.Status = "Success";
                response.Details = "Email sent successfully";
            }
            catch (Exception)
            {
                response.Status = "Failed";
                response.Details = "Email sent failed";
            }
            return response;
        }
    }
}
