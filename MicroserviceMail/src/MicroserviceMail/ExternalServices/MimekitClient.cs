using MailKit.Net.Smtp;
using MailKit.Security;
using MicroserviceMail.Domain;
using MicroserviceMail.Enum;
using MicroserviceMail.ViewModel;
using MimeKit;

namespace MicroserviceMail.ExternalServices
{
    public class MimekitClient : IClient
    {
        public MimeMessage ConfigureMessage(MailViewModel email)
        {

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("",email.From));
            message.To.AddRange(GetInternetAddresses(email.To));
            if(email.Cc != null)
                message.Cc.AddRange(GetInternetAddresses(email.Cc));
            message.Subject = email.Subject;
            message.Body = new TextPart("html") { Text = email.Body };

            return message;
        }

        private InternetAddressList GetInternetAddresses(string emailArray) 
        {
            var list = new InternetAddressList();
            foreach (var email in emailArray.Split(';'))
            {
                if(!string.IsNullOrEmpty(email))
                    list.Add(new MailboxAddress("",email) );
            }

            return list;
        }

        public (EStatusMail Status, string Msg) SendMessage(MimeMessage message, ConfigurationSettingsMail config)
        {
            try
            {
                using (var client = new SmtpClient())
                {
                    client.Connect(config.SMTP, config.Port, SecureSocketOptions.Auto);
                    client.Authenticate(config.User, config.Passwd);
                    client.Send(message);
                    client.Disconnect(true);
                }
                
                return  (EStatusMail.SENT, $"Enviado em {DateTime.Now}");
            }
            catch (Exception e)
            {
                return (EStatusMail.ERROR, $"Erro ao enviar em {DateTime.Now}.\n Error_Message: {e.Message}.\n Error_Innerexception{e.InnerException}");
            }
          
        }

    }
}
