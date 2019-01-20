using System;
using System.Threading.Tasks;
using Game.Server.Services.Models;

namespace Game.Server.Services
{
    public interface IGameFlowService
    {
        Task<ScoreServiceResult> ExecuteUpdateScoreFlow(string gameId, string countryId, int year, CountryYearConsumptionResourceValues productionRecorded);
    }

    public class GameFlowService : IGameFlowService
    {
        private readonly IGameDataService _gameDataService;
        private readonly IGameScoreService _gameScoreService;

        public GameFlowService(IGameDataService gameDataService, IGameScoreService gameScoreService)
        {
            _gameDataService = gameDataService;
            _gameScoreService = gameScoreService;
        }

        public async Task<ScoreServiceResult> ExecuteUpdateScoreFlow(string gameId, string countryId, int year, CountryYearConsumptionResourceValues productionRecorded)
        {
            var country = await _gameDataService.GetCountryById(gameId, countryId);

            // do max year stuff
            country.Years[year + 1] = new CountryYear();

            _gameScoreService.CalculateYearValues(year, country, productionRecorded);

            return new ScoreServiceResult
            {
                NextYearTarget = country.Years[year + 1].Targets,
                Excess = CalculateExcessFromScores(country.Years[year].Scores)
            };
        }

        private CountryYearConsumptionResourceValues CalculateExcessFromScores(CountryYearConsumptionResourceValues scores)
        {
            return new CountryYearConsumptionResourceValues
            {
                Energy = CalculateExcessForScore(scores.Energy),
                Chocolate = CalculateExcessForScore(scores.Chocolate),
                Meat = CalculateExcessForScore(scores.Meat),
                Grain = CalculateExcessForScore(scores.Grain),
                Textiles = CalculateExcessForScore(scores.Textiles)
            };
        }

        private int CalculateExcessForScore(int scoreValue)
        {
            if (scoreValue > 0)
            {
                return scoreValue;
            }

            return 0;
        }
    }
}