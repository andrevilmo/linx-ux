namespace Linx.Framework.BV.LicenseServer
{
    /// <summary>Omni RD-15: 200 Succeeded, 400 Denied, other Failed.</summary>
    public enum TrustUnblockOutcome
    {
        Succeeded = 0,
        Denied = 1,
        Failed = 2
    }

    public sealed class TrustUnblockResult
    {
        public int Outcome { get; set; }
        public string Details { get; set; }

        public bool Succeeded
        {
            get { return Outcome == (int)TrustUnblockOutcome.Succeeded; }
        }

        public static TrustUnblockResult Create(TrustUnblockOutcome outcome, string details)
        {
            return new TrustUnblockResult
            {
                Outcome = (int)outcome,
                Details = details
            };
        }
    }
}
