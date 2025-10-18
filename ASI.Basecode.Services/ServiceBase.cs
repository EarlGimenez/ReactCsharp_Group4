using Microsoft.Extensions.Logging;

namespace ASI.Basecode.Services
{
    /// 
    /// Service Base class implementation
    /// 
    public class ServiceBase
    {
        protected ILogger _logger;

        /// 
        /// Initializes a new instance of the ServiceBase class.
        /// 
        /// <param name="loggerFactory">Logger factory.</param>
        public ServiceBase(ILoggerFactory loggerFactory)
        {
            this._logger = loggerFactory.CreateLogger<ServiceBase>();
        }
    }
}
