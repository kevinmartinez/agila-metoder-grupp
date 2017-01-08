using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessSystem {
    class OrderStock<T> where T : Order<OrderRow> {
        public List<Order<OrderRow>> OrderStockList = new List<Order<OrderRow>>();

        public string CompanyName { get; set; }

        public OrderStock() //konstruktor
        {
            this.CompanyName = "The Company";  //TODO: ask for company name or get from config
        }

        public IEnumerator GetEnumerator() {
            for (int i = 0; i < OrderStockList.Count; i++) {

                yield return OrderStockList[i];
            }
        }

        //public void WriteOrderStockToFile(StreamWriter file)
        //{
        //    file.WriteLine("Order stock for the company: " + this.CompanyName);
        //    foreach (var v in OrderStockList)
        //    {

        //        file.WriteLine("Customer name: " + v.CustomerName + " Order number: " + v.OrderNumber);
        //        v.PrintAllRowsInOrderToFile(v.CustomerName, file);

        //    }
        //}

        public void AddOrderToOrderStock(StoreInterface store)
        {
            int orderNumber = OrderStockList.Count;
            Console.WriteLine("Register new order");
            Console.WriteLine("Enter customer name: ");  //TODO: eller customer number?
            string input = Console.ReadLine();
            while (input == "") {
                Console.WriteLine("Customer name cannot be empty. You must enter a valid customer name please: ");
                input = Console.ReadLine();
            }
            //TODO: koll om kundnamnet/kundnumret finns i kundlistan
            
           
            //Customer customer = Store.CheckIfCustomerExists typ... som returnerar customer objektet så vi kan koppla det till kundnamn/kundnr.
            //Order<OrderRow> order = new Order<OrderRow>(customer.Name, orderNumber);

            Order<OrderRow> order = new Order<OrderRow>(input, orderNumber);

            OrderStockList.Add(order);

        }

        public void ListAllOrdersPerCustomer(string customerName)
        {
            List<Order<OrderRow>> tempList = new List<Order<OrderRow>>();
            tempList = OrderStockList.FindAll(s => s.CustomerName.Equals(customerName));
            Console.WriteLine("All orders for the customer " + customerName);
            for (int i = 0; i < tempList.Count; i++)
            {
                Console.WriteLine("Order number: " + tempList[i].OrderNumber);
                tempList[i].PrintAllRowsInOrderToConsole();
            }

        }

        //public void PrintAllOrdersInOrderStockList() {
        //    foreach (var v in OrderStockList) {
        //        Console.Write("Order for customer: " + v.CustomerName);
        //        //v.PrintAllRowsInOrder(v.CustomerName);
        //        Console.WriteLine(" ----- ");
        //    }
        //}
    }
}
