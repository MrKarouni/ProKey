using System.Web.Http;
using ProKey.Web.JsonWebTokenConfig;

namespace JwtWithWebAPI.Controllers
{
    [JwtAuthorize]
    public class MyProtectedApiController : ApiController
    {
        public IHttpActionResult Get()
        {
            return Ok(new
            {
                Id = 1,
                Title = "Hello from My Protected Controller!",
                Username = this.User.Identity.Name
            });
        }
    }
}