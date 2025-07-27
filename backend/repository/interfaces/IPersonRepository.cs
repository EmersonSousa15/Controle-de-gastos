using Models;

namespace Repository.Interfaces
{
    public interface IPersonRepository
    {
        List<Person> GetAll();
        Person? GetById(int id);
        void Add(Person person);
        void Update(Person person);
        void Delete(int id);
    }
}
