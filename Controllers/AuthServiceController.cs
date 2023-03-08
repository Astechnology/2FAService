using _2FAService.Extensions;
using _2FAService.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace _2FAService.Controllers
{
    /// <summary>
    ///  The 2FAService Authentication
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthServiceController : ControllerBase
    {
        private IService2FASetting _setting;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="setting"></param>
        public AuthServiceController(IService2FASetting setting)
        {
            _setting = setting;
        }


        /// <summary>
        /// Authenticate the Request with the provided Data
        /// </summary>
        /// <param name="model">the data to be authenticated</param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthResponseModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorMessage))]
        public IActionResult Authenticate(AuthRequestModel model)
        {
            var entry = MyCustomStorage.Instance().GetEntry(model.PhoneNumber);
            if (entry == null)
            {

                return Ok(new ErrorMessage
                {

                    Message = $"Can't find the Entry with the phone number ({model.PhoneNumber}) ",
                    Title = "Phone Number not found",

                });
            }

            // the record have been found by Phone number
            // verify if the code match
            if (entry.AuthCode.Equals(model.AuthCode, StringComparison.InvariantCultureIgnoreCase))
            {
                // check the validity period
                var validUntil = entry.CreatedOn + _setting.CodeValidityDuration;
                if (DateTime.Now > validUntil)
                {
                    return Ok(new ErrorMessage
                    {

                        Message = $"The data expired - the current delay is:{_setting.CodeValidityDuration} ",
                        Title = $"CODE Expired till {validUntil}",

                    });

                }


                //authorized the session 
                var claims = new[]
                {
                    new Claim(ClaimTypes.MobilePhone,entry.PhoneNumber),
                    new Claim(ClaimTypes.UserData,JsonConvert.SerializeObject(entry))
                };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                Microsoft.AspNetCore.Authentication.AuthenticationHttpContextExtensions.SignInAsync(this.HttpContext,
                    CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

                // response
                return Ok(new AuthResponseModel
                {
                    IsSuccess = true,
                    Message = "You are authorized",
                    PhoneNumber = model.PhoneNumber,
                    ProvidedCode = model.AuthCode
                });
            }
            else
            {
                return Ok(new ErrorMessage
                {

                    Message = $"Can't find the Entry with the data ({model.PhoneNumber}, {model.AuthCode}) ",
                    Title = "Auth Error",

                });
            }

        }


        /// <summary>
        /// Generate the Code for a further Authentication 
        /// </summary>
        /// <param name="phoneNumber">the phone number</param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorMessage))]
        public IActionResult GenerateAuthCode(string phoneNumber)
        {
            var existingModel = MyCustomStorage.Instance().GetEntry(phoneNumber);
            if (existingModel == null)
            {
                // first request to the authentication
                var code = CodeGenerator.SmartAuthCode(_setting.CodeNumbersCount, _setting.NumberOfLettersInCode);
                var model = new AuthModel
                {
                    PhoneNumber = phoneNumber,
                    AuthCode = code,
                    CurrentCodeRequestCount = 1,
                    CreatedOn = DateTime.Now
                };

                MyCustomStorage.Instance().SaveEntry(model);
                return Ok(model);

            }
            else
            {
                if (existingModel.CurrentCodeRequestCount > this._setting.NumberOfCodeRequestPerNumber)
                {
                    // return BadRequest()
                    return Ok(new ErrorMessage
                    {
                        Title = "Too much requests",
                        Message = $"You Have reached the Maximum of request:({_setting.NumberOfCodeRequestPerNumber}) please contact the administrator"
                    });
                }
                else
                {
                    existingModel.CurrentCodeRequestCount += 1;
                    existingModel.AuthCode = CodeGenerator.SmartAuthCode(_setting.CodeNumbersCount, _setting.NumberOfLettersInCode);
                    existingModel.CreatedOn = DateTime.Now;
                    MyCustomStorage.Instance().SaveEntry(existingModel);

                    return Ok(existingModel);
                }
            }

        }
    }
}
