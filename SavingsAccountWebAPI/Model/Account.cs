namespace SavingsAccountWebAPI.Model
{
    public class Account : Entity
    {
        public Guid AccountNumber { get; set; }
        public string? OwnerName { get; set; }
        public float? OpeningBalance { get; set; }
        public float CurrentBalance { get; set; }

    }
}
