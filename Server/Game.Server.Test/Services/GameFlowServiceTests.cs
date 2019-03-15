using System;
using System.Threading.Tasks;
using Game.Server.DataRepositories;
using Game.Server.Services;
using Game.Server.Services.Models;
using Moq;
using Xunit;

namespace Game.Server.Test.Services
{
    public class GameFlowServiceTests
    {
        private Mock<IGameDataService> _mockIGameDataService;
        private Mock<IGameHubService> _mockIGameHubService;
        private Mock<IGameScoreService> _mockIGameScoreService;

        private GameFlowService _gameFlowService;

        private GameInformationResult _getGameInformationForACountryResult;
        private ConsumptionResources _getBreakEvenForACountryResult;
        private ConsumptionResources _getTargetsForACountryForAYearResult;
        private ConsumptionResources _excess;
        private ConsumptionResources _scores;
        private ConsumptionResources _targets;
        private ConsumptionResources _recorded;


        public GameFlowServiceTests()
        {
            _getGameInformationForACountryResult = new GameInformationResult
            {
                Id = Guid.Parse("81a130d2-502f-4cf1-a376-63edeb000e9f"),
                CurrentYear = 2
            };
            _getBreakEvenForACountryResult = new ConsumptionResources();
            _getTargetsForACountryForAYearResult = new ConsumptionResources();
            _excess = new ConsumptionResources();
            _scores = new ConsumptionResources();
            _targets = new ConsumptionResources();
            _recorded = new ConsumptionResources();

            _mockIGameDataService = new Mock<IGameDataService>();
            _mockIGameDataService.Setup(m => m.GetGameInformationForACountry("cId")).ReturnsAsync(() => _getGameInformationForACountryResult);
            _mockIGameDataService.Setup(m => m.GetBreakEvenForACountry("cId")).ReturnsAsync(() => _getBreakEvenForACountryResult);
            _mockIGameDataService.Setup(m => m.GetTargetsForACountryForAYear("cId", 2)).ReturnsAsync(() => _getTargetsForACountryForAYearResult);
            _mockIGameDataService.Setup(m => m.SetCountryYearExcess("cId", 2, _excess)).Returns(() => Task.CompletedTask);
            _mockIGameDataService.Setup(m => m.SetCountryYearScores("cId", 2, _scores)).Returns(() => Task.CompletedTask);
            _mockIGameDataService.Setup(m => m.SetCountryYearTargets("cId", 2, _targets)).Returns(() => Task.CompletedTask);

            _mockIGameScoreService = new Mock<IGameScoreService>();
            _mockIGameScoreService.Setup(m => m.CalculateYearValues(_getBreakEvenForACountryResult, _getTargetsForACountryForAYearResult, _recorded, out _excess, out _scores, out _targets));

            _mockIGameHubService = new Mock<IGameHubService>();
            _mockIGameHubService.Setup(m => m.ScoresUpdated("81a130d2-502f-4cf1-a376-63edeb000e9f", "cId", 2, _scores)).Returns(() => Task.CompletedTask);

            _gameFlowService = new GameFlowService(_mockIGameDataService.Object, _mockIGameHubService.Object, _mockIGameScoreService.Object);
        }

        [Fact]
        public async void ExecuteUpdateScoreFlow_Should_BroadCastTheScoresToTheGameHub()
        {
            await _gameFlowService.ExecuteUpdateScoreFlow("cId", _recorded);

            _mockIGameHubService.Verify(m => m.ScoresUpdated("81a130d2-502f-4cf1-a376-63edeb000e9f", "cId", 2, _scores));
        }

        [Fact]
        public async void ExecuteUpdateScoreFlow_Should_ReturnTheScoringInformation()
        {
            var res = await _gameFlowService.ExecuteUpdateScoreFlow("cId", _recorded);

            Assert.Same(_excess, res.Excess);
            Assert.Same(_scores, res.Scores);
            Assert.Same(_targets, res.NextYearTarget);
        }
    }
}