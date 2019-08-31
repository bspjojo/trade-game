using System.Threading.Tasks;
using Game.Server.DataRepositories;
using Microsoft.Extensions.Logging;

namespace Game.Server.Services
{
    public interface IGameUpdatedService
    {
        Task GameUpdated(string gameId);
    }

    public class GameUpdatedService : IGameUpdatedService
    {
        private readonly IGameDataService _gameDataService;
        private readonly IGameHubService _gameHubService;
        private readonly ILogger<GameUpdatedService> _logger;

        public GameUpdatedService(ILogger<GameUpdatedService> logger, IGameDataService gameDataService, IGameHubService gameHubService)
        {
            _gameDataService = gameDataService;
            _gameHubService = gameHubService;
            _logger = logger;
        }

        public async Task GameUpdated(string gameId)
        {
            var scores = await _gameDataService.GetGameScores(gameId);

            _logger.LogInformation($"Game information updated for {gameId}");

            await _gameHubService.ScoresUpdated(gameId, scores);
        }
    }
}