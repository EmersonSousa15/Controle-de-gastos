using Models;
using Models.DTOs;

namespace Service.Interfaces
{
    public interface IBalanceService
    {
        List<Balance> CalculateAll();
        Balance? Calculate(int personId);
        BalanceDTO CalculateAllTransactions();

        void AddBalance(Balance balance);
        void UpdateBalance(Balance balance);
        void DeleteBalance(int personId);

    }
}
