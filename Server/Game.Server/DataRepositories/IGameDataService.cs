
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Game.Server.Services.Models;

namespace Game.Server.DataRepositories
{
    public interface IGameDataService
    {
        Task<Guid> CreateGameFromScenarioId(Guid scenarioId, string gameName);
        Task<ConsumptionResources> GetBreakEvenForACountry(string countryId);
        Task<GameInformationResult> GetGameInformationForACountry(string countryId);
        Task<ConsumptionResources> GetTargetsForACountryForAYear(string countryId, int year);
        Task SetCountryYearExcess(string countryId, int year, ConsumptionResources excess);
        Task SetCountryYearScores(string countryId, int year, ConsumptionResources score);
        Task SetCountryYearTargets(string countryId, int year, ConsumptionResources targets);
        Task<IEnumerable<GameSearchResult>> GetListOfActiveGames();
        Task<IEnumerable<CountrySearchResult>> GetListOfCountriesInGame(Guid gameId);
        Task<GameScoresBroadcastModel> GetGameScores(string gameId);
        Task UpdateCurrentYearForGame(string gameId, int year);
    }
}