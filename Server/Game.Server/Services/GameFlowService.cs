using System.Threading.Tasks;
using Game.Server.Services.Models;
using Game.Server.DataRepositories;
using Game.Server.Models;
using Microsoft.Extensions.Logging;

namespace Game.Server.Services
{
    public interface IGameFlowService
    {
        Task<ScoreServiceResult> ExecuteUpdateScoreFlow(string countryId, ConsumptionResources consumptionResourcesRecorded);
        Task UpdateGameYear(UpdateYear updateInformation);
    }

    public class GameFlowService : IGameFlowService
    {
        private readonly IGameDataService _gameDataService;
        private readonly IGameScoreService _gameScoreService;
        private readonly IGameUpdatedService _gameUpdatedService;
        private readonly ILogger<GameFlowService> _logger;

        public GameFlowService(ILogger<GameFlowService> logger, IGameDataService gameDataService, IGameScoreService gameScoreService, IGameUpdatedService gameUpdatedService)
        {
            _gameDataService = gameDataService;
            _gameScoreService = gameScoreService;
            _gameUpdatedService = gameUpdatedService;
            _logger = logger;
        }

        public async Task<ScoreServiceResult> ExecuteUpdateScoreFlow(string countryId, ConsumptionResources consumptionResourcesRecorded)
        {
            _logger.LogInformation($"{nameof(ExecuteUpdateScoreFlow)} for {countryId}");

            var gameInformation = await _gameDataService.GetGameInformationForACountry(countryId);

            var breakEven = await _gameDataService.GetBreakEvenForACountry(countryId);
            var targetsForCurrentYear = await _gameDataService.GetTargetsForACountryForAYear(countryId, gameInformation.CurrentYear);

            _logger.LogInformation($"{countryId}:{gameInformation.CurrentYear} calculating year values.");
            _gameScoreService.CalculateYearValues(breakEven, targetsForCurrentYear, consumptionResourcesRecorded, out var currentYearExcess, out var currentYearScores, out var nextYearTargets);
            _logger.LogInformation($"{countryId}:{gameInformation.CurrentYear} calculating year values complete.");

            await _gameDataService.SetCountryYearExcess(countryId, gameInformation.CurrentYear, currentYearExcess);
            await _gameDataService.SetCountryYearScores(countryId, gameInformation.CurrentYear, currentYearScores);
            await _gameDataService.SetCountryYearTargets(countryId, gameInformation.CurrentYear + 1, nextYearTargets);

            _logger.LogInformation($"Game score flow executed.");

            await _gameUpdatedService.GameUpdated(gameInformation.Id.ToString());

            return new ScoreServiceResult
            {
                NextYearTarget = nextYearTargets,
                Excess = currentYearExcess,
                Scores = currentYearScores
            };
        }

        public async Task UpdateGameYear(UpdateYear updateInformation)
        {
            _logger.LogInformation($"Setting game year for {updateInformation.GameId} to {updateInformation.Year}");

            await _gameDataService.UpdateCurrentYearForGame(updateInformation.GameId, updateInformation.Year);
            _gameUpdatedService.GameUpdated(updateInformation.GameId);
        }
    }
}