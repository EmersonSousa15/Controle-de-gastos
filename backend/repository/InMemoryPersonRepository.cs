using System.Collections.Generic;
using System.Linq;
using Models;
using Repository.Interfaces;

namespace Repository
{
    public class InMemoryPersonRepository : IPersonRepository
    {
        private readonly ITransactionRepository transactionRepository;
        private readonly List<Person> people = new();
        private int nextId = 1;

        public InMemoryPersonRepository(ITransactionRepository _transactionRepository)
        {
            transactionRepository = _transactionRepository;
        }

        public List<Person> GetAll() => people;

        public Person? GetById(int id) => people.FirstOrDefault(p => p.PersonId == id);

        public void Add(Person person)
        {
            person.PersonId = nextId++;
            people.Add(person);
        }

        public void Update(Person person)
        {
            var existing = GetById(person.PersonId);
            if (existing != null)
            {
                existing.Name = person.Name;
                existing.Age = person.Age;
            }
        }

        public void Delete(int id)
        {
            var person = GetById(id);
            if (person != null)
            {
                transactionRepository.Delete(id);
                people.Remove(person);
            }
        }
    }
}
