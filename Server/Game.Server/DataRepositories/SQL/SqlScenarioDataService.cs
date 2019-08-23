using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Game.Server.Models;
using Game.Server.Models.DataTransferModels;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Game.Server.DataRepositories.SQL
{
    public class SqlScenarioDataService : IScenarioDataService
    {
        private readonly string _connectionString;
        private readonly ILogger<SqlScenarioDataService> _logger;

        public SqlScenarioDataService(ILogger<SqlScenarioDataService> logger, IOptions<DatabaseConnections> dataConnections)
        {
            _logger = logger;
            _connectionString = dataConnections.Value.TradeGame;
        }

        public async Task<ScenarioDTO> CreateScenario(ScenarioDTO scenarioIn)
        {
            _logger.LogInformation($"Creating new scenario {scenarioIn.Name}");

            var createScenarioSql = @"DECLARE @NewScenarioVar table(ID UNIQUEIDENTIFIER);
                                      
                                      INSERT INTO dbo.Scenarios
                                          (Name,DateCreated,Duration,Author)
                                          OUTPUT inserted.ID INTO @NewScenarioVar
                                      VALUES
                                          (@Name, @DateCreated, @Duration, @Author)
                                      
                                      SELECT TOP 1
                                          ID
                                      FROM @NewScenarioVar";

            var insertCountrySql = @"DECLARE @NewCountryVar table(ID UNIQUEIDENTIFIER);

                                    INSERT INTO dbo.Scenario_Countries
                                        (ScenarioID, Name, TargetScore, Produce_Grain, Produce_Meat, Produce_Oil, Produce_Cocoa, Produce_Cotton, Target_Grain, Target_Meat, Target_Energy, Target_Chocolate, Target_Textiles)
                                        OUTPUT inserted.ID INTO @NewCountryVar
                                    VALUES
                                        (@ScenarioId, @CountryName, @TargetScore, @Produce_Grain, @Produce_Meat, @Produce_Oil, @Produce_Cocoa, @Produce_Cotton, @Target_Grain, @Target_Meat, @Target_Energy, @Target_Chocolate, @Target_Textiles)
                                    
                                    SELECT TOP 1
                                        ID
                                    FROM @NewCountryVar";

            ScenarioDTO scenarioOut = new ScenarioDTO();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var scenarioId = await connection.QueryFirstAsync<IdQuery>(createScenarioSql, new
                {
                    Name = scenarioIn.Name,
                    DateCreated = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture),
                    Duration = scenarioIn.Duration,
                    Author = scenarioIn.Author
                });

                scenarioOut.Id = scenarioId.ID.ToString();
                scenarioOut.Name = scenarioIn.Name;
                scenarioOut.Duration = scenarioIn.Duration;
                scenarioOut.Author = scenarioIn.Author;
                var outCountries = new List<ScenarioCountry>();

                foreach (var country in scenarioIn.Countries)
                {
                    _logger.LogInformation($"Inserting country {country.Name}");

                    var countryId = await connection.QueryFirstAsync<IdQuery>(insertCountrySql, new
                    {
                        ScenarioId = scenarioId.ID,
                        CountryName = country.Name,
                        TargetScore = country.TargetScore,
                        Produce_Grain = country.Produce.Grain,
                        Produce_Meat = country.Produce.Meat,
                        Produce_Oil = country.Produce.Oil,
                        Produce_Cocoa = country.Produce.Cocoa,
                        Produce_Cotton = country.Produce.Cotton,
                        Target_Grain = country.Targets.Grain,
                        Target_Meat = country.Targets.Meat,
                        Target_Energy = country.Targets.Energy,
                        Target_Chocolate = country.Targets.Chocolate,
                        Target_Textiles = country.Targets.Textiles
                    });

                    country.Id = countryId.ID.ToString();
                    outCountries.Add(country);
                }

                scenarioOut.Countries = outCountries;

                connection.Close();
            }

            return scenarioOut;
        }

        public async Task<IEnumerable<ScenarioSummaryDTO>> List()
        {
            _logger.LogInformation("Listing scenarios");

            var getScenariosSql = @"SELECT ID
                                        , [Name]
                                        , DateCreated
                                        , Duration
                                        , Author
                                    FROM dbo.Scenarios";

            IEnumerable<ScenarioSummaryDTO> scenarioSummaries = new List<ScenarioSummaryDTO>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                scenarioSummaries = await connection.QueryAsync<ScenarioSummaryDTO>(getScenariosSql);

                connection.Close();
            }

            _logger.LogInformation($"Found {scenarioSummaries.Count()} scenarios");

            return scenarioSummaries;
        }

        public async Task<ScenarioDTO> UpdateScenario(ScenarioDTO scenarioIn)
        {
            throw new NotImplementedException(nameof(UpdateScenario));
        }
    }
}