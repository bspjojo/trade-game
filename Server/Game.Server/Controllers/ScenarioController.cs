using System.Threading.Tasks;
using Game.Server.Models.DataTransferModels;
using Microsoft.AspNetCore.Mvc;

namespace Game.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ScenarioController
    {
        [HttpPost]
        public async Task<IActionResult> Update([FromBody] ScenarioDTO scenario)
        {
            var updatedScenario = scenario;

            return new OkObjectResult(updatedScenario);
        }
    }
}