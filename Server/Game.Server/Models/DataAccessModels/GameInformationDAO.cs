using System;

namespace Game.Server.Services.Models
{
    public class GameInformationDAO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int CurrentYear { get; set; }
    }
}