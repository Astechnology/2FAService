namespace _2FAService.Models
{
    public class AuthRequestModel
    {
        /// <summary>
        /// the phone number for the the authentication proccess
        /// </summary>
        public string PhoneNumber { get; set; } = "";

        /// <summary>
        /// the Received Code for the number 
        /// </summary>
        public string AuthCode { get; set; } = "";
    }
}
