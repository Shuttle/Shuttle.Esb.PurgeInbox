using System.Threading.Tasks;
using Shuttle.Core.Contract;
using Shuttle.Core.Pipelines;

namespace Shuttle.Esb.PurgeInbox;

public class PurgeInboxObserver : IPipelineObserver<OnAfterCreatePhysicalQueues>
{
    private readonly IServiceBusConfiguration _configuration;

    public PurgeInboxObserver(IServiceBusConfiguration configuration)
    {
        _configuration = Guard.AgainstNull(configuration);
    }

    public async Task ExecuteAsync(IPipelineContext<OnAfterCreatePhysicalQueues> pipelineContext)
    {
        await ((_configuration.Inbox?.WorkQueue as IPurgeQueue)?.PurgeAsync() ?? Task.CompletedTask);
    }
}