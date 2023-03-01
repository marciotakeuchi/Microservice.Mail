using MicroserviceMail.Data;
using MicroserviceMail.Domain;
using MicroserviceMail.Enum;
using System.Web.Http;

namespace MicroserviceMail.Repository
{
    public class SendMailRepository : ISendMailRepository
    {

        private readonly ApplicationDbContext _applicationDbContext;

        public SendMailRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Mail> SaveMailInfo(Mail mail)
        {
            try
            {
                _applicationDbContext.Add(mail);
                await _applicationDbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw ;
            }

            return mail;
        }
    }
}
