using MicroserviceMail.Domain;
using MicroserviceMail.Repository;

namespace MicroserviceMail.Tests.Repository
{
    public class SendMailRepositoryFake : ISendMailRepository
    {
        public Task<Mail> SaveMailInfo(Mail mail)
        {
            return Task.FromResult(mail);
        }
    }
}