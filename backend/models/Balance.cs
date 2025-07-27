namespace Models
{
    public class Balance
    {
        public int PersonId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal TotalIncomes { get; set; }
        public decimal TotalExpenses { get; set; }
        public decimal Total => TotalIncomes - TotalExpenses;
    }
}
