using Models;

public interface ITransactionRepository
{
    List<Transaction> GetAll();
    Transaction? GetById(int id);
    void Add(Transaction transaction);
    void Update(Transaction transaction);
    void Delete(int id);
    void DeleteByPersonId(int idPerson);
    List<Transaction> GetByPersonId(int personId);
}
