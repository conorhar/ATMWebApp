using System.Collections.Generic;
using System.Linq;
using ConorHarringtonATM.Data;
using Microsoft.AspNetCore.Http.Features;

namespace ConorHarringtonATM.Services
{
    public class BillService : IBillService
    {
        private readonly ApplicationDbContext _dbContext;

        public BillService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool CheckInputDividesBy100(int input)
        {
            if (input % 100 != 0) return false;
            return true;
        }

        public bool CheckEnoughBillsAreAvailable(int input)
        {
            var totalValue = GetTotalValue();

            if (totalValue < input) return false;
            return true;
        }

        public int GetTotalValue()
        {
            return _dbContext.Bills.Sum(r => r.Quantity * r.Value);
        }

        public bool CheckRemainingBillsAddUp(int input)
        {
            var billsListInts = GetIntList();

            foreach (var b in billsListInts.OrderByDescending(r =>r))
            {
                if (input > b || input == b)
                {
                    billsListInts.Remove(b);
                    input -= b;
                }

                if (input == 0) return true;
            }

            return false;
        }

        private List<int> GetIntList()
        {
            var billsList = new List<int>();

            foreach (var bill in _dbContext.Bills.ToList())
            {
                for (int i = 0; i < bill.Quantity; i++)
                {
                    billsList.Add(bill.Value);
                }
            }

            return billsList;
        }

        public List<int> GetSmallestAmountBills(int input)
        {
            var result = new List<int>();
            var billsListInts = GetIntList();

            foreach (var b in billsListInts.OrderByDescending(r => r))
            {
                if (input > b || input == b)
                {
                    result.Add(b);
                    input -= b;
                }

                if (input == 0) return result;
            }

            return result;
        }

        public void UpdateBillsInDB(List<int> billListInts)
        {
            foreach (var b in billListInts)
            {
                var dbBill = _dbContext.Bills.First(r => r.Value == b);
                dbBill.Quantity -= 1;
            }

            _dbContext.SaveChanges();
        }

        public List<Bill> ConvertToBills(List<int> ints)
        {
            var billList = new List<Bill>();

            foreach (var i in ints)
            {
                var bill = billList.FirstOrDefault(r => r.Value == i);

                if (bill == null)
                {
                    billList.Add(new Bill {Value = i, Quantity = 1});
                }
                else
                {
                    bill.Quantity += 1;
                }
            }

            return billList;
        }
    }
}