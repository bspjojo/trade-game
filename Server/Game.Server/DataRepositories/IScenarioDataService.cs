using System.Collections.Generic;
using System.Threading.Tasks;
using Game.Server.Models.DataTransferModels;

namespace Game.Server.DataRepositories
{
    public interface IScenarioDataService
    {
        Task<ScenarioDTO> CreateScenario(ScenarioDTO scenarioIn);
        Task<IEnumerable<ScenarioSummaryDTO>> List();
    }
}