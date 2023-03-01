using MicroserviceMail.Domain;
using System.Web.Http;

namespace MicroserviceMail.Repository
{
    public interface ISendMailRepository
    {
        Task<Mail> SaveMailInfo(Mail mail);
    }
}
