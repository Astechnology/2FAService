namespace _2FAService
{
    public class AuthModel : IComparable<AuthModel>
    {
        /// <summary>
        /// the phone number for the the authentication proccess
        /// </summary>
        public string PhoneNumber { get; set; } = "";

        /// <summary>
        /// the Generated Code for the number 
        /// </summary>
        public string AuthCode { get; set; } = "";

        /// <summary>
        /// the date when the code have been generated
        /// </summary>
        public DateTime CreatedOn { get; set; }

        public int CurrentCodeRequestCount { get; set; }

        public int CompareTo(AuthModel other)
        {
            return this.PhoneNumber.CompareTo(other.PhoneNumber);
        }
    }
}
