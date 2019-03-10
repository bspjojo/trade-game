using Game.Server.Services.Models;

namespace Game.Server.Models
{
    // Upload
    public class ScorerClientScoreUpdate
    {
        public string CountryId { get; set; }
        public ConsumptionResources YearResults { get; set; }
        //public CountryYearProductionResources GrowthCertificatesPurchased { get; set; }
    }

    // Download
    public class ScorerClientScoreUpdateResponse
    {
        public ConsumptionResources NextYearTarget { get; set; }
        public ConsumptionResources Excess { get; set; }
    }
}