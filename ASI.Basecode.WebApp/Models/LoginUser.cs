using ASI.Basecode.Data.Models;
using static ASI.Basecode.Resources.Constants.Enums;

namespace ASI.Basecode.WebApp.Models
{
    /// 
    /// Login User Model
    /// 
    public class LoginUser
    {
        /// 
        /// Login Result
        /// 
        public LoginResult loginResult { get; set; }
        /// 
        /// Message
        /// 
        public string message { get; set; }
        /// 
        /// Access Token
        /// 
        public string access_token { get; set; }
        /// 
        /// Expires In
        /// 
        public int expires_in { get; set; }
        /// 
        /// User Data
        /// 
        public User userData { get; set; }
    }
}
