using System.Threading.Tasks;
using Shuttle.Core.Contract;
using Shuttle.Core.Pipelines;

namespace Shuttle.Esb.PurgeInbox
{
    public class PurgeInboxObserver : IPipelineObserver<OnAfterCreatePhysicalQueues>
    {
        private readonly IServiceBusConfiguration _configuration;

        public PurgeInboxObserver(IServiceBusConfiguration configuration)
        {
            Guard.AgainstNull(configuration, nameof(configuration));

            _configuration = configuration;
        }

        public void Execute(OnAfterCreatePhysicalQueues pipelineEvent)
        {
            (_configuration.Inbox.WorkQueue as IPurgeQueue)?.Purge(); ;
        }

        public async Task ExecuteAsync(OnAfterCreatePhysicalQueues pipelineEvent)
        {
            Execute(pipelineEvent);

            await Task.CompletedTask;
        }
    }
}