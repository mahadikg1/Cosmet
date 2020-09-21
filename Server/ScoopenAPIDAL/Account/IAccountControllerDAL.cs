using ScoopenAPIModals.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoopenAPIDAL
{
    public interface IAccountControllerDAL
    {
        int RegisterUser(string firstName, string lastName, string mobile, string email, string otp);

        int ActivateRegisteredUser(string mobile, string password, string email, string otp);

        void SaveOtpInDatabase(string mobile, string email, string otp);

        string GetOtpFromDatabase(string mobile, string email);

        User Authenticate(string username, string password);

        int ChangePasswordOnFirstLogin(string userName, string currentPassword, string newPassword);
    }
}
