using EmailService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmailService.Controllers
{
    [ApiController]
    [Route("email")]
    public class EmailServiceController : ControllerBase
    {
        private readonly ILogger<EmailServiceController> _logger;
        private readonly IEmailSender _emailSender;

        public EmailServiceController(ILogger<EmailServiceController> logger, IEmailSender emailSender)
        {
            _logger = logger;
            _emailSender = emailSender;
        }

        [HttpPost]
        [Route("send")]    
        public async Task<IActionResult> SendEmail(EmailData emailData)
        {
            if (!(emailData.ToRecipients.Length > 0))  
            {
                _logger.LogDebug("ToEmail address is required.");
                return BadRequest("ToEmail address is required.");
            }
            try
            {
                await _emailSender.SendEmail(emailData); 
                return Ok("Email sent successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while service the request. {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
