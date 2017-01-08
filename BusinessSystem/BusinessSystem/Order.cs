using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Net;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using BusinessSystem;

namespace BusinessSystem
{
    class Order<T> where T : OrderRow
    {
        private List<OrderRow> _orderRowList = new List<OrderRow>();
        private int row = 0;
        public string CustomerName { get; set; }
        public int OrderNumber { get; set; }
       
        public IEnumerator GetEnumerator() {
            for (int i = 0; i < _orderRowList.Count; i++) {

                yield return _orderRowList[i];
            }
        }

        public Order(string customerName, int orderNumber) //Konstruktor
        {
            this.CustomerName = customerName;
            this.OrderNumber = orderNumber;
            bool continueToAddRows = true;
            do
            {
                AskForInputAndAddRowToOrder();
                continueToAddRows = CheckIfUserWillContinueToAddRows();
            } while (continueToAddRows);
        }

        public void AskForInputAndAddRowToOrder()
        {
            Console.WriteLine("Select a product to add to order: ");
            string inputProduct = Console.ReadLine();
            while (inputProduct == "") {
                Console.WriteLine("Product cannot be empty. Enter a valid product please: ");
                inputProduct = Console.ReadLine();
            }
            //TODO: check if product exists in store, if not continue asking for a valid product
            int numberOfItems = GetANumberFromUser();
            while (numberOfItems == 0) {
                numberOfItems = GetANumberFromUser();
            }
            OrderRow orderRow = new OrderRow(row, inputProduct, numberOfItems);
            _orderRowList.Add(orderRow);
            row++;
            Console.WriteLine("The product was successfully added to the order. ");
        }
        
        private int GetANumberFromUser()
        {
            Console.WriteLine("How many items of this product do you want to buy? ");
            string inputNumber = Console.ReadLine();
            while (inputNumber == "" || inputNumber == "0") {
                Console.WriteLine("The number of products cannot be empty or zero. Enter a number please: ");
                inputNumber = Console.ReadLine();
            }
            int numberOfItems = 0;
            try {
                numberOfItems = Convert.ToInt32(inputNumber);
            }
            catch (Exception exception) {

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
            foreach (var v in _orderRowList) {
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
