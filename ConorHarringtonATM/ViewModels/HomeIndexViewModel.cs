using System.Collections.Generic;

namespace ConorHarringtonATM.ViewModels
{
    public class HomeIndexViewModel
    {
        public List<BillItem> RemainingBillItems { get; set; }

        public class BillItem
        {
            public int Value { get; set; }
            public int Quantity { get; set; }
        }
    }
}