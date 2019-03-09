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

        public async Task<ConsumptionResources> GetBreakEvenForACountry(string countryId)
        {
            _logger.LogInformation("Getting break even for country with id games.");

            var selectBreakEvenForACountryQuery = @"SELECT BreakEven.Chocolate
                                            , BreakEven.Energy
                                            , BreakEven.Grain
                                            , BreakEven.Meat
                                            , BreakEven.Textiles
                                        FROM dbo.BaseLine_Scenario_Country_Targets as BreakEven
                                            JOIN dbo.Scenario_Countries AS ScenarioCountries
                                            ON ScenarioCountries.ID = BreakEven.ScenarioCountryID
                                            JOIN dbo.Game_Countries AS GameCountries
                                            ON ScenarioCountries.ID = GameCountries.ScenarioCountryID
                                        WHERE GameCountries.ID = @CountryId";

            ConsumptionResourceDAO result = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                result = await connection.QueryFirstAsync<ConsumptionResourceDAO>(selectBreakEvenForACountryQuery, new
                {
                    CountryId = countryId
                });
                connection.Close();
            }

            return new ConsumptionResources
            {
                Meat = result.Meat,
                Chocolate = result.Chocolate,
                Textiles = result.Textiles,
                Energy = result.Energy,
                Grain = result.Grain
            };
        }

        public Task<ConsumptionResources> GetTargetsForACountryForAYear(string countryId, int year)
        {
            //ConsumptionResourceDAO
            throw new NotImplementedException();
        }

        public async Task<GameInformationResult> GetGameInformationForACountry(string countryId)
        {
            var gameInformationQuery = @"SELECT dbo.Games.ID
                                            , dbo.Games.CurrentYear
                                        FROM dbo.Games JOIN dbo.Game_Countries ON dbo.Games.ID = dbo.Game_Countries.GameID
                                        WHERE dbo.Game_Countries.ID = @CountryId";

            GameInformationDAO result = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                result = await connection.QueryFirstAsync<GameInformationDAO>(gameInformationQuery, new
                {
                    CountryId = countryId
                });
                connection.Close();
            }

            return new GameInformationResult
            {
                Name = result.Name,
                CurrentYear = result.CurrentYear,
                Id = result.Id
            };
        }
    }
}