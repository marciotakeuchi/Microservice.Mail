using MailKit.Security;
using MicroserviceMail.Domain;
using MicroserviceMail.Enum;
using MicroserviceMail.ExternalServices;
using MicroserviceMail.ViewModel;
using MimeKit;

namespace MicroserviceMail.Tests.ExternalServices
{
    public class MimekitClientFake : IClient
    {
        public MimeMessage ConfigureMessage(MailViewModel email)
        {
            return new MimeMessage();
        }
      
        public (EStatusMail Status, string Msg) SendMessage(MimeMessage message, ConfigurationSettingsMail config)
        {
               return (EStatusMail.SENT, $"Enviado em 01/01/2023");
        }
    }
}
