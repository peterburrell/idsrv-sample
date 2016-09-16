using System.Security.Claims;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class UsersController : ApiController
    {
        public IHttpActionResult Get()
        {
            var user = (ClaimsPrincipal) User;

            var subject = user.FindFirst("sub").Value;
            var client = user.FindFirst("client_id").Value;

            return Json(new 
            {
                client,
                subject
            });
        }
    }
}
