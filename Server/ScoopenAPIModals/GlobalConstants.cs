using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ScoopenAPIModals
{
    public static class GlobalConstants
    {
        public static string SmsGatewayApiUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["SmsGatewayApiUrl"].ToString();
            }
        }

        public static string SmsGatewayApiKey
        {
            get
            {
                return ConfigurationManager.AppSettings["SmSGatewayApiKey"].ToString();
            }
        }
        public static string FromEmail
        {
            get
            {
                return ConfigurationManager.AppSettings["FromEmail"];
            }
        }

        public static string FromEmailPasword
        {
            get
            {
                return ConfigurationManager.AppSettings["FromEmailPassword"];
            }
        }

        public static string JwtTokenIssuer
        {
            get
            {
                return ConfigurationManager.AppSettings["JwtTokenIssuer"].ToString();
            }
        }
    }
}
