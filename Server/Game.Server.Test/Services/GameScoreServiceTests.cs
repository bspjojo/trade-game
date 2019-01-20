using System.Collections.Concurrent;
using Game.Server.Services;
using Game.Server.Services.Models;
using Xunit;

namespace Game.Server.Test.Services
{
    public class GameScoreServiceTests
    {
        [Fact]
        public void CalculateYearValues_Should_CalculateTargetsForResources()
        {
            var country = new GameCountry
            {
                Years = new ConcurrentDictionary<int, CountryYear>()
            };

            country.Years[0] = new CountryYear
            {
                Targets = new CountryYearConsumptionResourceValues
                {
                    Chocolate = 5,
                    Energy = 5,
                    Grain = 5,
                    Meat = 5,
                    Textiles = 5
                }
            };

            country.Years[1] = new CountryYear();

            var recordedScore = new CountryYearConsumptionResourceValues
            {
                Chocolate = 4,
                Energy = 3,
                Grain = 2,
                Meat = 6,
                Textiles = 5
            };

            var gss = new GameScoreService();
            gss.CalculateYearValues(0, country, recordedScore);

            var calcd = country.Years[1].Targets;

            Assert.Equal(6, calcd.Chocolate);
            Assert.Equal(7, calcd.Energy);
            Assert.Equal(8, calcd.Grain);
            Assert.Equal(5, calcd.Meat);
            Assert.Equal(5, calcd.Textiles);
        }

        [Fact]
        public void CalculateYearValues_Should_CalculateScoresForResources()
        {
            var country = new GameCountry
            {
                Years = new ConcurrentDictionary<int, CountryYear>()
            };

            country.Years[0] = new CountryYear
            {
                Targets = new CountryYearConsumptionResourceValues
                {
                    Chocolate = 5,
                    Energy = 5,
                    Grain = 5,
                    Meat = 5,
                    Textiles = 5
                }
            };

            country.Years[1] = new CountryYear();

            var recordedScore = new CountryYearConsumptionResourceValues
            {
                Chocolate = 4,
                Energy = 3,
                Grain = 2,
                Meat = 6,
                Textiles = 5
            };

            var gss = new GameScoreService();
            gss.CalculateYearValues(0, country, recordedScore);

            var calcd = country.Years[0].Scores;

            Assert.Equal(-1, calcd.Chocolate);
            Assert.Equal(-2, calcd.Energy);
            Assert.Equal(-3, calcd.Grain);
            Assert.Equal(1, calcd.Meat);
            Assert.Equal(0, calcd.Textiles);
        }
    }
}