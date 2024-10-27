using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Shuttle.Core.Contract;
using Shuttle.Core.Pipelines;

namespace Shuttle.Esb.PurgeInbox;

public class PurgeInboxHostedService : IHostedService
{
    private readonly IPipelineFactory _pipelineFactory;
    private readonly PurgeInboxObserver _purgeInboxObserver;
    private readonly Type _startupPipelineType = typeof(StartupPipeline);

    public PurgeInboxHostedService(IPipelineFactory pipelineFactory, PurgeInboxObserver purgeInboxObserver)
    {
        _pipelineFactory = Guard.AgainstNull(pipelineFactory);
        _purgeInboxObserver = Guard.AgainstNull(purgeInboxObserver);

        _pipelineFactory.PipelineCreated += OnPipelineCreated;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        _pipelineFactory.PipelineCreated -= OnPipelineCreated;

        await Task.CompletedTask;
    }

    private void OnPipelineCreated(object? sender, PipelineEventArgs e)
    {
        if (e.Pipeline.GetType() != _startupPipelineType)
        {
            return;
        }

        e.Pipeline.RegisterObserver(_purgeInboxObserver);
    }
}