using System;
using Game.Server.Services.Models;

namespace Game.Server.Services
{
    public interface IGameScoreService
    {
        void CalculateYearValues(int year, GameCountry country, CountryYearConsumptionResourceValues productionRecorded);
    }

    public class GameScoreService : IGameScoreService
    {
        public void CalculateYearValues(int year, GameCountry country, CountryYearConsumptionResourceValues productionRecorded)
        {
            var breakEven = country.Years[0].Targets;

            var currentYearTargets = country.Years[year].Targets;

            var currentYearScores = country.Years[year].Scores = new CountryYearConsumptionResourceValues();

            var nextYearKey = year + 1;
            var nextYearTargets = new CountryYearConsumptionResourceValues();
            if (country.Years.ContainsKey(nextYearKey))
            {
                country.Years[year + 1].Targets = nextYearTargets;
            }

            CalculateScoresAndTargetsForCountryYearResources(breakEven, currentYearTargets, productionRecorded, nextYearTargets, currentYearScores);
        }

        private void CalculateScoresAndTargetsForCountryYearResources(
            CountryYearConsumptionResourceValues breakEvenResourceProduction,
            CountryYearConsumptionResourceValues targetConsumptionResources,
            CountryYearConsumptionResourceValues resourcesProduced,
            CountryYearConsumptionResourceValues nextYearTargets,
            CountryYearConsumptionResourceValues currentYearScores)
        {
            CalculateValuesForResource(breakEvenResourceProduction.Grain, targetConsumptionResources.Grain, resourcesProduced.Grain, (v) => nextYearTargets.Grain = v, (v) => currentYearScores.Grain = v);
            CalculateValuesForResource(breakEvenResourceProduction.Meat, targetConsumptionResources.Meat, resourcesProduced.Meat, (v) => nextYearTargets.Meat = v, (v) => currentYearScores.Meat = v);
            CalculateValuesForResource(breakEvenResourceProduction.Chocolate, targetConsumptionResources.Chocolate, resourcesProduced.Chocolate, (v) => nextYearTargets.Chocolate = v, (v) => currentYearScores.Chocolate = v);
            CalculateValuesForResource(breakEvenResourceProduction.Energy, targetConsumptionResources.Energy, resourcesProduced.Energy, (v) => nextYearTargets.Energy = v, (v) => currentYearScores.Energy = v);
            CalculateValuesForResource(breakEvenResourceProduction.Textiles, targetConsumptionResources.Textiles, resourcesProduced.Textiles, (v) => nextYearTargets.Textiles = v, (v) => currentYearScores.Textiles = v);
        }

        private void CalculateValuesForResource(int breakEvenResourceValue, int targetValue, int production, Action<int> sentNextTargetValue, Action<int> setScore)
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

            setScore(score);
            sentNextTargetValue(nextTarget);
        }
    }
}