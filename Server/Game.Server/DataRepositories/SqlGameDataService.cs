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

        public async Task<ConsumptionResources> GetTargetsForACountryForAYear(string countryId, int year)
        {
            var yearTargetsForCountryForYear = @"SELECT YearTargets.Chocolate
                                                    , YearTargets.Energy
                                                    , YearTargets.Grain
                                                    , YearTargets.Meat
                                                    , YearTargets.Textiles
                                                FROM dbo.Game_Country_Year_Targets as YearTargets
                                                WHERE YearTargets.Year = @Year
                                                AND YearTargets.GameCountryID = @CountryId";

            ConsumptionResourceDAO result = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                result = await connection.QueryFirstAsync<ConsumptionResourceDAO>(yearTargetsForCountryForYear, new
                {
                    CountryId = countryId,
                    Year = year
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

        public async Task SetCountryYearExcess(string countryId, int year, ConsumptionResources excess)
        {
            var updateExcessForCountryForYear = @"BEGIN TRAN
                                                UPDATE dbo.Game_Country_Year_Excess
                                                SET Chocolate = @Chocolate, Energy = @Energy, Grain = @Grain, Meat = @Meat, Textiles = @Textiles
                                                WHERE GameCountryID = @CountryId AND Year = @Year
                                                IF @@ROWCOUNT=0
                                                    INSERT INTO dbo.Game_Country_Year_Excess
                                                    (GameCountryID, Year, Chocolate, Energy, Grain, Meat, Textiles)
                                                VALUES
                                                    (@CountryID, @Year, @Chocolate, @Energy, @Grain, @Meat, @Textiles)

                                                COMMIT TRAN";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                await connection.ExecuteAsync(updateExcessForCountryForYear, new
                {
                    CountryId = countryId,
                    Year = year,
                    Chocolate = excess.Chocolate,
                    Energy = excess.Energy,
                    Grain = excess.Grain,
                    Meat = excess.Meat,
                    Textiles = excess.Textiles
                });
                connection.Close();
            }
        }

        public async Task SetCountryYearScores(string countryId, int year, ConsumptionResources score)
        {
            var updateScoresForCountryForYear = @"BEGIN TRAN
                                                UPDATE dbo.Game_Country_Year_Score
                                                SET Chocolate = @Chocolate, Energy = @Energy, Grain = @Grain, Meat = @Meat, Textiles = @Textiles
                                                WHERE GameCountryID = @CountryId AND Year = @Year
                                                IF @@ROWCOUNT=0
                                                    INSERT INTO dbo.Game_Country_Year_Score
                                                    (GameCountryID, Year, Chocolate, Energy, Grain, Meat, Textiles)
                                                VALUES
                                                    (@CountryID, @Year, @Chocolate, @Energy, @Grain, @Meat, @Textiles)

                                                COMMIT TRAN";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                await connection.ExecuteAsync(updateScoresForCountryForYear, new
                {
                    CountryId = countryId,
                    Year = year,
                    Chocolate = score.Chocolate,
                    Energy = score.Energy,
                    Grain = score.Grain,
                    Meat = score.Meat,
                    Textiles = score.Textiles
                });
                connection.Close();
            }
        }

        public async Task SetCountryYearTargets(string countryId, int year, ConsumptionResources targets)
        {
            var updateTargetsForCountryForYear = @"BEGIN TRAN
                                                UPDATE dbo.Game_Country_Year_Targets
                                                SET Chocolate = @Chocolate, Energy = @Energy, Grain = @Grain, Meat = @Meat, Textiles = @Textiles
                                                WHERE GameCountryID = @CountryId AND Year = @Year
                                                IF @@ROWCOUNT=0
                                                    INSERT INTO dbo.Game_Country_Year_Targets
                                                    (GameCountryID, Year, Chocolate, Energy, Grain, Meat, Textiles)
                                                VALUES
                                                    (@CountryID, @Year, @Chocolate, @Energy, @Grain, @Meat, @Textiles)

                                                COMMIT TRAN";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                await connection.ExecuteAsync(updateTargetsForCountryForYear, new
                {
                    CountryId = countryId,
                    Year = year,
                    Chocolate = targets.Chocolate,
                    Energy = targets.Energy,
                    Grain = targets.Grain,
                    Meat = targets.Meat,
                    Textiles = targets.Textiles
                });
                connection.Close();
            }
        }

        public async Task<GameScoresBroadcastModel> GetGameScores(string gameId)
        {
            var queryGameScoresSql = @"SELECT GameCountries.ID as GameCountryId
                                    , ScenarioCountries.Name
                                    , ScenarioCountries.TargetScore
                                    , CountryYearScores.Year
                                    , CountryYearScores.Meat
                                    , CountryYearScores.Grain
                                    , CountryYearScores.Chocolate
                                    , CountryYearScores.Textiles
                                    , CountryYearScores.Energy
                                FROM dbo.Game_Countries AS GameCountries
                                    LEFT JOIN dbo.Scenario_Countries AS ScenarioCountries
                                    ON ScenarioCountries.ID = GameCountries.ScenarioCountryID
                                    INNER JOIN dbo.Game_Country_Year_Score AS CountryYearScores ON CountryYearScores.GameCountryID = GameCountries.ID
                                WHERE GameID = @GameId
                                ORDER BY CountryYearScores.Year DESC, ScenarioCountries.Name";

            var gameInfoSql = @"SELECT Game.Name
                                , Game.CurrentYear
                                , Scenario.Duration
                                FROM games AS Game
                                    LEFT JOIN dbo.Scenarios AS Scenario
                                    ON Scenario.ID = Game.ScenarioId
                                WHERE Game.ID = @GameID";

            IEnumerable<GameCountryScoresDAO> scoresResult = null;
            GameDAO gameResult = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                scoresResult = await connection.QueryAsync<GameCountryScoresDAO>(queryGameScoresSql, new
                {
                    GameId = gameId
                });

                gameResult = await connection.QueryFirstAsync<GameDAO>(gameInfoSql, new
                {
                    GameId = gameId
                });

                connection.Close();
            }

            var countryDictionary = new Dictionary<string, ScenarioCountry>();

            foreach (var r in scoresResult)
            {
                ScenarioCountry scenarioCountry;

                if (!countryDictionary.TryGetValue(r.GameCountryId.ToString(), out scenarioCountry))
                {
                    scenarioCountry = new ScenarioCountry
                    {
                        Id = r.GameCountryId,
                        TargetScore = r.TargetScore,
                        CurrentScore = 0,
                        Name = r.Name,
                        Scores = new List<ConsumptionResourcesForAYear>()
                    };

                    countryDictionary[r.GameCountryId.ToString()] = scenarioCountry;
                }

                var t = new ConsumptionResourcesForAYear
                {
                    Year = r.Year,
                    Meat = r.Meat,
                    Chocolate = r.Chocolate,
                    Grain = r.Grain,
                    Energy = r.Energy,
                    Textiles = r.Textiles,
                    Score = r.Meat + r.Chocolate + r.Grain + r.Energy + r.Textiles
                };

                scenarioCountry.CurrentScore += t.Score;
                scenarioCountry.Scores.Add(t);
            }

            foreach (var item in countryDictionary.Values)
            {
                item.Scores = item.Scores.OrderBy(v => v.Year).ToList();
            }

            var bm = new GameScoresBroadcastModel
            {
                Id = Guid.Parse(gameId),
                Duration = gameResult.Duration,
                Name = gameResult.Name,
                CurrentYear = gameResult.CurrentYear,
                Countries = countryDictionary.Values.ToList()
            };

            return bm;
        }

        public async Task UpdateCurrentYearForGame(string gameId, int year)
        {
            var updateYearSql = @"UPDATE Games
                                  SET CurrentYear = @NewYear
                                  WHERE Games.ID = @GameID";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                await connection.ExecuteAsync(updateYearSql, new
                {
                    NewYear = year,
                    GameID = gameId
                });

                connection.Close();
            }
        }
    }
}