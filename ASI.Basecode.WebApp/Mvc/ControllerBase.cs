using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Security.Claims;
using System.Text;

namespace ASI.Basecode.WebApp.Mvc
{
    /// 
    /// Declare ControllerBase.
    /// 
    public class ControllerBase<TController> : Controller where TController : class
    {
        /// AppConfiguration
        protected readonly IConfiguration _configuration;

        /// HttpContextAccessor
        protected readonly IHttpContextAccessor _httpContextAccessor;

        /// Logger
        protected ILogger _logger;

        /// Session
        protected ISession _session => _httpContextAccessor.HttpContext.Session;

        /// 
        /// Initializes a new instance of the ControllerBase{TController} class.
        /// 
        /// <param name="httpContextAccessor">HTTP context accessor</param>
        /// <param name="localizer">Localizer</param>
        /// <param name="loggerFactory">Logger factory</param>
        /// <param name="configuration">Configuration</param>
        /// <param name="mapper">Mapper</param>
        public ControllerBase(
                                IHttpContextAccessor httpContextAccessor,
                                ILoggerFactory loggerFactory,
                                IConfiguration configuration,
                                IMapper mapper = null)
        {
            this._httpContextAccessor = httpContextAccessor;
            this._configuration = configuration;
            this._logger = loggerFactory.CreateLogger<TController>();
            this._configuration = configuration;
            this._mapper = mapper;
        }

        /// Mapper
        protected IMapper _mapper { get; set; }

        /// 
        /// Get UserId.
        /// 
        public string UserId
        {
            get { return User.FindFirst(ClaimTypes.NameIdentifier).Value; }
        }

        /// 
        /// Get UserName.
        /// 
        public string UserName
        {
            get { return User.Identity.Name; }
        }

        /// 
        /// Get Role.
        /// 
        public string Supervisor
        {
            get { return User.FindFirst(ClaimTypes.Role).Value; }
        }

        /// 
        /// Get ClientId.
        /// 
        public string ClientId
        {
            get { return User.FindFirst("ClientId").Value; }
        }

        /// 
        /// Get ClientSystemId
        /// 
        public string ClientSystemId
        {
            get { return User.FindFirst("ClientSystemId").Value; }
        }

        /// 
        /// Get ClientSystemName
        /// 
        public string ClientSystemName
        {
            get { return User.FindFirst("ClientSystemName").Value; }
        }

        /// 
        /// Get ClientUserRole.
        /// 
        public string ClientUserRole
        {
            get { return User.FindFirst("ClientUserRole").Value; }
        }

        /// 
        /// Return filter default if expiration session.
        /// 
        /// <param name="context">context</param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
        }

        /// 
        /// OnActionExecuted.
        /// 
        /// <param name="context">context</param>
        public override void OnActionExecuted(ActionExecutedContext context)
        {
        }

        /// 
        /// Write Log on Exception 
        /// 
        protected void HandleExceptionLog(Exception ex, string request)
        {
            string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            string actionMethod = this.ControllerContext.RouteData.Values["action"].ToString();

            StringBuilder logContent = new StringBuilder();
            logContent.AppendLine($"\n======================================== start ========================================");
            logContent.AppendLine($"■ API Controller Name: \n\t{controllerName}");
            logContent.AppendLine($"■ API Action Method: \n\t{actionMethod}");
            logContent.AppendLine($"■ API Request Model: \n\t{request}");
            logContent.AppendLine($"■ Exception Message: \n\t{ex.Message}");
            logContent.AppendLine($"■ Exception StackTrace: \n\t{ex.StackTrace}");
            logContent.AppendLine($"========================================= end =========================================\r\n");

            this._logger.LogError(logContent.ToString());
        }
    }
}
