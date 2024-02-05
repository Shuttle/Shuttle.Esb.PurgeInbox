using Shuttle.Core.Contract;
using Shuttle.Core.Pipelines;

namespace Shuttle.Esb.PurgeInbox
{
    public class PurgeInboxObserver : IPipelineObserver<OnAfterConfigure>
    {
        private readonly IServiceBusConfiguration _configuration;

        public PurgeInboxObserver(IServiceBusConfiguration configuration)
        {
            Guard.AgainstNull(configuration, nameof(configuration));

            _configuration = configuration;
        }

        public void Execute(OnAfterConfigure pipelineEvent)
        {
            (_configuration.Inbox.WorkQueue as IPurgeQueue)?.Purge(); ;
        }
    }
}