namespace Repository
{
    using Models;
    using Repository.Interfaces;
    public class InMemoryTransactionRepository : ITransactionRepository
    {
        private static List<Transaction> transactions = new();
        private static int nextId = 1;

        public List<Transaction> GetAll() => transactions;

        public Transaction? GetById(int id) => transactions.FirstOrDefault(t => t.TransactionId == id);

        public void Add(Transaction transaction)
        {
            transaction.TransactionId = nextId++;
            transactions.Add(transaction);
        }

        public void Update(Transaction transaction)
        {
            var existing = GetById(transaction.TransactionId);
            if (existing != null)
            {
                existing.Description = transaction.Description;
                existing.Amount = transaction.Amount;
                existing.Type = transaction.Type;
                existing.PersonId = transaction.PersonId;
                existing.CategoryId = transaction.CategoryId;
            }
        }

        public void Delete(int id) => transactions.RemoveAll(t => t.TransactionId == id);


        public List<Transaction> GetByPersonId(int personId)
        {
            return transactions.Where(t => t.PersonId == personId).ToList();
        }


        public void DeleteByPersonId(int idPerson) => transactions.RemoveAll(t => t.PersonId == idPerson);
    
    }
}