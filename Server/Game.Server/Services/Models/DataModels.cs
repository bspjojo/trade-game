using System.Collections.Generic;

namespace Game.Server.Services.Models
{
    public class ScoreServiceResult
    {
        public ConsumptionResources NextYearTarget { get; set; }
        public ConsumptionResources Excess { get; set; }
        public ConsumptionResources Scores { get; set; }
    }

    public class ConsumptionResources
    {
        public int Energy { get; set; }
        public int Chocolate { get; set; }
        public int Meat { get; set; }
        public int Grain { get; set; }
        public int Textiles { get; set; }
    }

    public class ProductionResources
    {
        public int Oil { get; set; }
        public int Cocoa { get; set; }
        public int Meat { get; set; }
        public int Grain { get; set; }
        public int Cotton { get; set; }
    }
}