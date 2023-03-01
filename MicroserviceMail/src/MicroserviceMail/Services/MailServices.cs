using AutoMapper;
using MicroserviceMail.Domain;
using MicroserviceMail.Enum;
using MicroserviceMail.ExternalServices;
using MicroserviceMail.Repository;
using MicroserviceMail.ViewModel;
using MimeKit;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web.Http;

namespace MicroserviceMail.Services
{
    public class MailServices : IMailServices
    {
        private readonly IClient _mimeKitClient;
        private readonly IMapper _mapper;
        private readonly ISendMailRepository _sendMailRepository;

        public MailServices(IClient mimeKitClient, ISendMailRepository sendMailRepository, IMapper mapper)
        {
            _mimeKitClient = mimeKitClient;
            _sendMailRepository = sendMailRepository;
            _mapper = mapper;
        }
        public MailViewModel SendMessage(MailViewModel mail, ConfigurationSettingsMail config)
        {
            var message = _mimeKitClient.ConfigureMessage(mail);

            var result = _mimeKitClient.SendMessage(message, config);

            mail.Status = $"{result.Status} - {result.Msg}";

            return mail;
        }

        public bool IsValidEmails(string emails, EFieldMail field)
        {
            if (string.IsNullOrEmpty(emails))
            {
                if (field == EFieldMail.Cc)
                    return true;
                else
                    return false;
            }

            string[] mails = emails.Split(';');

            foreach (string mail in mails)
            {
                string email = string.Empty;
                try
                {
                    // Normalize the domain
                    email = Regex.Replace(mail, @"(@)(.+)$", DomainMapper,
                                          RegexOptions.None, TimeSpan.FromMilliseconds(200));

                    // Examines the domain part of the email and normalizes it.
                    string DomainMapper(Match match)
                    {
                        // Use IdnMapping class to convert Unicode domain names.
                        var idn = new IdnMapping();

                        // Pull out and process domain name (throws ArgumentException on invalid)
                        string domainName = idn.GetAscii(match.Groups[2].Value);

                        return match.Groups[1].Value + domainName;
                    }
                }
                catch (RegexMatchTimeoutException e)
                {
                    return false;
                }
                catch (ArgumentException e)
                {
                    return false;
                }

                try
                {
                    if (Regex.IsMatch(email,
                        @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                        RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)) == false)
                    {
                        return false;
                    }
                }
                catch (RegexMatchTimeoutException)
                {
                    return false;
                }


            }

            return true;
        }

        public async Task<MailViewModel> SaveMailInfo(MailViewModel mailViewModel)
        {
            try
            {
                Mail mail = _mapper.Map<Mail>(mailViewModel);
                mail.Status += $"\nSalvo em {DateTime.Now}";
                mail.CreateAt = DateTime.Now;
                mail.Sent = (mail.Status.Contains("ERROR") ? false: true) ;

                var result = await _sendMailRepository.SaveMailInfo(mail);

                mailViewModel = _mapper.Map<MailViewModel>(result);

            }
            catch (Exception e)
            {
                mailViewModel.Status += $"\nErro ao salvar informações no banco de dados. Error: {e.Message}";
            }

            return mailViewModel;

        }
    }
}
