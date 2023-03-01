using MicroserviceMail.Domain;
using MicroserviceMail.Enum;
using MicroserviceMail.Services;
using MicroserviceMail.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;


namespace MicroserviceMail.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MailController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IMailServices _mailServices;

        public MailController(IConfiguration config, IMailServices mailServices)
        {
            _config = config;
            _mailServices = mailServices;
        }

        [HttpPost("SendMail")]
        public async Task<IActionResult> SendMail(MailViewModel mail)
        {
            var configMail = GetSettingsMail();

            if (
            !_mailServices.IsValidEmails(mail.From, EFieldMail.From) ||
            !_mailServices.IsValidEmails(mail.To, EFieldMail.To) ||
            !_mailServices.IsValidEmails(mail.Cc, EFieldMail.Cc)
                )
            {
                mail.Status = "Bad Request. Emails invalidos";
                return StatusCode(400, mail.Status);
            }

            try
            {
                var resultService = _mailServices.SendMessage(mail, configMail);
               
                var resultDb = await _mailServices.SaveMailInfo(resultService);

                if (resultDb.Status.Contains("Erro"))
                {
                    return StatusCode(503, "Serviço indisponível.");
                }

                return Ok(resultDb);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Erro ao enviar e-mail. {e.Message}");
            }

        }

        private ConfigurationSettingsMail GetSettingsMail()
        {

            return new ConfigurationSettingsMail
            {
                SMTP = _config.GetValue<string>("MailConfig:SMTP"),
                Port = _config.GetValue<int>("MailConfig:Port"),
                User = _config.GetValue<string>("MailConfig:User"),
                Passwd = _config.GetValue<string>("MailConfig:Passwd"),
                EnableSsl = _config.GetValue<bool>("MailConfig:EnableSsl")
            };

        }
    }
}