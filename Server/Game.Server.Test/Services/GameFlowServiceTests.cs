using System;
using System.Threading.Tasks;
using Game.Server.DataRepositories;
using Game.Server.Models;
using Game.Server.Services;
using Game.Server.Services.Models;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Game.Server.Test.Services
{
    public class GameFlowServiceTests
    {
        private readonly Mock<IGameDataService> _mockGameDataService;
        private readonly Mock<IGameScoreService> _mockGameScoreService;
        private readonly Mock<IGameUpdatedService> _mockGameUpdatedService;

        private GameFlowService _gameFlowService;

        private ConsumptionResources _breakEven;
        private ConsumptionResources _targetsForCurrentYear;
        private ConsumptionResources _recorded;
        private ConsumptionResources _excess;
        private ConsumptionResources _scores;
        private ConsumptionResources _targets;

        public GameFlowServiceTests()
        {
            _mockGameDataService = new Mock<IGameDataService>();
            var gameInformation = new GameInformationResult
            {
                Id = Guid.Parse("7339670a-bf97-40dc-bb99-834b076768cc"),
                CurrentYear = 4
            };
            _mockGameDataService.Setup(m => m.GetGameInformationForACountry("cId")).ReturnsAsync(() => gameInformation);
            _mockGameDataService.Setup(m => m.SetCountryYearExcess("cId", 4, _excess)).Returns(Task.CompletedTask);
            _mockGameDataService.Setup(m => m.SetCountryYearScores("cId", 4, _scores)).Returns(Task.CompletedTask);
            _mockGameDataService.Setup(m => m.SetCountryYearTargets("cId", 5, _targets)).Returns(Task.CompletedTask);

            _breakEven = new ConsumptionResources();
            _mockGameDataService.Setup(m => m.GetBreakEvenForACountry("cId")).ReturnsAsync(() => _breakEven);

            _targetsForCurrentYear = new ConsumptionResources();
            _mockGameDataService.Setup(m => m.GetTargetsForACountryForAYear("cId", 4)).ReturnsAsync(() => _targetsForCurrentYear);

            _mockGameScoreService = new Mock<IGameScoreService>();
            _recorded = new ConsumptionResources();
            _mockGameScoreService.Setup(m => m.CalculateYearValues(_breakEven, _targetsForCurrentYear, _recorded, out _excess, out _scores, out _targets));
            _mockGameUpdatedService = new Mock<IGameUpdatedService>();
            _mockGameUpdatedService.Setup(m => m.GameUpdated(It.IsAny<string>())).Returns(() => Task.CompletedTask);

            var mockLogger = new Mock<ILogger<GameFlowService>>();

            _gameFlowService = new GameFlowService(mockLogger.Object, _mockGameDataService.Object, _mockGameScoreService.Object, _mockGameUpdatedService.Object);
        }

        #region ExecuteUpdateScoreFlow

        [Fact]
        public async void ExecuteUpdateScoreFlow_ShouldSetTheCountryYearExcess()
        {
            var res = await _gameFlowService.ExecuteUpdateScoreFlow("cId", _recorded);

            _mockGameDataService.Setup(m => m.SetCountryYearExcess("cId", 4, _excess));
        }

        [Fact]
        public async void ExecuteUpdateScoreFlow_ShouldSetTheCountryYearScores()
        {
            var res = await _gameFlowService.ExecuteUpdateScoreFlow("cId", _recorded);

            _mockGameDataService.Setup(m => m.SetCountryYearScores("cId", 4, _scores));
        }

        [Fact]
        public async void ExecuteUpdateScoreFlow_ShouldSetTheCountryYearTargets()
        {
            var res = await _gameFlowService.ExecuteUpdateScoreFlow("cId", _recorded);

            _mockGameDataService.Setup(m => m.SetCountryYearTargets("cId", 5, _targets));
        }

        [Fact]
        public async void ExecuteUpdateScoreFlow_ShouldCallGameUpdatedService_GameUpdatedWithTheGameId()
        {
            var res = await _gameFlowService.ExecuteUpdateScoreFlow("cId", _recorded);

            _mockGameUpdatedService.Setup(m => m.GameUpdated("7339670a-bf97-40dc-bb99-834b076768cc"));
        }

        [Fact]
        public async void ExecuteUpdateScoreFlow_ShouldReturnTheNextYearTarget_Excess_Score()
        {
            var res = await _gameFlowService.ExecuteUpdateScoreFlow("cId", _recorded);

            Assert.Equal(_targets, res.NextYearTarget);
            Assert.Equal(_excess, res.Excess);
            Assert.Equal(_scores, res.Scores);
        }

        #endregion

        #region UpdateGameYear

        public async void UpdateGameYear_ShouldCallGameDataServiceToUpdateTheYearForAGame()
        {
            await _gameFlowService.UpdateGameYear(new UpdateYear { GameId = "gId", Year = 2 });

            _mockGameDataService.Verify(m => m.UpdateCurrentYearForGame("gId", 2), Times.Once);
        }

        public async void UpdateGameYear_ShouldCallGameUpdatedServiceBroadcastTheGameHasUpdated()
        {
            await _gameFlowService.UpdateGameYear(new UpdateYear { GameId = "gId", Year = 2 });

            _mockGameUpdatedService.Verify(m => m.GameUpdated("gId"), Times.Once);
        }

        #endregion
    }
}