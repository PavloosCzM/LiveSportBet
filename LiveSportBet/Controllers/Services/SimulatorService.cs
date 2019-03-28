using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LiveSportBet.Hubs;
using LiveSportBet.Models;

namespace LiveSportBet.Services
{
    // This class it serves only for test purposes. Class Randomly change rate values.
    // Class is create like HostedService, means that ther we run timer which ticks and update rate values.
    internal class SimulatorService : IHostedService, IDisposable
    {
        private Timer _timer;
        private MatchManager matchManager = MatchManager.GetInstance();
        Random random = new Random();
        int secondsWait;
        int secondsElapsed = 0;

        // Triggered when the application host is ready to start the service.
        // Run Timer
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, 1000, 1000);
            secondsWait = random.Next(5, 20);
            return Task.CompletedTask;
        }

        // Code Inside time is execudet evry second, Before we start a timer we generate random number that represents number of cicles that has been skiped.
        // If we skip number of ciclet thats is equal to random number we execute function generateRandom() and generate new random number to skip cicles
        private void DoWork(object state)
        {
            if (secondsWait == secondsElapsed)
            {
                secondsElapsed = 0;
                secondsWait = random.Next(5, 20);
                generateRandom();
            }
            else
            {
                secondsElapsed++;
            }
             
        }

        // This method generate us random number of matches rates to be changed and for evry change call changeMatch() to change rate
        private void generateRandom()
        {
            int count = random.Next(1, 5);
            MatchModel[] matches = matchManager.getMatches();
            for (int i = 0; i < count; i++)
            {
                int rateId = random.Next(1, 3);
                int matchIndex = random.Next(0, matches.Length - 1);
                int matchId = matches[matchIndex].getId();
                float randomFloat = (float)Math.Round(random.NextDouble() + random.Next(5), 2);
                matchManager.changeMatch(matchId, rateId, randomFloat);
            }
        }

        //Triggered when the application host is performing a graceful shutdown.
        //Change Timer time to zero.
        public Task StopAsync(CancellationToken cancellationToken)
        {

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }


        // Releasing unmanaged resources
        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
