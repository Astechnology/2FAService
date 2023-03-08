using _2FAService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace _2FAService.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        [Authorize]
        [HttpGet]
        [Route("[action]")]
        public IActionResult Welcome()
        {
            var data = this.User?.Claims.FirstOrDefault(u => u.Type == ClaimTypes.UserData)?.Value;
            if (string.IsNullOrEmpty(data))
            {
                return Ok(new WelcomeMessage
                {
                    Name = "",
                    Message = "Welcome to your page"
                });
            }
            else
            {
                var model = JsonConvert.DeserializeObject<AuthResponseModel>(data);
                if (model != null)
                {
                    return Ok(new WelcomeMessage
                    { Name = model.PhoneNumber, Message = $"Welcome dear {model.PhoneNumber}" });
                }
                return Ok(new WelcomeMessage
                {
                    Name = "",
                    Message = "Welcome to your page"
                });

            }

        }
    }
}
