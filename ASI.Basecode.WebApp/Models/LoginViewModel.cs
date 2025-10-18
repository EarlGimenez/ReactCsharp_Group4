using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ASI.Basecode.WebApp.Models
{
    /// 
    /// Login View Model
    /// 
    public class LoginViewModel
    {
        /// ユーザーID
        [JsonPropertyName("userId")]
        [Required(ErrorMessage = "UserId is required.")]
        public string UserId { get; set; }
        /// パスワード
        [JsonPropertyName("password")]
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
    }
}
