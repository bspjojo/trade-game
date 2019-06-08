using System.Threading.Tasks;
using Game.Server.Models.DataTransferModels;
using Game.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace Game.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ScenarioController
    {
        private readonly IScenarioService _scenarioService;

        public ScenarioController(IScenarioService scenarioService)
        {
            _scenarioService = scenarioService;
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromBody] ScenarioDTO scenario)
        {
            var updatedScenario = await _scenarioService.Update(scenario);

            return new OkObjectResult(updatedScenario);
        }

        public async Task<IActionResult> List()
        {
            var scenarioSummaries = await _scenarioService.List();

            return new OkObjectResult(scenarioSummaries);
        }
    }
}