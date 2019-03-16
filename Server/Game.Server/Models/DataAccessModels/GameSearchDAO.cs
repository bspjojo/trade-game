using System;

namespace Game.Server.Models.DataAccessModels
{
    public class GameSearchDAO
    {
        public Guid Id { get; set; }
        public DateTime DateStarted { get; set; }
        public string Name { get; set; }
    }
}