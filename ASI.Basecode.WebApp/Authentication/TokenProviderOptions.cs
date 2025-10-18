using Microsoft.IdentityModel.Tokens;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ASI.Basecode.WebApp.Authentication
{
    /// 
    /// Token provider
    /// 
    public class TokenProviderOptions
    {
        /// 
        /// Gets or sets the path for token generation.
        /// 
        public string Path { get; set; } = "api/token";
        /// 
        /// Gets or sets the issuer.
        /// 
        public string Issuer { get; set; }
        /// 
        /// Gets or sets the audience.
        /// 
        public string Audience { get; set; }
        /// 
        /// Gets or sets the expiration.
        /// 
        public TimeSpan Expiration { get; set; } = TimeSpan.FromMinutes(5);
        /// 
        /// Gets or sets the signing credentials.
        /// 
        public SigningCredentials SigningCredentials { get; set; }
        /// 
        /// Gets or sets the identity resolver.
        /// 
        public Func<string, string, Task<ClaimsIdentity>> IdentityResolver { get; set; }
        /// 
        /// Gets or sets the nonce generator.
        /// 
        public Func<Task<string>> NonceGenerator { get; set; }
          = () => Task.FromResult(Guid.NewGuid().ToString());
    }
}
