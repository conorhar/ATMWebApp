using System.Collections.Generic;

namespace ConorHarringtonATM.ViewModels
{
    public class HomeResultViewModel
    {
        public string Header { get; set; }
        public List<BillItem> RemainingBillItems { get; set; }
        public List<BillItem> DispensedBillItems { get; set; }

        public class BillItem
        {
            public int Value { get; set; }
            public int Quantity { get; set; }
        }
    }
}