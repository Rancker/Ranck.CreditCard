using Ranck.CreditCard.Application.Services;
using Ranck.CreditCard.Application.Services.Interfaces;
using Ranck.CreditCard.Infrastructure.Communication.ApiClients;
using Ranck.CreditCard.Infrastructure.Communication.Interfaces;
using Ranck.CreditCard.Infrastructure.Data;
using Ranck.CreditCard.Infrastructure.Data.Interfaces;

namespace Ranck.CreditCard.WebService.Framework
{
    /// <summary>
    /// The extension to add services.
    /// </summary>
    public static class ServicesExtensions
    {
        /// <summary>
        /// Adds the custom services.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <returns></returns>
        public static IServiceCollection AddCustomServices(this IServiceCollection service)
        {
            service.AddScoped<ICreditCardService, CreditCardService>();
            service.AddScoped<ICreditCardServiceClient, CreditCardServiceClient>();
            service.AddSingleton<ICreditCardRepository, CreditCardRepository>();
            return service;
        }
    }
}
