using MicroserviceMail.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MicroserviceMail.Data.Map
{
    public class MailMap : IEntityTypeConfiguration<Mail>
    {
        public void Configure(EntityTypeBuilder<Mail> builder)
        {
            builder.ToTable("Mail");
            builder.HasKey(x => x.MailId);
            builder.Property(x => x.Owner).HasColumnType("Varchar(200)").IsRequired();
            builder.Property(x => x.From).HasColumnType("Varchar(200)").IsRequired();
            builder.Property(x => x.To).HasColumnType("Varchar(2000)").IsRequired();
            builder.Property(x => x.Cc).HasColumnType("Varchar(2000)");
            builder.Property(x => x.Subject).HasColumnType("Varchar(200)").IsRequired();
            builder.Property(x => x.Body).HasColumnType("Varchar(2000)").IsRequired();
            builder.Property(x => x.Status).HasColumnType("Varchar(1000)").IsRequired();
        }
    }
}
