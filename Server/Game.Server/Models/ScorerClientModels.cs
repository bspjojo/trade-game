using Game.Server.Services.Models;

namespace Game.Server.Models
{
    // Upload
    public class ScorerClientScoreUpdate
    {
        public string GameId { get; set; }
        public string CountryId { get; set; }
        public int Year { get; set; }
        public CountryYearConsumptionResourceValues YearResults { get; set; }
        //public CountryYearProductionResources GrowthCertificatesPurchased { get; set; }
    }

    // Download
    public class ScorerClientScoreUpdateResponse
    {
        public CountryYearConsumptionResourceValues NextYearTarget { get; set; }
        public CountryYearConsumptionResourceValues Excess { get; set; }
    }
}