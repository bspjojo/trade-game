using System;
using Game.Server.Services.Models;

namespace Game.Server.Services
{
    public interface IGameScoreService
    {
        void CalculateYearValues(ConsumptionResources breakEven, ConsumptionResources currentYearTargets, ConsumptionResources consumptionResourcesRecorded, out ConsumptionResources currentYearExcess, out ConsumptionResources currentYearScores, out ConsumptionResources nextYearTargets);
    }

    public class GameScoreService : IGameScoreService
    {
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
            CalculateValuesForResource(breakEvenTarget.Grain, consumptionTarget.Grain, consumptionsProduced.Grain, v => nextYearTargets.Grain = v, v => currentYearScores.Grain = v, v => excesses.Grain = v);
            CalculateValuesForResource(breakEvenTarget.Meat, consumptionTarget.Meat, consumptionsProduced.Meat, v => nextYearTargets.Meat = v, v => currentYearScores.Meat = v, v => excesses.Meat = v);
            CalculateValuesForResource(breakEvenTarget.Chocolate, consumptionTarget.Chocolate, consumptionsProduced.Chocolate, v => nextYearTargets.Chocolate = v, v => currentYearScores.Chocolate = v, v => excesses.Chocolate = v);
            CalculateValuesForResource(breakEvenTarget.Energy, consumptionTarget.Energy, consumptionsProduced.Energy, v => nextYearTargets.Energy = v, v => currentYearScores.Energy = v, v => excesses.Energy = v);
            CalculateValuesForResource(breakEvenTarget.Textiles, consumptionTarget.Textiles, consumptionsProduced.Textiles, v => nextYearTargets.Textiles = v, v => currentYearScores.Textiles = v, v => excesses.Textiles = v);
        }

        private void CalculateValuesForResource(int breakEvenResourceValue, int targetValue, int consumptionProduced, Action<int> sentNextTargetValue, Action<int> setScore, Action<int> setExcess)
        {
            var deficit = targetValue - consumptionProduced;
            var score = consumptionProduced - targetValue;

            setExcess(CalculateExcessForScore(score));
            setScore(score);
            sentNextTargetValue(CalculateNextTarget(deficit, breakEvenResourceValue));
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