using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Game.Server.DataRepositories;
using Game.Server.Models.DataTransferModels;

namespace Game.Server.Services
{
    public interface IScenarioService
    {
        Task<ScenarioDTO> Update(ScenarioDTO scenarioIn);
        Task<IEnumerable<ScenarioSummaryDTO>> List();
    }

    public class ScenarioService : IScenarioService
    {
        private readonly IScenarioDataService _sqlScenarioDataService;

        public ScenarioService(IScenarioDataService sqlScenarioDataService)
        {
            _sqlScenarioDataService = sqlScenarioDataService;
        }

        public Task<IEnumerable<ScenarioSummaryDTO>> List()
        {
            return _sqlScenarioDataService.List();
        }

        public Task<ScenarioDTO> Update(ScenarioDTO scenarioIn)
        {
            if (string.IsNullOrWhiteSpace(scenarioIn.Id))
            {
                return _sqlScenarioDataService.CreateScenario(scenarioIn);
            }
            else
            {
                throw new NotImplementedException("Updating a scenario is not currently supported");
            }
        }
    }
}