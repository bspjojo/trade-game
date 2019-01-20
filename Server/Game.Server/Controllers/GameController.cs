using System;
using System.Threading.Tasks;
using Game.Server.Models;
using Game.Server.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Game.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GameController
    {
        private readonly IGameFlowService _gameFlowService;

        public GameController(IGameFlowService gameFlowService)
        {
            _gameFlowService = gameFlowService;
        }

        [HttpPost]
        public async Task<ScorerClientScoreUpdateResponse> UpdateScores([FromBody] ScorerClientScoreUpdate roundResults)
        {
            var scoreUpdateResult = await _gameFlowService.ExecuteUpdateScoreFlow(roundResults.GameId, roundResults.CountryId, roundResults.Year, roundResults.YearResults);

            var response = new ScorerClientScoreUpdateResponse
            {
                NextYearTarget = scoreUpdateResult.NextYearTarget,
                Excess = scoreUpdateResult.Excess
            };

            return response;
        }
    }
}