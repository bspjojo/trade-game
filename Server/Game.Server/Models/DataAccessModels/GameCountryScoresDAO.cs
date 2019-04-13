using System;

namespace Game.Server.Models.DataAccessModels
{
    public class GameCountryScoresDAO
    {
        public Guid GameCountryId { get; set; }
        public string Name { get; set; }
        public int TargetScore { get; set; }
        public int Year { get; set; }
        public int Duration { get; set; }
        public int Energy { get; set; }
        public int Chocolate { get; set; }
        public int Meat { get; set; }
        public int Grain { get; set; }
        public int Textiles { get; set; }
    }
}