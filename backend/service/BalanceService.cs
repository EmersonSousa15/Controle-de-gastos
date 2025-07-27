using Models;
using Models.DTOs;
using Service.Interfaces;
using Repository.Interfaces;

namespace Service
{
    public class BalanceService : IBalanceService
    {
        private readonly IPersonRepository personRepo;
        private readonly ITransactionRepository transactionRepo;

        public BalanceService(IPersonRepository _personRepo, ITransactionRepository _transactionRepo)
        {
            personRepo = _personRepo;
            transactionRepo = _transactionRepo;
        }

        public List<Balance> CalculateAll()
        {
            var balances = new List<Balance>();
            foreach (var person in personRepo.GetAll())
            {
                balances.Add(Calculate(person.PersonId)!);
            }
            return balances;
        }

        public Balance? Calculate(int personId)
        {
            var person = personRepo.GetById(personId);
            if (person == null) return null;

            var transactions = transactionRepo.GetByPersonId(personId);

            decimal incomes = transactions
                .Where(t => t.Type.ToLower() == "receita")
                .Sum(t => t.Amount);

            decimal expenses = transactions
                .Where(t => t.Type.ToLower() == "despesa")
                .Sum(t => t.Amount);

            return new Balance
            {
                PersonId = person.PersonId,
                Name = person.Name,
                TotalIncomes = incomes,
                TotalExpenses = expenses
            };
        }

        public BalanceDTO CalculateAllTransactions()
        {
            var transactions = transactionRepo.GetAll();

            var totalIncomes = transactions
                .Where(t => t.Type == "receita")
                .Sum(t => (double)t.Amount);

            var totalExpenses = transactions
                .Where(t => t.Type == "despesa")
                .Sum(t => (double)t.Amount);

            return new BalanceDTO
            {
                TotalIncomes = totalIncomes,
                TotalExpenses = totalExpenses,
                Total = totalIncomes - totalExpenses
            };
        }

        private readonly Dictionary<int, Balance> _balances = new();

        public void AddBalance(Balance balance)
        {

            if (!_balances.ContainsKey(balance.PersonId))
            {
                _balances[balance.PersonId] = balance;
            }
            else
            {
                throw new InvalidOperationException($"Saldo da pessoa {balance.PersonId} já existe.");
            }
        }

        public void UpdateBalance(Balance balance)
        {
            if (_balances.ContainsKey(balance.PersonId))
            {
                _balances[balance.PersonId] = balance;
            }
            else
            {
                throw new KeyNotFoundException($"Saldo da passoa {balance.PersonId} não encontrado.");
            }
        }

        public void DeleteBalance(int personId)
        {
            if (!_balances.Remove(personId))
            {
                throw new KeyNotFoundException($"Saldo ds pessoa {personId} não encontrado!");
            }
        }


    }
}
