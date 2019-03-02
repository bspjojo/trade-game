using System;
using System.Threading.Tasks;
using Game.Server.Services.Models;
using Game.Server.DataRepositories;

namespace Game.Server.Services
{
    public interface IGameFlowService
    {
        Task<ScoreServiceResult> ExecuteUpdateScoreFlow(string countryId, int year, ConsumptionResources productionRecorded);
    }

    public class GameFlowService : IGameFlowService
    {
        private readonly IGameDataService _gameDataService;
        private readonly IGameHubService _gameHubService;
        private readonly IGameScoreService _gameScoreService;

        public GameFlowService(IGameDataService gameDataService, IGameHubService gameHubService, IGameScoreService gameScoreService)
        {
            _gameDataService = gameDataService;
            _gameHubService = gameHubService;
            _gameScoreService = gameScoreService;
        }

        public async Task<ScoreServiceResult> ExecuteUpdateScoreFlow(string countryId, int year, ConsumptionResources productionRecorded)
        {
            var country = await _gameDataService.GetCountryById(countryId);

            // do max year stuff
            country.Years[year + 1] = new CountryYear();

            _gameScoreService.CalculateYearValues(year, country, productionRecorded);

            var scores = country.Years[year].Scores;
            var excess = country.Years[year].Excess;

            // await _gameHubService.ScoresUpdated(gameId, countryId, year, scores);

            return new ScoreServiceResult
            {
                NextYearTarget = country.Years[year + 1].Targets,
                Excess = excess
            };
        }
    }
}