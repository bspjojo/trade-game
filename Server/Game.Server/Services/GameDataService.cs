using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Game.Server.Services.Models;

namespace Game.Server.Services
{
    public interface IGameDataService
    {
        Task<GameCountry> GetCountryById(string gameId, string countryId);
    }

    public class InMemoryGameDataService : IGameDataService
    {
        private ConcurrentDictionary<string, GameModel> _gamesDictionary;

        public InMemoryGameDataService()
        {
            _gamesDictionary = new ConcurrentDictionary<string, GameModel>();

            var game1 = new GameModel
            {
                Id = "1",
                GameCountries = new ConcurrentDictionary<string, GameCountry>()
            };

            _gamesDictionary.TryAdd("1", game1);

            var country1 = new GameCountry
            {
                Id = "1",
                Name = "country1",
                Years = new ConcurrentDictionary<int, CountryYear>()
            };

            game1.GameCountries.TryAdd("1", country1);

            var year0 = new CountryYear
            {
                Id = "0",
                Targets = new ConsumptionResources
                {
                    Energy = 4,
                    Chocolate = 4,
                    Meat = 4,
                    Grain = 4,
                    Textiles = 4
                }
            };

            country1.Years.TryAdd(0, year0);
        }

        public Task<GameCountry> GetCountryById(string gameId, string countryId)
        {
            if (_gamesDictionary.TryGetValue(gameId, out var game))
            {
                if (game.GameCountries.TryGetValue(countryId, out var country))
                {
                    return Task.FromResult(country);
                }

                throw new ArgumentOutOfRangeException(nameof(countryId), countryId, "Unknown country");
            }

            throw new ArgumentOutOfRangeException(nameof(gameId), gameId, "Unknown game");
        }
    }
}