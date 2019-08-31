using Game.Server.DataRepositories;
using Game.Server.Services;
using Game.Server.Services.Models;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Game.Server.Test.Services
{
    public class GameUpdatedServiceTests
    {
        private Mock<IGameDataService> _mockGameDataService;
        private Mock<IGameHubService> _mockGameHubService;
        private GameUpdatedService _gameUpdatedService;

        private GameScoresBroadcastModel _broadcastModel;

        public GameUpdatedServiceTests()
        {
            _mockGameDataService = new Mock<IGameDataService>();
            _broadcastModel = new GameScoresBroadcastModel();
            _mockGameDataService.Setup(m => m.GetGameScores("gameId")).ReturnsAsync(() => _broadcastModel);

            _mockGameHubService = new Mock<IGameHubService>();
            var mockLogger = new Mock<ILogger<GameUpdatedService>>();

            _gameUpdatedService = new GameUpdatedService(mockLogger.Object, _mockGameDataService.Object, _mockGameHubService.Object);
        }

        [Fact]
        public async void GameUpdated_ShouldBroadcastTheScoresForTheGameId()
        {
            await _gameUpdatedService.GameUpdated("gameId");

            _mockGameHubService.Verify(m => m.ScoresUpdated("gameId", _broadcastModel));
        }
    }
}