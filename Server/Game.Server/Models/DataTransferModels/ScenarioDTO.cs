using System.Collections.Generic;

namespace Game.Server.Models.DataTransferModels
{
    public class ScenarioDTO
    {
        public string Id { get; set; }
        public string Author { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }
        public IEnumerable<ScenarioCountry> Countries { get; set; }
    }

    public class ScenarioCountry
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int TargetScore { get; set; }
        public BaselineProduce Produce { get; set; }
        public BaselineTargets Targets { get; set; }
    }

    public class BaselineProduce
    {
        public int Grain { get; set; }
        public int Meat { get; set; }
        public int Oil { get; set; }
        public int Cocoa { get; set; }
        public int Cotton { get; set; }
    }

    public class BaselineTargets
    {
        public int Grain { get; set; }
        public int Meat { get; set; }
        public int Energy { get; set; }
        public int Chocolate { get; set; }
        public int Textiles { get; set; }
    }
}