using ASI.Basecode.Services.Interfaces;
using ASI.Basecode.Services.ServiceModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace ASI.Basecode.WebApp.Controllers
{
    /// 
    /// Contact form API controller
    /// 
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowReactApp")]
    public class ContactController : ControllerBase
    {
        private readonly IEmailService _emailService;
        private readonly ILogger<ContactController> _logger;
        private readonly EmailSettings _emailSettings;

        public ContactController(
            IEmailService emailService, 
            ILogger<ContactController> logger,
            IOptions<EmailSettings> emailSettings)
        {
            _emailService = emailService;
            _logger = logger;
            _emailSettings = emailSettings.Value;
        }

        /// 
        /// Submit contact form
        /// POST /api/contact
        /// 
        /// <param name="contactForm">Contact form data</param>
        /// <returns>Contact response</returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<ContactResponseViewModel>> SubmitContactForm([FromBody] ContactFormViewModel contactForm)
        {
            try
            {
                _logger.LogInformation($"Contact form submission received from {contactForm?.Email}");

                if (contactForm == null)
                {
                    _logger.LogWarning("Contact form data is null");
                    return BadRequest(new ContactResponseViewModel
                    {
                        Success = false,
                        Message = "Invalid contact form data"
                    });
                }

                // Validate model
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Contact form validation failed");
                    return BadRequest(new ContactResponseViewModel
                    {
                        Success = false,
                        Message = "Please fill in all required fields"
                    });
                }

                // Send email
                await _emailService.SendContactEmailAsync(contactForm);

                _logger.LogInformation($"Contact email sent successfully from {contactForm.Email}");

                return Ok(new ContactResponseViewModel
                {
                    Success = true,
                    Message = "Thank you for contacting us! We will get back to you soon."
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error processing contact form from {contactForm?.Email}");
                _logger.LogError($"Exception type: {ex.GetType().Name}");
                _logger.LogError($"Exception message: {ex.Message}");
                
                if (ex.InnerException != null)
                {
                    _logger.LogError($"Inner exception: {ex.InnerException.Message}");
                }

                return StatusCode(500, new ContactResponseViewModel
                {
                    Success = false,
                    Message = $"An error occurred: {ex.Message}"
                });
            }
        }

        /// 
        /// Test endpoint to verify API and configuration
        /// GET /api/contact/test
        /// 
        [HttpGet("test")]
        [AllowAnonymous]
        public ActionResult<object> TestEndpoint()
        {
            _logger.LogInformation("Contact API test endpoint called");
            
            return Ok(new
            {
                success = true,
                message = "Contact API is working!",
                timestamp = DateTime.Now,
                configuration = new
                {
                    smtpServer = _emailSettings.SmtpServer,
                    smtpPort = _emailSettings.SmtpPort,
                    senderEmail = _emailSettings.SenderEmail,
                    recipientEmail = _emailSettings.RecipientEmail,
                    senderName = _emailSettings.SenderName,
                    enableSsl = _emailSettings.EnableSsl,
                    usernameConfigured = !string.IsNullOrEmpty(_emailSettings.Username),
                    passwordConfigured = !string.IsNullOrEmpty(_emailSettings.Password)
                }
            });
        }
    }
}
