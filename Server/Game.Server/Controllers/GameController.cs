using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Game.Server.Models;
using Game.Server.Services;
using Game.Server.Services.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Game.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GameController
    {
        private readonly IGameFlowService _gameFlowService;
        private readonly IGameDataService _gameDataService;

        public GameController(IGameFlowService gameFlowService, IGameDataService gameDataService)
        {
            _gameFlowService = gameFlowService;
            _gameDataService = gameDataService;
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

        [HttpGet]
        public Task<List<GameSearchResult>> Games()
        {
            return _gameDataService.GetListOfGames();
        }
    }
}