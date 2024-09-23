using Ranck.CreditCard.Application.Mappings;
using Ranck.CreditCard.Core.Configuration;
using Ranck.CreditCard.WebService.Framework;
using Ranck.CreditCard.WebService.Middlewares;

namespace Ranck.CreditCard.WebService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Add Configuration
            var configurations = builder.Configuration.GetSection(CreditCardServiceOptions.CreditCardService);
            builder.Services.Configure<CreditCardServiceOptions>(options => configurations.Bind(options));

            //Add Services
            builder.Services.AddCustomServices();

            //AddMapper
            builder.Services
                .AddAutoMapper(typeof(CreateCreditServiceResponseProfile).Assembly);
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseMiddleware<ErrorLoggingMiddleware>();
            app.MapControllers();

            app.Run();
        }
    }
}
