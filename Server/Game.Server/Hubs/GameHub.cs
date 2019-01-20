using Game.Server.Services.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Game.Server.Hubs
{
    public class GameHub : Hub<IGameHub>
    {
        public Task JoinGame(string gameId)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, gameId);
        }

        public Task LeaveGame(string gameId)
        {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, gameId);
        }
    }
}