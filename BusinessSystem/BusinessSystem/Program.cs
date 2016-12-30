using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessSystem {
    class Program {
        static void Main(string[] args) {
            BusinessSystemUi ui = new BusinessSystemUi();
            ui.MainMenu();
            string menuSelection = Console.ReadLine();
            switch (menuSelection) {
                case "8":
                    ui.SubMenu_8();
                    break;
            }

        }
    }
    class BusinessSystemUi {
        public void MainMenu() {
            Console.Clear();
            Console.WriteLine("Main menu. Select a number:"
                    + "\n1. To register a new order."
                    + "\n2. To remove an order."
                    + "\n3. To update an order"
                    + "\n4. To list all orders by customer name."
                    + "\n5. To list all products in store."
                    + "\n6. To change the price of a product."
                    + "\n7. To register a new product."
                    + "\n8. To register a new customer"
                    + "\n0. Exit the application.");
        }

        public void SubMenu_8() {
            Console.WriteLine("Create new customer. State customer name: ");
            string inputName = Console.ReadLine();
            Console.WriteLine("State customer address: ");
            string inputAddress = Console.ReadLine();
            Customer customer = new Customer(inputName, inputAddress);
        }
    }
    class Customer {
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public int ShoeSize { get; set; }

        public Customer(string name, string address)  //Konstruktor
        {
            this.CustomerName = name;
            this.CustomerAddress = address;
        }
    }
}
