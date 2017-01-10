using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
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


    //=====================================================================================================================================


    class OrderStock<T> where T : Orders<OrderRow>
    {
        public List<Orders<OrderRow>> OrderStockList = new List<Orders<OrderRow>>();

        public string CompanyName { get; set; }

        public OrderStock() //konstruktor
        {
            this.CompanyName = "The Company";  //TODO: ask for company name or get from config
        }

        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < OrderStockList.Count; i++)
            {

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
            while (input == "")
            {
                Console.WriteLine("Customer name cannot be empty. You must enter a valid customer name please: ");
                input = Console.ReadLine();
            }
            //TODO: koll om kundnamnet/kundnumret finns i kundlistan


            //Customer customer = Store.CheckIfCustomerExists typ... som returnerar customer objektet så vi kan koppla det till kundnamn/kundnr.
            //Order<OrderRow> order = new Order<OrderRow>(customer.Name, orderNumber);

            Orders<OrderRow> order = new Orders<OrderRow>(input, orderNumber);

            OrderStockList.Add(order);

        }

        public void ListAllOrdersPerCustomer(string customerName)
        {
            List<Orders<OrderRow>> tempList = new List<Orders<OrderRow>>();
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

        //==================================================================================================================================

        public void AskForInputAndAddRowToOrder()
        {
            Console.WriteLine("Select a product to add to order: ");
            string inputProduct = Console.ReadLine();
            while (inputProduct == "")
            {
                Console.WriteLine("Product cannot be empty. Enter a valid product please: ");
                inputProduct = Console.ReadLine();
            }
            //TODO: check if product exists in store, if not continue asking for a valid product
            int numberOfItems = GetANumberFromUser();
            while (numberOfItems == 0)
            {
                numberOfItems = GetANumberFromUser();
            }
            OrderRow orderRow = new OrderRow(row, inputProduct, numberOfItems);
            orderRows.Add(orderRow);
            row++;
            Console.WriteLine("The product was successfully added to the order. ");
        }

        private int GetANumberFromUser()
        {
            Console.WriteLine("How many items of this product do you want to buy? ");
            string inputNumber = Console.ReadLine();
            while (inputNumber == "" || inputNumber == "0")
            {
                Console.WriteLine("The number of products cannot be empty or zero. Enter a number please: ");
                inputNumber = Console.ReadLine();
            }
            int numberOfItems = 0;
            try
            {
                numberOfItems = Convert.ToInt32(inputNumber);
            }
            catch (Exception exception)
            {

                Console.WriteLine("You must enter a number. Try again please.");
            }
            return numberOfItems;
        }

        private bool CheckIfUserWillContinueToAddRows()
        {
            Console.WriteLine("Do you want to continue adding rows in this order? (Y/N)");
            string answer = Console.ReadLine();
            while (answer == "")
            {
                Console.WriteLine("You must answer Y or N if you want to continue adding rows or not please.");
                answer = Console.ReadLine();
            }

            if (answer.ToLower() == "y")
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public void PrintAllRowsInOrderToConsole()
        {
            foreach (var v in orderRows)
            {
                Console.WriteLine("Row: " + v.row + " Product: " + v.ProductName + " Amount: " + v.NumberOfItems);
            }
        }

        //public void PrintAllRowsInOrderToFile(string customerName, StreamWriter file)
        //{

        //    //lista alla order för en viss kund... 
        //    //Console.WriteLine("List orders for :" + customerName);
        //    foreach (var v in _orderRowList)
        //    {
        //        //Console.WriteLine("Row: " + v.row + " Product: " + v.ProductName + " Amount: " + v.NumberOfItems);
        //        file.WriteLine(v.row + ";" + v.ProductName + ";" + v.NumberOfItems);
        //    }
        //}
    }

}

}
*/
