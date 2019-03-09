
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Game.Server.Services.Models;

namespace Game.Server.DataRepositories
{
    public interface IGameDataService
    {
        Task<ConsumptionResources> GetBreakEvenForACountry(string countryId);
        Task<GameInformationResult> GetGameInformationForACountry(string countryId);
        Task<ConsumptionResources> GetTargetsForACountryForAYear(string countryId, int year);
        Task<IEnumerable<GameSearchResult>> GetListOfActiveGames();
        Task<IEnumerable<CountrySearchResult>> GetListOfCountriesInGame(Guid gameId);
    }
}