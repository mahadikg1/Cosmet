using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoopenAPIDAL
{
    public static class ScoopenDB
    {
        public static string spRegisterUser = "spRegisterUser";
        public static string spActivateRegisteredUser = "spActivateRegisteredUser";
        public static string spSaveOtpInDatabase = "usp_SaveOtpInDatabase";
        public static string spGetOtpFromDatabase = "usp_GetOtpFromDatabase";
        public static string spAuthenticateUser = "spAuthenticateUser";
        public static string spChangePasswordOnFirstLogin = "usp_ChangePasswordOnFirstLogin";
    }
}
