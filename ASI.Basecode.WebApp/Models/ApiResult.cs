using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using static ASI.Basecode.Resources.Constants.Enums;

namespace ASI.Basecode.WebApp.Models
{
    /// 
    /// ApiResult
    /// 
    /// <typeparam name="T"></typeparam>
    public class ApiResult<T>
    {
        /// 
        /// Gets or sets the response.
        /// 
        public object Response { get; set; }
        /// 
        /// Gets or sets the status.
        /// 
        [JsonConverter(typeof(StringEnumConverter))]
        public Status Status { get; set; }
        /// 
        /// Gets or sets the message.
        /// 
        public string Message { get; set; }
        /// 
        /// Gets or sets the name.
        /// 
        public string Name { get; set; }
        /// 
        /// Gets or sets the data.
        /// 
        public byte[] data { get; set; }

        /// 
        /// Initializes a new instance of the ApiResult{T} class.
        /// 
        /// <param name="status">The status.</param>
        /// <param name="model">The model.</param>
        /// <param name="message">The message.</param>
        public ApiResult(Status status, object model, string message)
        {
            this.Status = status;
            this.Response = model;
            this.Message = message;
        }

        /// 
        /// Initializes a new instance of the ApiResult{T} class.
        /// 
        /// <param name="status">The status.</param>
        /// <param name="data">The data.</param>
        /// <param name="message">The message.</param>
        public ApiResult(Status status, byte[] data, string message)
        {
            this.Status = status;
            this.data = data;
            this.Message = message;
        }

        /// 
        /// Initializes a new instance of the ApiResult{T} class.
        /// 
        /// <param name="status">The status.</param>
        /// <param name="name">The name.</param>
        /// <param name="model">The model.</param>
        /// <param name="message">The message.</param>
        public ApiResult(Status status, string name, object model, string message)
        {
            this.Status = status;
            this.Response = model;
            this.Message = message;
            this.Name = name;
        }

        /// 
        /// Initializes a new instance of the ApiResult{T} class.
        /// 
        /// <param name="status">The status.</param>
        /// <param name="message">The message.</param>
        public ApiResult(Status status, string message)
        {
            this.Status = status;
            this.Message = message;
        }

        /// 
        /// Initializes a new instance of the ApiResult{T} class.
        /// 
        /// <param name="status">The status.</param>
        /// <param name="model">The model.</param>
        /// <param name="message">The message.</param>
        public ApiResult(Status status, T model, string message)
        {
            this.Status = status;
            this.Response = model;
            this.Message = message;
        }

        /// 
        /// Creates the API success response.
        /// 
        /// <param name="model">The model.</param>
        /// <param name="message">The message.</param>
        /// <returns>ApiResult object</returns>
        public static ApiResult<T> CreateSuccess(T model, string message)
        {
            return new ApiResult<T>(Status.Success, model, message);
        }

        /// 
        /// Creates the API success response.
        /// 
        /// <param name="model">The model.</param>
        /// <param name="message">The message.</param>
        /// <returns>ApiResult object</returns>
        public static ApiResult<object> CreateSuccess(object model, string message)
        {
            return new ApiResult<object>(Status.Success, model, message);
        }

        /// 
        /// Creates the API success response.
        /// 
        /// <param name="name">The name.</param>
        /// <param name="model">The model.</param>
        /// <param name="message">The message.</param>
        /// <returns>ApiResult object</returns>
        public static ApiResult<object> CreateSuccess(string name, object model, string message)
        {
            return new ApiResult<object>(Status.Success, name, model, message);
        }

        /// 
        /// Creates the API success response.
        /// 
        /// <param name="message">The message.</param>
        /// <returns>ApiResult object</returns>
        public static ApiResult<object> CreateSuccess(string message)
        {
            return new ApiResult<object>(Status.Success, message);
        }

        /// 
        /// Creates the API success response.
        /// 
        /// <param name="data">The data.</param>
        /// <param name="message">The message.</param>
        /// <returns>ApiResult object</returns>
        public static ApiResult<T> CreateSuccess(byte[] data, string message)
        {
            return new ApiResult<T>(Status.Success, data, message);
        }

        /// 
        /// Creates the API error response.
        /// 
        /// <param name="message">The message.</param>
        /// <returns>ApiResult object</returns>
        public static ApiResult<T> CreateError(string message)
        {
            return new ApiResult<T>(Status.Error, message);
        }
        public static ApiResult<T> CreateError(Status status, string message)
        {
            return new ApiResult<T>(status, message);
        }
    }
}
