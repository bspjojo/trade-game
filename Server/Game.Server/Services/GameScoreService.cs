using System;
using Game.Server.Services.Models;
using Microsoft.Extensions.Logging;

namespace Game.Server.Services
{
    public interface IGameScoreService
    {
        void CalculateYearValues(ConsumptionResources breakEven, ConsumptionResources currentYearTargets, ConsumptionResources consumptionResourcesRecorded, out ConsumptionResources currentYearExcess, out ConsumptionResources currentYearScores, out ConsumptionResources nextYearTargets);
    }

    public class GameScoreService : IGameScoreService
    {
        private readonly ILogger<GameScoreService> _logger;
        public GameScoreService(ILogger<GameScoreService> logger)
        {
            _logger = logger;
        }

        public void CalculateYearValues(ConsumptionResources breakEven, ConsumptionResources currentYearTargets, ConsumptionResources consumptionResourcesRecorded, out ConsumptionResources currentYearExcess, out ConsumptionResources currentYearScores, out ConsumptionResources nextYearTargets)
        {
            nextYearTargets = new ConsumptionResources();
            currentYearScores = new ConsumptionResources();
            currentYearExcess = new ConsumptionResources();

            CalculateScoresAndTargetsForCountryYearResources(breakEven, currentYearTargets, consumptionResourcesRecorded, nextYearTargets, currentYearScores, currentYearExcess);
        }

        private void CalculateScoresAndTargetsForCountryYearResources(
            ConsumptionResources breakEvenTarget,
            ConsumptionResources consumptionTarget,
            ConsumptionResources consumptionsProduced,
            ConsumptionResources nextYearTargets,
            ConsumptionResources currentYearScores,
            ConsumptionResources excesses)
        {
            CalculateValuesForResource("Grain", breakEvenTarget.Grain, consumptionTarget.Grain, consumptionsProduced.Grain, v => nextYearTargets.Grain = v, v => currentYearScores.Grain = v, v => excesses.Grain = v);
            CalculateValuesForResource("Meat", breakEvenTarget.Meat, consumptionTarget.Meat, consumptionsProduced.Meat, v => nextYearTargets.Meat = v, v => currentYearScores.Meat = v, v => excesses.Meat = v);
            CalculateValuesForResource("Chocolate", breakEvenTarget.Chocolate, consumptionTarget.Chocolate, consumptionsProduced.Chocolate, v => nextYearTargets.Chocolate = v, v => currentYearScores.Chocolate = v, v => excesses.Chocolate = v);
            CalculateValuesForResource("Energy", breakEvenTarget.Energy, consumptionTarget.Energy, consumptionsProduced.Energy, v => nextYearTargets.Energy = v, v => currentYearScores.Energy = v, v => excesses.Energy = v);
            CalculateValuesForResource("Textiles", breakEvenTarget.Textiles, consumptionTarget.Textiles, consumptionsProduced.Textiles, v => nextYearTargets.Textiles = v, v => currentYearScores.Textiles = v, v => excesses.Textiles = v);
        }

        private void CalculateValuesForResource(string resourceName, int breakEvenResourceValue, int targetValue, int consumptionProduced, Action<int> sentNextTargetValue, Action<int> setScore, Action<int> setExcess)
        {
            var deficit = targetValue - consumptionProduced;
            var score = consumptionProduced - targetValue;

            var excess = CalculateExcessForScore(score);
            var nextTarget = CalculateNextTarget(deficit, breakEvenResourceValue);

            _logger.LogInformation($"{resourceName}. Break even: {breakEvenResourceValue}, Deficit: {deficit}, Score: {score}, Excess: {excess}, Next target: {nextTarget}");

            setExcess(excess);
            setScore(score);
            sentNextTargetValue(nextTarget);
        }

        private int CalculateExcessForScore(int scoreValue)
        {
            if (scoreValue > 0)
            {
                return scoreValue;
            }

            return 0;
        }

        private int CalculateNextTarget(int deficit, int breakEven)
        {
            if (deficit <= 0)
            {
                return breakEven;
            }
            else
            {
                return breakEven + deficit;
            }
        }
    }
}