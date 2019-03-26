using System.Threading.Tasks;
using Game.Server.Hubs;
using Game.Server.Services.Models;
using Microsoft.AspNetCore.SignalR;

namespace Game.Server.Services
{
    public interface IGameHubService
    {
        Task ScoresUpdated(string gameId, GameScoresBroadcastModel scores);
    }

    public class GameHubService : IGameHubService
    {
        private readonly IHubContext<GameHub, IGameHub> _gameHubContext;

        public GameHubService(IHubContext<GameHub, IGameHub> gameHubContext)
        {
            _gameHubContext = gameHubContext;
        }

        public Task ScoresUpdated(string gameId, GameScoresBroadcastModel scores)
        {
            return _gameHubContext.Clients.Group(gameId).ScoresUpdated(scores);
        }
    }
}