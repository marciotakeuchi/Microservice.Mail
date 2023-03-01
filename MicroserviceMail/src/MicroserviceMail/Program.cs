using AutoMapper;
using MicroserviceMail.Data;
using MicroserviceMail.ExternalServices;
using MicroserviceMail.Repository;
using MicroserviceMail.Services;
using Microsoft.EntityFrameworkCore;

namespace MicroserviceMail
{
    public class Program
    {
        public static void Main(string[] args)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddEntityFrameworkNpgsql()
                .AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
                );

            builder.Services.AddScoped<IMailServices, MailServices>();
            builder.Services.AddScoped<ISendMailRepository, SendMailRepository>();
            builder.Services.AddScoped<IClient, MimekitClient>();
            builder.Services.AddAutoMapper(typeof(DomainToViewModelMappingProfile), typeof(ViewModelToDomainMappingProfile));
     

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "SendMailG", Version = "v1" });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}