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
        private Mock<IHubContext<GameHub, IGameHub>> _mockGameHubContext;
        private GameHubService _gameHubService;

        private GameScoresBroadcastModel _broadcastmodel;

        public GameHubServiceTests()
        {
            _broadcastmodel = new GameScoresBroadcastModel();
            var gameHub = new Mock<IGameHub>();
            gameHub.Setup(m => m.ScoresUpdated(_broadcastmodel))
                .Returns(Task.CompletedTask);

            var mockHubClients = new Mock<IHubClients<IGameHub>>();
            mockHubClients.Setup(m => m.Group("game"))
                .Returns(gameHub.Object);

            var mockHubContext = new Mock<IHubContext<GameHub, IGameHub>>();
            mockHubContext.SetupGet(m => m.Clients)
                .Returns(mockHubClients.Object);

            _mockGameHubContext = new Mock<IHubContext<GameHub, IGameHub>>();
            _mockGameHubContext.Setup(m => m.Clients).Returns(() => mockHubClients.Object);

            _gameHubService = new GameHubService(_mockGameHubContext.Object);
        }

        [Fact]
        public void ScoresUpdated_ShouldCallScoresUpdatedForTheGroupForTheGameId()
        {
            Assert.Equal(Task.CompletedTask, _gameHubService.ScoresUpdated("game", _broadcastmodel));
        }
    }
}