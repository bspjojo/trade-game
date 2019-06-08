using System;
using System.Threading.Tasks;
using Game.Server.DataRepositories.SQL;
using Game.Server.Models.DataTransferModels;

namespace Game.Server.Services
{
    public interface IScenarioUpdateService
    {
        Task<ScenarioDTO> Update(ScenarioDTO scenarioIn);
    }

    public class ScenarioUpdateService : IScenarioUpdateService
    {
        private readonly IScenarioDataService _sqlScenarioDataService;

        public ScenarioUpdateService(IScenarioDataService sqlScenarioDataService)
        {
            _sqlScenarioDataService = sqlScenarioDataService;
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