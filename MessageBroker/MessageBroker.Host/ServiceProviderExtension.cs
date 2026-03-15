using MessageBroker.Common.Interfaces;
using MessageBroker.Service.Repositories;
using MessageBroker.Service.Services;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace MessageBroker.Host
{
    internal static class ServiceProviderExtension
    {
        internal static IServiceProvider Configure()
        {
            var services = new ServiceCollection();

            services.AddSingleton<ISubscriptionRepository, SubscriptionRepository>();

            services.AddSingleton<PublisherService>();
            services.AddSingleton<SubscriberService>();

            return services.BuildServiceProvider();
        }
    }
}
