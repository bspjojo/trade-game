using System.Threading.Tasks;
using Game.Server.DataRepositories;

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

        public GameUpdatedService(IGameDataService gameDataService, IGameHubService gameHubService)
        {
            _gameDataService = gameDataService;
            _gameHubService = gameHubService;
        }

        public async Task GameUpdated(string gameId)
        {
            var scores = await _gameDataService.GetGameScores(gameId);

            await _gameHubService.ScoresUpdated(gameId, scores);
        }
    }
}