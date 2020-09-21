using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace RegistrationAndLogin.Infrastructure
{
    public class AppConfig
    {
        public static string WebApiEndPointAddress
        {
            get
            {
                const string webConfigSetting = "scoopen.WebApi.EndPoint.Address";
                var address = ConfigurationManager.AppSettings["ApiAddress"];
                if (address == null)
                    throw new ConfigurationErrorsException(string.Format("{0} should be defined in the web.config file", webConfigSetting));

                return address;
            }
        }

        public static bool WebApiUsesWindowsAuthentication
        {
            get
            {
                const string webConfigSetting = "scoopen.WebApi.Use.Windows.Authentication";

                var settingValue = ConfigurationManager.AppSettings["Authentication"];
                if (settingValue == null)
                    throw new ConfigurationErrorsException(string.Format("{0} should be defined in the web.config file", webConfigSetting));

                try
                {
                    var useWindowsAuthentication = Convert.ToBoolean(settingValue);
                    return useWindowsAuthentication;
                }
                catch (FormatException)
                {
                    throw new ConfigurationErrorsException(string.Format("{0} should be true or false", webConfigSetting));
                }
            }
        }

        public static TimeSpan ClientTimeOut
        {
            get
            {
                const string webConfigSetting = "scoopen.WebApi.EndPoint.ClientTimeOut";
                TimeSpan ClientTimeOut = TimeSpan.Parse(ConfigurationManager.AppSettings["ClientTimeOut"]);
                if (ClientTimeOut == null)
                    throw new ConfigurationErrorsException(string.Format("{0} should be defined in the web.config file", webConfigSetting));

                return ClientTimeOut;
            }
        }
    }

    public abstract class Constants
    {
        public static string JsonHttpHeader = "application/json";
    }
}