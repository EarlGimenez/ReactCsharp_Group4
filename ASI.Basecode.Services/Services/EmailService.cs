using ASI.Basecode.Services.Interfaces;
using ASI.Basecode.Services.ServiceModels;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ASI.Basecode.Services.Services
{
    /// 
    /// Email service for sending emails via SMTP
    /// 
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
            
            // Validate configuration on initialization
            if (_emailSettings == null)
                throw new InvalidOperationException("EmailSettings configuration is missing");
        }

        /// 
        /// Send contact form email to configured recipient
        /// 
        public async Task SendContactEmailAsync(ContactFormViewModel contactForm)
        {
            if (contactForm == null)
                throw new ArgumentNullException(nameof(contactForm));

            // Validate email settings before proceeding
            if (string.IsNullOrEmpty(_emailSettings.RecipientEmail))
                throw new InvalidOperationException("RecipientEmail is not configured in EmailSettings");

            if (string.IsNullOrEmpty(_emailSettings.SenderEmail))
                throw new InvalidOperationException("SenderEmail is not configured in EmailSettings");

            if (string.IsNullOrEmpty(_emailSettings.Username))
                throw new InvalidOperationException("Username is not configured in EmailSettings");

            if (string.IsNullOrEmpty(_emailSettings.Password))
                throw new InvalidOperationException("Password is not configured in EmailSettings");

            // Create HTML email body
            var emailBody = $@"
                <html>
                <head>
                    <meta charset=""UTF-8"">
                    <style>
                        body {{ font-family: Arial, sans-serif; line-height: 1.6; color: #333; }}
                        .container {{ max-width: 600px; margin: 0 auto; padding: 20px; }}
                        .header {{ background-color: #4F46E5; color: white; padding: 20px; text-align: center; border-radius: 5px 5px 0 0; }}
                        .content {{ background-color: #f9f9f9; padding: 30px; border: 1px solid #ddd; border-radius: 0 0 5px 5px; }}
                        .field {{ margin-bottom: 15px; }}
                        .field-label {{ font-weight: bold; color: #555; }}
                        .field-value {{ margin-top: 5px; padding: 10px; background-color: white; border-left: 3px solid #4F46E5; }}
                        .message-box {{ background-color: white; padding: 15px; border: 1px solid #ddd; border-radius: 5px; margin-top: 10px; }}
                        .footer {{ margin-top: 20px; padding-top: 20px; border-top: 1px solid #ddd; font-size: 12px; color: #888; text-align: center; }}
                    </style>
                </head>
                <body>
                    <div class='container'>
                        <div class='header'>
                            <h2>New Contact Form Submission</h2>
                        </div>
                        <div class='content'>
                            <div class='field'>
                                <div class='field-label'>From:</div>
                                <div class='field-value'>{contactForm.Name}</div>
                            </div>
                            <div class='field'>
                                <div class='field-label'>Email:</div>
                                <div class='field-value'><a href='mailto:{contactForm.Email}'>{contactForm.Email}</a></div>
                            </div>
                            <div class='field'>
                                <div class='field-label'>Subject:</div>
                                <div class='field-value'>{contactForm.Subject}</div>
                            </div>
                            <div class='field'>
                                <div class='field-label'>Message:</div>
                                <div class='message-box'>{contactForm.Message.Replace("\n", "<br/>")}</div>
                            </div>
                            <div class='footer'>
                                <p>This message was sent from the BookIt Room Booking System contact form.</p>
                                <p>Sent on: {DateTime.Now:MMMM dd, yyyy 'at' hh:mm tt}</p>
                            </div>
                        </div>
                    </div>
                </body>
                </html>";

            var subject = $"Contact Form: {contactForm.Subject}";
            
            // Send to configured recipient email
            await SendEmailAsync(_emailSettings.RecipientEmail, subject, emailBody);
        }

        /// 
        /// Send generic email via SMTP
        /// 
        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            if (string.IsNullOrEmpty(toEmail))
                throw new ArgumentNullException(nameof(toEmail));

            if (string.IsNullOrEmpty(subject))
                throw new ArgumentNullException(nameof(subject));

            if (string.IsNullOrEmpty(body))
                throw new ArgumentNullException(nameof(body));

            try
            {
                using (var message = new MailMessage())
                {
                    message.From = new MailAddress(_emailSettings.SenderEmail, _emailSettings.SenderName);
                    message.To.Add(new MailAddress(toEmail));
                    message.Subject = subject;
                    message.Body = body;
                    message.IsBodyHtml = true;
                    message.BodyEncoding = Encoding.UTF8;
                    message.SubjectEncoding = Encoding.UTF8;
                    message.Priority = MailPriority.Normal;

                    using (var smtpClient = new SmtpClient(_emailSettings.SmtpServer, _emailSettings.SmtpPort))
                    {
                        smtpClient.Credentials = new NetworkCredential(
                            _emailSettings.Username, 
                            _emailSettings.Password
                        );
                        smtpClient.EnableSsl = _emailSettings.EnableSsl;
                        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

                        await smtpClient.SendMailAsync(message);
                    }
                }
            }
            catch (SmtpException ex)
            {
                throw new Exception($"Failed to send email: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while sending email: {ex.Message}", ex);
            }
        }
    }
}
