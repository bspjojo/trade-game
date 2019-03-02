using System.Collections.Generic;
using System.Threading.Tasks;
using Game.Server.Models.DataAccessModels;
using Game.Server.Services.Models;

namespace Game.Server.DataRepositories
{
    public interface IGameDataService
    {
        Task<GameCountry> GetCountryById(string countryId);
        Task<IEnumerable<GameSearchResult>> GetListOfActiveGames();
        Task<List<CountrySearchResult>> GetListOfCountriesInGame(string gameId);
    }
}