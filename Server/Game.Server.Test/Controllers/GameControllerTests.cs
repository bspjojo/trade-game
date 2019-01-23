using System.Threading.Tasks;
using Game.Server.Controllers;
using Game.Server.Models;
using Game.Server.Services;
using Game.Server.Services.Models;
using Moq;
using Xunit;

namespace Game.Server.Test.Controllers
{
    public class GameControllerTests
    {
        private readonly Mock<IGameFlowService> _mockIGameFlowService;
        private readonly GameController _controller;

        private ScoreServiceResult _executeUpdateScoreFlowResponse;

        public GameControllerTests()
        {
            _mockIGameFlowService = new Mock<IGameFlowService>();
            _mockIGameFlowService
                .Setup(m => m.ExecuteUpdateScoreFlow(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<ConsumptionResources>()))
                .ReturnsAsync(() => _executeUpdateScoreFlowResponse);

            _executeUpdateScoreFlowResponse = new ScoreServiceResult
            {
                Excess = new ConsumptionResources(),
                NextYearTarget = new ConsumptionResources()
            };

            _controller = new GameController(_mockIGameFlowService.Object);
        }

        [Fact]
        public async Task UpdateScores_ShouldCallGameFlowServiceWith_GameIdCountryIdYearYearResults()
        {
            var update = new ScorerClientScoreUpdate
            {
                GameId = "gameId",
                CountryId = "countryId",
                Year = 3,
                YearResults = new ConsumptionResources()
            };

            await _controller.UpdateScores(update);

            _mockIGameFlowService.Verify(m => m.ExecuteUpdateScoreFlow("gameId", "countryId", 3, update.YearResults));
        }

        [Fact]
        public async Task UpdateScores_ShouldReturnTheResponseWith_NextYearTargetAndExcess()
        {
            var update = new ScorerClientScoreUpdate
            {
                GameId = "gameId",
                CountryId = "countryId",
                Year = 3,
                YearResults = new ConsumptionResources()
            };

            var response = await _controller.UpdateScores(update);

            Assert.Same(_executeUpdateScoreFlowResponse.NextYearTarget, response.NextYearTarget);
            Assert.Same(_executeUpdateScoreFlowResponse.Excess, response.Excess);
        }
    }
}