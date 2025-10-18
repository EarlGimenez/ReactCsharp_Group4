using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using System;

namespace ASI.Basecode.WebApp.Authentication
{
    /// 
    /// Adds a token generation endpoint to an application pipeline.
    /// 
    public static class TokenProviderAppBuilderExtensions
    {
        /// 
        /// Adds the TokenProviderMiddleware"/> middleware to the specified IApplicationBuilder"/>, which enables token generation capabilities.
        /// <param name="app">The IApplicationBuilder"/> to add the middleware to.</param>
        /// <param name="options">A  TokenProviderOptions"/> that specifies options for the middleware.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// 
        public static IApplicationBuilder UseTokenProvider(this IApplicationBuilder app, TokenProviderOptions options)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return app.UseMiddleware<TokenProviderMiddleware>(Options.Create(options));
        }
    }
}
