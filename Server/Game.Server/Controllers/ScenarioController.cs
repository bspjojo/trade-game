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
        private readonly IScenarioUpdateService _scenarioUpdateService;

        public ScenarioController(IScenarioUpdateService scenarioUpdateService)
        {
            _scenarioUpdateService = scenarioUpdateService;
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromBody] ScenarioDTO scenario)
        {
            var updatedScenario = await _scenarioUpdateService.Update(scenario);

            return new OkObjectResult(updatedScenario);
        }
    }
}