namespace _2FAService.Models
{
    public class AuthResponseModel
    {
        /// <summary>
        /// the Number we have process the authentication for
        /// </summary>
        public string PhoneNumber { get; set; } = "";

        /// <summary>
        /// the Provided Code for authentication
        /// </summary>
        public string ProvidedCode { get; set; } = "";
        /// <summary>
        /// The state of the request : indicate if the authentification succeed or not
        /// </summary>
        public bool IsSuccess { get; set; } = false;

        /// <summary>
        /// the message after the verification of the rprovided code in the auth request
        /// </summary>
        public string Message { get; set; } = "";

    }
}
