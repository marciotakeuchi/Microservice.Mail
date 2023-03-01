using MicroserviceMail.Domain;
using MicroserviceMail.Enum;
using MicroserviceMail.ViewModel;
using System.Web.Http;

namespace MicroserviceMail.Services
{
    public interface IMailServices
    {
        MailViewModel SendMessage(MailViewModel mail, ConfigurationSettingsMail config);

        Task<MailViewModel> SaveMailInfo(MailViewModel mailViewModel);

        bool IsValidEmails(string emails, EFieldMail field);
    }
}
