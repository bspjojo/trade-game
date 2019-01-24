using System;
using System.Collections.Concurrent;

namespace Game.Server.Services.Models
{
    public class GameSearchResult
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class GameModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime GameDate { get; set; }
        public ConcurrentDictionary<string, GameCountry> GameCountries { get; set; }
    }

    public class GameCountry
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ConcurrentDictionary<int, CountryYear> Years { get; set; }
    }

    public class CountryYear
    {
        public string Id { get; set; }
        public ConsumptionResources Targets { get; set; }
        public ConsumptionResources Scores { get; set; }
        public ConsumptionResources Excess { get; set; }
        public ProductionResources ProductionResources { get; set; }
    }

    public class ScoreUpdatedBroadcastModel
    {
        public string CountryId { get; set; }
        public int Year { get; set; }
        public ConsumptionResources Scores { get; set; }
    }
}