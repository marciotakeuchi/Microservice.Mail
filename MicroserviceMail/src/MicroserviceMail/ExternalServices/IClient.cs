using MicroserviceMail.Domain;
using MicroserviceMail.Enum;
using MicroserviceMail.ViewModel;
using MimeKit;

namespace MicroserviceMail.ExternalServices
{
    public interface IClient
    {
        MimeMessage ConfigureMessage(MailViewModel email);
        (EStatusMail Status, string Msg) SendMessage(MimeMessage message, ConfigurationSettingsMail config);
    }
}
