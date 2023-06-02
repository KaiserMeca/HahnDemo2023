using Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AppWebApi.Controllers
{
    /// <summary>
    /// Controller for sending mail and checking if it was sent successfully.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MailSendOkController : ControllerBase
    {
        private readonly IMailSendOkServices _mailSendOkServices;

        /// <summary>
        /// Initializes a new instance of the <see cref="MailSendOkController"/> class
        /// </summary>
        /// <param name="mailSendOkServices">Mail error services</param>
        public MailSendOkController(IMailSendOkServices mailSendOkServices)
        {
            _mailSendOkServices = mailSendOkServices;
        }

        /// <summary>
        /// Verify that there were no errors with the email
        /// </summary>
        /// <returns>Returns an <see cref="IActionResult"/> indicating the result of the operation.</returns>
        [HttpGet]
        public async Task<IActionResult> SendMail()
        {
            bool send = await _mailSendOkServices.SendMailOk();

            if (send)
            {
                return Ok(new { message = "" });
            }
            else
            {
                return BadRequest(new { message = "Mail not sent" });
            }
        }
    }
}
