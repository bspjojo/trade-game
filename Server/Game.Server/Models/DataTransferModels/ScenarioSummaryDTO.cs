using System;

namespace Game.Server.Models.DataTransferModels
{
    public class ScenarioSummaryDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }
    }
}