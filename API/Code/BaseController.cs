using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace API.Code
{
    [ValidateModel]
    [Route("api/[controller]/[action]")]
    [Produces("application/json")]

    public class BaseController<T> : ControllerBase where T : BaseController<T>
    {
        public BaseController()
        {

        }

        public long CurrentUserID
        {
            get
            {
                return long.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "userID").Value);
            }

        }

        private string GetClaim(string ClaimName)
        {
            return HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimName).Value;
        }
    }
}
