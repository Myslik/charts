using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Notino.Charts.Web.Controllers
{
    [Route("Account")]
    public class AccountController : Controller
    {
        [HttpGet("Login")]
        public async Task Login()
        {
            var result = await HttpContext.AuthenticateAsync("oidc");
            if (result.Succeeded)
            {
                Response.Redirect("/");
            }
            else
            {
                await HttpContext.ChallengeAsync("oidc");
            }
        }

        [HttpPost("Logout")]
        public async Task Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignOutAsync("oidc");
        }
    }
}