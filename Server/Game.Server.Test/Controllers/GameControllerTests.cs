using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Game.Server.Controllers;
using Game.Server.DataRepositories;
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
        private readonly Mock<IGameDataService> _mockIGameDataService;
        private readonly GameController _controller;

        private ScoreServiceResult _executeUpdateScoreFlowResponse;
        private List<GameSearchResult> _gameSearchResults;
        private List<CountrySearchResult> _countrySearchResults;
        private GameScoresBroadcastModel _gameBroadcastModel;

        public GameControllerTests()
        {
            _mockIGameFlowService = new Mock<IGameFlowService>();
            _mockIGameFlowService
                .Setup(m => m.ExecuteUpdateScoreFlow(It.IsAny<string>(), It.IsAny<ConsumptionResources>()))
                .ReturnsAsync(() => _executeUpdateScoreFlowResponse);

            _mockIGameFlowService
                .Setup(m => m.UpdateGameYear(It.IsAny<UpdateYear>())).Returns(Task.CompletedTask);

            _gameSearchResults = new List<GameSearchResult>();
            _gameBroadcastModel = new GameScoresBroadcastModel();
            _mockIGameDataService = new Mock<IGameDataService>();
            _mockIGameDataService.Setup(m => m.GetListOfActiveGames()).ReturnsAsync(() => _gameSearchResults);
            _mockIGameDataService.Setup(m => m.GetListOfCountriesInGame(It.IsAny<Guid>())).ReturnsAsync(() => _countrySearchResults);
            _mockIGameDataService.Setup(m => m.GetGameScores("gameId")).ReturnsAsync(() => _gameBroadcastModel);

            _executeUpdateScoreFlowResponse = new ScoreServiceResult
            {
                Excess = new ConsumptionResources(),
                NextYearTarget = new ConsumptionResources(),
                Scores = new ConsumptionResources()
            };

            _controller = new GameController(_mockIGameFlowService.Object, _mockIGameDataService.Object);
        }

        #region UpdateScores
        [Fact]
        public async Task UpdateScores_ShouldCallGameFlowServiceWith_GameIdCountryIdYearYearResults()
        {
            var update = new ScorerClientScoreUpdate
            {
                CountryId = "countryId",
                YearResults = new ConsumptionResources()
            };

            await _controller.UpdateScores(update);

            _mockIGameFlowService.Verify(m => m.ExecuteUpdateScoreFlow("countryId", update.YearResults));
        }

        [Fact]
        public async Task UpdateScores_ShouldReturnTheResponseWith_NextYearTargetAndExcess()
        {
            var update = new ScorerClientScoreUpdate
            {
                CountryId = "countryId",
                YearResults = new ConsumptionResources()
            };

            var response = await _controller.UpdateScores(update);

            Assert.Same(_executeUpdateScoreFlowResponse.NextYearTarget, response.NextYearTarget);
            Assert.Same(_executeUpdateScoreFlowResponse.Excess, response.Excess);
            Assert.Same(_executeUpdateScoreFlowResponse.Scores, response.Scores);
        }

        #endregion

        #region Games

        [Fact]
        public async Task Games_ShouldReturnTheTaskFrom_GameDataService_GetListOfActiveGames()
        {
            var response = await _controller.Games();

            Assert.Same(_gameSearchResults, response);
        }

        #endregion

        #region Countries

        [Fact]
        public async Task Countries_ShouldReturnTheTaskFrom_GameDataService_GetListOfCountriesInGame()
        {
            var guid = Guid.Parse("85caca0c-00d5-47a1-9467-915a134e47db");

            var response = await _controller.Countries(guid);

            _mockIGameDataService.Setup(m => m.GetListOfCountriesInGame(guid));

            Assert.Same(_countrySearchResults, response);
        }

        #endregion

        #region Scores

        [Fact]
        public async Task Scores_ShouldReturnTheResultFromGameDataService_GetGameScores()
        {
            var response = await _controller.Scores("gameId");

            Assert.Same(_gameBroadcastModel, response);
        }

        #endregion

        #region UpdateGameYear

        [Fact]
        public async Task UpdateGameYear_ShouldCallGameFlowService_UpdateGameYearWithTheData()
        {
            var data = new UpdateYear();

            await _controller.UpdateGameYear(data);

            _mockIGameFlowService.Verify(m => m.UpdateGameYear(data), Times.Once);
        }

        #endregion
    }
}