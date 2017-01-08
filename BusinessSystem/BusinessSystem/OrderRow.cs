using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessSystem {
    class OrderRow
    {
        public int row { get; set; }
        public string ProductName { get; set; }
        public int NumberOfItems { get; set; }

        public OrderRow(int row, string productName, int numberOfItems)
        {
            this.row = row;
            this.ProductName = productName;
            this.NumberOfItems = numberOfItems;
        }
    }
}
