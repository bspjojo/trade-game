using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Game.Server.DataRepositories;
using Game.Server.Models.DataTransferModels;
using Microsoft.Extensions.Logging;

namespace Game.Server.Services
{
    public interface IScenarioService
    {
        Task<ScenarioDTO> Update(ScenarioDTO scenarioIn);
        Task<IEnumerable<ScenarioSummaryDTO>> List();
    }

    public class ScenarioService : IScenarioService
    {
        private readonly ILogger<ScenarioService> _logger;
        private readonly IScenarioDataService _sqlScenarioDataService;

        public ScenarioService(ILogger<ScenarioService> logger, IScenarioDataService sqlScenarioDataService)
        {
            _logger = logger;
            _sqlScenarioDataService = sqlScenarioDataService;
        }

        public Task<IEnumerable<ScenarioSummaryDTO>> List()
        {
            _logger.LogInformation("Getting list of scenario summaries.");

            return _sqlScenarioDataService.List();
        }

        public Task<ScenarioDTO> Update(ScenarioDTO scenarioIn)
        {
            if (string.IsNullOrWhiteSpace(scenarioIn.Id))
            {
                _logger.LogInformation("Creating scenario.");
                return _sqlScenarioDataService.CreateScenario(scenarioIn);
            }
            else
            {
                _logger.LogInformation($"Updating scenario {scenarioIn.Id}.");

                throw new NotImplementedException("Updating a scenario is not currently supported");
            }
        }
    }
}