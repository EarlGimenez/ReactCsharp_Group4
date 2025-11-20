using ASI.Basecode.Services.ServiceModels;
using System.Threading.Tasks;

namespace ASI.Basecode.Services.Interfaces
{
    /// 
    /// Email service interface for sending emails
    /// 
    public interface IEmailService
    {
        /// 
        /// Send contact form email
        /// 
        /// <param name="contactForm">Contact form data</param>
        /// <returns>Task</returns>
        Task SendContactEmailAsync(ContactFormViewModel contactForm);

        /// 
        /// Send generic email
        /// 
        /// <param name="toEmail">Recipient email</param>
        /// <param name="subject">Email subject</param>
        /// <param name="body">Email body (HTML)</param>
        /// <returns>Task</returns>
        Task SendEmailAsync(string toEmail, string subject, string body);
    }
}
