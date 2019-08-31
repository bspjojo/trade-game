using System.Collections.Concurrent;
using Game.Server.Services;
using Game.Server.Services.Models;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Game.Server.Test.Services
{
    public class GameScoreServiceTests
    {
        [Fact]
        public void CalculateYearValues_Should_CalculateTargetsForResources()
        {
            var targets = new ConsumptionResources
            {
                Chocolate = 5,
                Energy = 5,
                Grain = 5,
                Meat = 5,
                Textiles = 5
            };

            var breakEven = new ConsumptionResources
            {
                Chocolate = 5,
                Energy = 5,
                Grain = 5,
                Meat = 5,
                Textiles = 5
            };

            var consumptionRecorded = new ConsumptionResources
            {
                Chocolate = 4,
                Energy = 3,
                Grain = 2,
                Meat = 6,
                Textiles = 5
            };

            var mockLogger = new Mock<ILogger<GameScoreService>>();
            var gss = new GameScoreService(mockLogger.Object);
            gss.CalculateYearValues(breakEven, targets, consumptionRecorded, out var excess, out var scores, out var nextYearTargets);

            Assert.Equal(6, nextYearTargets.Chocolate);
            Assert.Equal(7, nextYearTargets.Energy);
            Assert.Equal(8, nextYearTargets.Grain);
            Assert.Equal(5, nextYearTargets.Meat);
            Assert.Equal(5, nextYearTargets.Textiles);
        }

        [Fact]
        public void CalculateYearValues_Should_CalculateScoresForResources()
        {
            var targets = new ConsumptionResources
            {
                Chocolate = 5,
                Energy = 5,
                Grain = 5,
                Meat = 5,
                Textiles = 5
            };

            var breakEven = new ConsumptionResources
            {
                Chocolate = 5,
                Energy = 5,
                Grain = 5,
                Meat = 5,
                Textiles = 5
            };

            var consumptionRecorded = new ConsumptionResources
            {
                Chocolate = 4,
                Energy = 3,
                Grain = 2,
                Meat = 6,
                Textiles = 5
            };

            var mockLogger = new Mock<ILogger<GameScoreService>>();
            var gss = new GameScoreService(mockLogger.Object);
            gss.CalculateYearValues(breakEven, targets, consumptionRecorded, out var excess, out var scores, out var nextYearTargets);

            Assert.Equal(-1, scores.Chocolate);
            Assert.Equal(-2, scores.Energy);
            Assert.Equal(-3, scores.Grain);
            Assert.Equal(1, scores.Meat);
            Assert.Equal(0, scores.Textiles);
        }

        [Fact]
        public void CalculateYearValues_Should_CalculateExcessesForResources()
        {
            var targets = new ConsumptionResources
            {
                Chocolate = 5,
                Energy = 5,
                Grain = 5,
                Meat = 5,
                Textiles = 5
            };

            var breakEven = new ConsumptionResources
            {
                Chocolate = 5,
                Energy = 5,
                Grain = 5,
                Meat = 5,
                Textiles = 5
            };

            var consumptionRecorded = new ConsumptionResources
            {
                Chocolate = 4,
                Energy = 3,
                Grain = 2,
                Meat = 6,
                Textiles = 5
            };

            var mockLogger = new Mock<ILogger<GameScoreService>>();
            var gss = new GameScoreService(mockLogger.Object);
            gss.CalculateYearValues(breakEven, targets, consumptionRecorded, out var excess, out var scores, out var nextYearTargets);

            Assert.Equal(0, excess.Chocolate);
            Assert.Equal(0, excess.Energy);
            Assert.Equal(0, excess.Grain);
            Assert.Equal(1, excess.Meat);
            Assert.Equal(0, excess.Textiles);
        }
    }
}