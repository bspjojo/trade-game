using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Game.Server.Models;
using Game.Server.Models.DataAccessModels;
using Game.Server.Services.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Game.Server.DataRepositories
{
    public class SqlGameDataService : IGameDataService
    {
        private readonly string _connectionString;
        public ILogger<SqlGameDataService> _logger { get; }

        public SqlGameDataService(ILogger<SqlGameDataService> logger, IOptions<GameConnection> gameConnectionInformation)
        {
            _logger = logger;
            _connectionString = gameConnectionInformation.Value.GameDb;
        }

        public Task<GameCountry> GetCountryById(string countryId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<CountrySearchResult>> GetListOfCountriesInGame(Guid gameId)
        {
            _logger.LogInformation("Getting active games.");

            var selectCountriesInGame = @"SELECT GameCountries.ID
                                        , ScenarioCountries.Name
                                        FROM dbo.Game_Countries [GameCountries]
                                            JOIN dbo.Scenario_Countries [ScenarioCountries] ON ScenarioCountries.ID = GameCountries.ScenarioCountryID
                                        WHERE GameID = @GameId";

            IEnumerable<GameCountryDAO> results = null;

            using (var connection = new SqlConnection(_connectionString))
            {

                connection.Open();
                results = await connection.QueryAsync<GameCountryDAO>(selectCountriesInGame, new
                {
                    GameId = gameId
                });
                connection.Close();
            }

            _logger.LogInformation($"Found {results.Count()} active games.");

            return results.Select(item => new CountrySearchResult
            {
                Id = item.Id,
                Name = item.Name
            });
        }

        public async Task<IEnumerable<GameSearchResult>> GetListOfActiveGames()
        {
            _logger.LogInformation("Getting active games.");

            var selectActiveGamesQuery = @"SELECT ID
                                        , [Name]
                                        , [DateStarted]
                                        FROM dbo.Games
                                        WHERE Active = 1";

            IEnumerable<GameSearchDAO> results = null;

            using (var connection = new SqlConnection(_connectionString))
            {

                connection.Open();
                results = await connection.QueryAsync<GameSearchDAO>(selectActiveGamesQuery);
                connection.Close();
            }

            _logger.LogInformation($"Found {results.Count()} active games.");

            return results.Select(item => new GameSearchResult
            {
                Id = item.Id,
                Name = item.Name,
                DateStarted = item.DateStarted
            });
        }
    }
}