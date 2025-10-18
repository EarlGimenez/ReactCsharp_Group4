namespace ASI.Basecode.WebApp.Models
{
    /// 
    /// Token Authentication
    /// 
    public class TokenAuthentication
    {
        /// 
        /// Gets or sets the secret key.
        /// 
        public string SecretKey { get; set; }
        /// 
        /// Gets or sets the audience.
        /// 
        public string Audience { get; set; }
        /// 
        /// Gets or sets the token path.
        /// 
        public string TokenPath { get; set; }
        /// 
        /// Gets or sets the name of the cookie.
        /// 
        public string CookieName { get; set; }
        /// 
        /// Gets or sets the expiration minutes.
        /// 
        public int ExpirationMinutes { get; set; }
    }
}
