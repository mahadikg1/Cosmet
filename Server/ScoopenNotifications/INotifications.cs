using ScoopenAPIModals.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoopenNotifications
{
    public interface INotifications
    {
        OtpResponse SendOTP(OtpRequest request);
        OtpResponse VerifyOTP(OtpRequest request);
        OtpResponse SendLoginDetails(LoginDetails request);
    }
}
