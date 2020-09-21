using ScoopenAPIBLL;
using ScoopenAPIDAL;
using ScoopenAPIModals.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ScoopenAPI.Controllers
{
    public class PasswordManageController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage ChangePasswordOnFirstLogin([FromBody] LoginRequest login)
        {
            AccountControllerBLL bll = new AccountControllerBLL(new AccountControllerDAL());
            int result = bll.ChangePasswordOnFirstLogin(login.Username, login.Password, login.Password);

            if (result == 1)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Password changed successful");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
    }
}
