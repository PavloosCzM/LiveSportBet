using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LiveSportBet.Events;
using LiveSportBet.Hubs;
using LiveSportBet.Models;

namespace LiveSportBet.Services
{
    public class UpdateService
    {
        public static UpdateService instance; // instance for singletone
        public static IHubContext<UpdateHub> _hubContext; // static variable where we store HubContext using UpdaterServiceHelper


        // singletone constructor
        public static UpdateService GetInstance()
        {
            if (instance == null)
            {
                instance = new UpdateService();
            }
            return instance;
        }

        private UpdateService()
        {
            MatchManager.GetInstance().onChange += HandleEvent; // If we in MatchManager change values, HandleEvent is started.
        }

        // Call and sendUpdate() to send changed data to users and send info message to server console.
        void HandleEvent(object sender, JsUpdateEventArgs match)
        {
            sendUpdate(match.matchId, match.rateId, match.data);
            Console.WriteLine("Nastala změna MatchID:" + match.matchId + " MatchRateId:" + match.rateId + " NewRate:" + match.data);
        }

        // Dend changed data to users Using UpdateHub
        private async void sendUpdate(int matchId, int rateId, float data)
        {
            await _hubContext.Clients.All.SendAsync("UpdateData", matchId, rateId, data);
        }

    }
}
