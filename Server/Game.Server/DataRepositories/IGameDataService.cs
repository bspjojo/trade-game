
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Game.Server.Services.Models;

namespace Game.Server.DataRepositories
{
    public interface IGameDataService
    {
        Task<GameCountry> GetCountryById(string countryId);
        Task<IEnumerable<GameSearchResult>> GetListOfActiveGames();
        Task<IEnumerable<CountrySearchResult>> GetListOfCountriesInGame(Guid gameId);
    }
}