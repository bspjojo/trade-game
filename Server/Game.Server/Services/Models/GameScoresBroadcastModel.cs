using System;
using System.Collections.Generic;

namespace Game.Server.Services.Models
{
    public class GameScoresBroadcastModel
    {
        public Guid Id { get; set; }
        public int Duration { get; set; }
        public int CurrentYear { get; set; }
        public List<ScenarioCountry> Countries { get; set; }
        public string Name { get; set; }
    }

    public class ScenarioCountry
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int TargetScore { get; set; }
        public int CurrentScore { get; set; }
        public List<ConsumptionResourcesForAYear> Scores;
    }

    public class ConsumptionResourcesForAYear : ConsumptionResources
    {
        public int Year { get; set; }
        public int Score { get; set; }
    }
}