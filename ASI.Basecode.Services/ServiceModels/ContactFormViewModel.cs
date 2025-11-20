using System.ComponentModel.DataAnnotations;

namespace ASI.Basecode.Services.ServiceModels
{
    /// 
    /// Contact form submission model
    /// 
    public class ContactFormViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Subject is required")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Message is required")]
        public string Message { get; set; }
    }

    /// 
    /// Contact form response model
    /// 
    public class ContactResponseViewModel
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
