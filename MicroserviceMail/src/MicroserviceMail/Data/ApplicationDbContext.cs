using MicroserviceMail.Data.Map;
using MicroserviceMail.Domain;
using Microsoft.EntityFrameworkCore;

namespace MicroserviceMail.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new MailMap());

            base.OnModelCreating(builder);
        }

        public DbSet<Mail> Mails { get; set; }

    }
}
