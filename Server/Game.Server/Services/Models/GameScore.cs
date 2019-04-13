using System;
using System.Collections.Concurrent;

namespace Game.Server.Services.Models
{
    public class GameSearchResult
    {
        public Guid Id { get; set; }
        public DateTime DateStarted { get; set; }
        public string Name { get; set; }
    }

    public class CountrySearchResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class GameInformationResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int CurrentYear { get; set; }
    }

    public class CountryYear
    {
        public string Id { get; set; }
        public ConsumptionResources Targets { get; set; }
        public ConsumptionResources Scores { get; set; }
        public ConsumptionResources Excess { get; set; }
        public ProductionResources ProductionResources { get; set; }
    }
}