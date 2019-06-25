using System;

namespace Game.Server.Models.DataTransferModels
{
    public class CreateGameFromScenarioDTO
    {
        public Guid ScenarioId { get; set; }
        public string Name { get; set; }
    }
}