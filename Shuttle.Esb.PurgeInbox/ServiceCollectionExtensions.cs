﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Shuttle.Core.Contract;

namespace Shuttle.Esb.PurgeInbox;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPurgeInbox(this IServiceCollection services)
    {
        Guard.AgainstNull(services);

        services.TryAddSingleton<PurgeInboxHostedService, PurgeInboxHostedService>();
        services.TryAddSingleton<PurgeInboxObserver, PurgeInboxObserver>();

        services.AddHostedService<PurgeInboxHostedService>();

        return services;
    }
}