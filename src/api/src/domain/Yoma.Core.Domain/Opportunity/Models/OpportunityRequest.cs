namespace Yoma.Core.Domain.Opportunity.Models
{
    public class OpportunityRequest
    {
        public Guid? Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public Guid TypeId { get; set; }

        public Guid OrganizationId { get; set; }

        public string? Instructions { get; set; }

        public string? URL { get; set; }

        public decimal? ZltoReward { get; set; }

        public decimal? YomaReward { get; set; }

        public decimal? ZltoRewardPool { get; set; }

        public decimal? YomaRewardPool { get; set; }

        public bool VerificationSupported { get; set; }

        public Guid DifficultyId { get; set; }

        public Guid CommitmentIntervalId { get; set; }

        public short? CommitmentIntervalCount { get; set; }

        public int? ParticipantLimit { get; set; }

        public List<string>? Keywords { get; set; }

        public DateTimeOffset DateStart { get; set; }

        public DateTimeOffset? DateEnd { get; set; }

        public bool PostAsActive { get; set; }
    }
}