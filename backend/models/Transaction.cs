namespace Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string Type { get; set; } = string.Empty;
        public int PersonId { get; set; }
        public int CategoryId { get; set; }
        
    }
}