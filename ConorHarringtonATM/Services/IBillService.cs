using System.Collections.Generic;
using ConorHarringtonATM.Data;

namespace ConorHarringtonATM.Services
{
    public interface IBillService
    {
        bool CheckInputDividesBy100(int input);
        bool CheckEnoughBillsAreAvailable(int input);
        bool CheckRemainingBillsAddUp(int input);
        List<int> GetSmallestAmountBills(int input);
        void UpdateBillsInDB(List<int> list);
        List<Bill> ConvertToBills(List<int> ints);
    }
}