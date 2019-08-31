using System.Threading.Tasks;
using Game.Server.Hubs;
using Game.Server.Services.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Game.Server.Services
{
    public interface IGameHubService
    {
        Task ScoresUpdated(string gameId, GameScoresBroadcastModel scores);
    }

    public class GameHubService : IGameHubService
    {
        private readonly IHubContext<GameHub, IGameHub> _gameHubContext;
        private readonly ILogger<GameHubService> _logger;

        public GameHubService(ILogger<GameHubService> logger, IHubContext<GameHub, IGameHub> gameHubContext)
        {
            _logger = logger;
            _gameHubContext = gameHubContext;
        }

        public Task ScoresUpdated(string gameId, GameScoresBroadcastModel scores)
        {
            _logger.LogInformation($"Broadcasting scores for game {gameId})");

            return _gameHubContext.Clients.Group(gameId).ScoresUpdated(scores);
        }
    }
}