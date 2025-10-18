using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ASI.Basecode.WebApp.Authentication
{
    /// 
    /// TokenProvider
    /// 
    public class TokenProvider
    {
        private readonly TokenProviderOptions _options;
        private readonly JsonSerializerSettings _serializerSettings;

        /// 
        /// Initializes a new instance of the TokenProvider class.
        /// 
        /// <param name="options">The options.</param>
        public TokenProvider(IOptions<TokenProviderOptions> options)
        {
            _options = options.Value;

            _serializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };
        }

        /// 
        /// Gets the JWT security token.
        /// 
        /// <param name="identity">The identity.</param>
        /// <param name="tokenProvider">The token provider.</param>
        /// <returns></returns>
        public string GetJwtSecurityToken(ClaimsIdentity identity, TokenProviderOptions tokenProvider)
        {
            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                                            issuer: tokenProvider.Issuer,
                                            audience: tokenProvider.Audience,
                                            claims: identity.Claims,
                                            notBefore: now,
                                            expires: now.Add(_options.Expiration),
                                            signingCredentials: tokenProvider.SigningCredentials);

            var encodedJwt = new JwtSecurityTokenHandler()
                            .WriteToken(jwt);

            return encodedJwt;
        }
    }
}
