using System.Threading.Tasks;
using Game.Server.Hubs;
using Game.Server.Services;
using Game.Server.Services.Models;
using Microsoft.AspNetCore.SignalR;
using Moq;
using Xunit;

namespace Game.Server.Test.Services
{
    public class GameHubServiceTests
    {
        private readonly GameHubService _gameHubService;
        private readonly Mock<IHubClients<IGameHub>> _mockHubClients;
        private readonly Mock<IGameHub> _gameHub;

        public GameHubServiceTests()
        {
            _gameHub = new Mock<IGameHub>();
            _gameHub.Setup(m => m.ScoresUpdated(It.IsAny<ScoreUpdatedBroadcastModel>()))
                .Returns(Task.CompletedTask);

            _mockHubClients = new Mock<IHubClients<IGameHub>>();
            _mockHubClients.Setup(m => m.Group(It.IsAny<string>()))
                .Returns(_gameHub.Object);

            var mockHubContext = new Mock<IHubContext<GameHub, IGameHub>>();
            mockHubContext.SetupGet(m => m.Clients)
                .Returns(_mockHubClients.Object);

            _gameHubService = new GameHubService(mockHubContext.Object);
        }

        [Fact]
        public async void ScoresUpdated_ShouldUseGroupMatchingTheGameId()
        {
            var scores = new ConsumptionResources();

            await _gameHubService.ScoresUpdated("gameId", "countryId", 2, scores);

            _mockHubClients.Verify(m => m.Group("gameId"));
        }

        [Fact]
        public async void ScoresUpdated_ShouldSendAScoreUpdatedBroadcastModelWith_CountryIdYearAndScores()
        {
            var scores = new ConsumptionResources();

            await _gameHubService.ScoresUpdated("gameId", "countryId", 2, scores);

            _gameHub.Verify(m => m.ScoresUpdated(It.Is<ScoreUpdatedBroadcastModel>(v => v.Year == 2 && v.Scores == scores && v.CountryId == "countryId")));
        }
    }
}