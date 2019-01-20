using System;
using System.Collections.Concurrent;

namespace Game.Server.Services.Models
{
    public class GameModel
    {
        public string Id { get; set; }
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
        public CountryYearConsumptionResourceValues Targets { get; set; }
        public CountryYearConsumptionResourceValues Scores { get; set; }
        //public CountryYearProductionResources ProductionResources { get; set; }
    }

    public class ScoreUpdatedBroadcastModel
    {
        public string CountryId { get; set; }
        public int Year { get; set; }
        public CountryYearConsumptionResourceValues Scores { get; set; }
    }
}