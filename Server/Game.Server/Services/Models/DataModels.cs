using System.Collections.Generic;

namespace Game.Server.Services.Models
{
    public class ScoreServiceResult
    {
        public CountryYearConsumptionResourceValues NextYearTarget { get; set; }
        public CountryYearConsumptionResourceValues Excess { get; set; }
    }

    public class CountryYearConsumptionResourceValues
    {
        public int Energy { get; set; }
        public int Chocolate { get; set; }
        public int Meat { get; set; }
        public int Grain { get; set; }
        public int Textiles { get; set; }
    }

    public class CountryYearProductionResources
    {
        public int Oil { get; set; }
        public int Cocoa { get; set; }
        public int Meat { get; set; }
        public int Grain { get; set; }
        public int Cotton { get; set; }
    }
}