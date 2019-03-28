using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LiveSportBet.Hubs;

namespace LiveSportBet.Services
{
    // This Service help us Get IHubContext to UpdateService Singleton obbject, because we can't get it by DI container.
    internal class UpdaterServiceHelper : IHostedService
    {

        private readonly IHubContext<UpdateHub> _hubContext;

        public UpdaterServiceHelper(IHubContext<UpdateHub> hubContext)
        {
            _hubContext = hubContext;
        }

        // Triggered when the application host is ready to start the service.
        public Task StartAsync(CancellationToken cancellationToken)
        {
            UpdateService._hubContext = _hubContext; // send _hubContext to UpdateService;
            return Task.CompletedTask;
        }

        //Triggered when the application host is performing a graceful shutdown.
        // Must be there
        public Task StopAsync(CancellationToken cancellationToken)
        {

            return Task.CompletedTask;
        }
    }
}
