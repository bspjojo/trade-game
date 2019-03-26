using System;
using System.Threading.Tasks;
using Game.Server.Services.Models;
using Game.Server.DataRepositories;

namespace Game.Server.Services
{
    public interface IGameFlowService
    {
        Task<ScoreServiceResult> ExecuteUpdateScoreFlow(string countryId, ConsumptionResources consumptionResourcesRecorded);
    }

    public class GameFlowService : IGameFlowService
    {
        private readonly IGameDataService _gameDataService;
        private readonly IGameHubService _gameHubService;
        private readonly IGameScoreService _gameScoreService;
        private readonly IGameUpdatedService _gameUpdatedService;

        public GameFlowService(IGameDataService gameDataService, IGameHubService gameHubService, IGameScoreService gameScoreService, IGameUpdatedService gameUpdatedService)
        {
            _gameDataService = gameDataService;
            _gameHubService = gameHubService;
            _gameScoreService = gameScoreService;
            _gameUpdatedService = gameUpdatedService;
        }

        public async Task<ScoreServiceResult> ExecuteUpdateScoreFlow(string countryId, ConsumptionResources consumptionResourcesRecorded)
        {
            var gameInformation = await _gameDataService.GetGameInformationForACountry(countryId);

            var breakEven = await _gameDataService.GetBreakEvenForACountry(countryId);
            var targetsForCurrentYear = await _gameDataService.GetTargetsForACountryForAYear(countryId, gameInformation.CurrentYear);

            _gameScoreService.CalculateYearValues(breakEven, targetsForCurrentYear, consumptionResourcesRecorded, out var currentYearExcess, out var currentYearScores, out var nextYearTargets);

            await _gameDataService.SetCountryYearExcess(countryId, gameInformation.CurrentYear, currentYearExcess);
            await _gameDataService.SetCountryYearScores(countryId, gameInformation.CurrentYear, currentYearScores);
            await _gameDataService.SetCountryYearTargets(countryId, gameInformation.CurrentYear + 1, nextYearTargets);

            _gameUpdatedService.GameUpdated(gameInformation.Id.ToString());

            return new ScoreServiceResult
            {
                NextYearTarget = nextYearTargets,
                Excess = currentYearExcess,
                Scores = currentYearScores
            };
        }
    }
}