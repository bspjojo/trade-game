using System;
using Game.Server.Services.Models;

namespace Game.Server.Services
{
    public interface IGameScoreService
    {
        void CalculateYearValues(int year, GameCountry country, ConsumptionResources productionRecorded);
    }

    public class GameScoreService : IGameScoreService
    {
        public void CalculateYearValues(int year, GameCountry country, ConsumptionResources productionRecorded)
        {
            var breakEven = country.Years[0].Targets;

            var currentYearTargets = country.Years[year].Targets;

            var currentYearScores = country.Years[year].Scores = new ConsumptionResources();
            var currentYearExcess = country.Years[year].Excess = new ConsumptionResources();

            var nextYearKey = year + 1;
            var nextYearTargets = new ConsumptionResources();
            if (country.Years.ContainsKey(nextYearKey))
            {
                country.Years[year + 1].Targets = nextYearTargets;
            }

            CalculateScoresAndTargetsForCountryYearResources(breakEven, currentYearTargets, productionRecorded, nextYearTargets, currentYearScores, currentYearExcess);
        }

        private void CalculateScoresAndTargetsForCountryYearResources(
            ConsumptionResources breakEvenTarget,
            ConsumptionResources consumptionTarget,
            ConsumptionResources resourcesProduced,
            ConsumptionResources nextYearTargets,
            ConsumptionResources currentYearScores,
            ConsumptionResources excesses)
        {
            CalculateValuesForResource(breakEvenTarget.Grain, consumptionTarget.Grain, resourcesProduced.Grain, v => nextYearTargets.Grain = v, v => currentYearScores.Grain = v, v => excesses.Grain = v);
            CalculateValuesForResource(breakEvenTarget.Meat, consumptionTarget.Meat, resourcesProduced.Meat, v => nextYearTargets.Meat = v, v => currentYearScores.Meat = v, v => excesses.Meat = v);
            CalculateValuesForResource(breakEvenTarget.Chocolate, consumptionTarget.Chocolate, resourcesProduced.Chocolate, v => nextYearTargets.Chocolate = v, v => currentYearScores.Chocolate = v, v => excesses.Chocolate = v);
            CalculateValuesForResource(breakEvenTarget.Energy, consumptionTarget.Energy, resourcesProduced.Energy, v => nextYearTargets.Energy = v, v => currentYearScores.Energy = v, v => excesses.Energy = v);
            CalculateValuesForResource(breakEvenTarget.Textiles, consumptionTarget.Textiles, resourcesProduced.Textiles, v => nextYearTargets.Textiles = v, v => currentYearScores.Textiles = v, v => excesses.Textiles = v);
        }

        private void CalculateValuesForResource(int breakEvenResourceValue, int targetValue, int production, Action<int> sentNextTargetValue, Action<int> setScore, Action<int> setExcess)
        {
            var deficit = targetValue - production;
            var score = production - targetValue;

            var nextTarget = 0;

            if (deficit <= 0)
            {
                nextTarget = breakEvenResourceValue;
            }
            else
            {
                nextTarget = breakEvenResourceValue + deficit;
            }

            setExcess(CalculateExcessForScore(score));
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
    }
}