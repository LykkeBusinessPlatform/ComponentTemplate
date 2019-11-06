using System.Collections.Generic;
using System.Threading.Tasks;
using Lykke.Common;
#if azurequeuesub
using Lykke.JobTriggers.Triggers;
#endif
using Lykke.Sdk;

namespace Lykke.Job.LykkeService.Services
{
    public class StartupManager : IStartupManager
    {
        private readonly IEnumerable<IStartStop> _startables;
#if azurequeuesub
        private readonly TriggerHost _triggerHost;
#endif

        public StartupManager(
            IEnumerable<IStartStop> startables
#if azurequeuesub
            , TriggerHost triggerHost
#endif
            )
        {
            _startables = startables;
#if azurequeuesub
            _triggerHost = triggerHost;
#endif
        }

#if azurequeuesub
        public async Task StartAsync()
        {
            foreach (var startable in _startables)
            {
                startable.Start();
            }

            await _triggerHost.Start();
        }
#else
        public Task StartAsync()
        {
            foreach (var startable in _startables)
            {
                startable.Start();
            }

            return Task.CompletedTask;
        }
#endif
    }
}
