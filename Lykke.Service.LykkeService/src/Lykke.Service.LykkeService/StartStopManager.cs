using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Common.Log;
using Lykke.Common;
using Lykke.Common.Log;
using Lykke.Sdk;

namespace Lykke.Service.LykkeService
{
    public class StartStopManager : IStartupManager, IShutdownManager
    {
        private readonly IEnumerable<IStartStop> _items;
        private readonly IEnumerable<IStopable> _stoppables;
        private readonly ILog _log;

        public StartStopManager(
            IEnumerable<IStartStop> items,
            IEnumerable<IStopable> stoppables,
            ILogFactory logFactory)
        {
            _items = items;
            _stoppables = stoppables;
            _log = logFactory.CreateLog(this);
        }

        public Task StartAsync()
        {
            foreach (var startable in _items)
            {
                startable.Start();
            }

            return Task.CompletedTask;
        }

        public async Task StopAsync()
        {
            try
            {
                await Task.WhenAll(_items.Select(i => Task.Run(() => i.Stop())));

                await Task.WhenAll(_stoppables.Select(i => Task.Run(() => i.Stop())));
            }
            catch (Exception ex)
            {
                _log.Warning($"Unable to stop a component", ex);
            }
        }
    }
}
