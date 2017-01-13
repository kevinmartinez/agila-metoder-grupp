using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BusinessSystem;
using System.IO;

namespace BusinessSystem
{
    class BusinessSystemUi
    {
        
        private StoreInterface Store = new StoreInterface();
        //OrderStock<Orders<OrderRow>> _orderStock = new OrderStock<Orders<OrderRow>>();

        public void StartApplication()
        {
            DisplayMainMenu();
        }

        public void EndApplication()
        {
            Store.SaveAllToFile();
        }

        private void DisplayMainMenu()
        {
            int userInputInt = 0;
            do
            {
                Console.Clear();
                Console.WriteLine(PrintMainMenu());
                string userInput = Console.ReadLine();
                while (userInput == "")
                {
                    Console.WriteLine("Please select a number from the menu. ");
                    userInput = Console.ReadLine();
                }
                try
                {
                    userInputInt = Convert.ToInt32(userInput);
                }
                catch (Exception exception)
                {
                    //TODO: logga exception
                }
                try {
                    int menuSelection = Convert.ToInt32(userInput);
                    DisplaySubMenu(menuSelection);
                }
                catch (Exception exception) {
                    //TODO: logga exception
                }
            } while (userInputInt <= 8 && userInputInt >= 1);


        }

        private string PrintMainMenu()
        {
            string menuMessage = "Main menu. Select a number:"
                                  + "\n1. To register a new order."
                                  + "\n2. To remove an order."
                                  + "\n3. To update an order"
                                  + "\n4. To list all orders by customer name."
                                  + "\n5. To change the number of products in store."
                                  + "\n6. To change the price of a product."
                                  + "\n7. To register a new product."
                                  + "\n8. To register a new customer"
                                  + "\n0. Exit the application.";
            return menuMessage;
        }

        private void DisplaySubMenu(int menuSelection)
        {
            switch (menuSelection)
            {
                case 1:
                    //ropa på registrera ny order
                    SubMenu_1();
                    break;
                case 2:
                    //ropa på ta bort order
                    //SubMenu_2();
                    break;
                case 3:
                    //ropa på uppdatera order
                    //SubMenu_3();
                    break;
                case 4:
                    //ropa på lista order per kund
                    //SubMenu_4();
                    break;
                case 5:
                    //ropa på ändra antalet produkter i lager
                    SubMenu_5();
                    break;
                case 6:
                    //ropa på byta pris på produkt
                    SubMenu_6();
                    break;
                case 7:
                    //ropa på registrera ny produkt
                    SubMenu_7();
                    break;
                case 8:
                    //ropa på registrera ny kund
                    SubMenu_8();
                    break;
            }
        }

        private void SubMenu_1()
        {
            Console.Clear();
            Console.WriteLine("Enter customer number: "); 
            string input = Console.ReadLine();
            while (input == "") {
                Console.WriteLine("Customer number cannot be empty. You must enter a valid customer number please: ");
                input = Console.ReadLine();
            }
            //string result = Store.AddOrderHeader(input);
            string result = "";

            switch (result)
            {
                case "Error":
                    Console.WriteLine("An error occurred, please try again. ");
                    break;
                case "Customer number does not exist!":
                    Console.WriteLine(result);
                    break;
                default:
                    //continue to add rows in order:
                    string message = "";
                    bool continueToAddRows = true;
                    do {
                        //AskForInputAndAddRowToOrder();
                        
                        Console.WriteLine("Enter product number: ");
                        string inputProductNumber = Console.ReadLine();
                        while (inputProductNumber == "") {
                            Console.WriteLine("Product number cannot be empty. You must enter a valid product number please: ");
                            inputProductNumber = Console.ReadLine();
                        }
                        Console.WriteLine("Enter the number of products: ");
                        string inputProductQuantity = Console.ReadLine();
                        while (inputProductQuantity == "") {
                            Console.WriteLine("You must enter how many products you want to buy. Enter a number please: ");
                            inputProductQuantity = Console.ReadLine();
                        }
                        string rowResult = Store.AddOrderRow(result, inputProductNumber, inputProductQuantity, out message);
                        if (message != "") {
                            Console.WriteLine(message);
                            
                        }
                        if (rowResult != "0") {
                            Console.WriteLine("The row " + rowResult + " was added to the order.");
                        }

                      continueToAddRows = CheckIfUserWillContinueToAddRows();
                        Console.Clear();
                    } while (continueToAddRows);

                    break;

            }     
                
        }

        private bool CheckIfUserWillContinueToAddRows() {
            Console.WriteLine("Do you want to continue adding rows in this order? (Y/N)");
            string answer = Console.ReadLine();
            while (answer == "") {
                Console.WriteLine("You must answer Y or N if you want to continue adding rows or not please.");
                answer = Console.ReadLine();
            }

            if (answer.ToLower() == "y") {
                return true;
            }
            else {
                return false;
            }

        }

        private void SubMenu_2()
        {
            //remove an order
        }

        private void SubMenu_3()
        {
            //update an order
        }

        private void SubMenu_4()
        {
            
        }

        private void SubMenu_5()
        {
            Console.Clear();
            Console.WriteLine("Change the number of products in store: ");
            Console.Write("Enter product number: ");
            string inputProductNumber = Console.ReadLine();
            while (inputProductNumber == "") {
                Console.WriteLine("Product number cannot be empty. Enter a valid product number please: ");
                inputProductNumber = Console.ReadLine();
            }
            Console.Write("Enter the new amount for this product: ");
            string inputProductQuantity = Console.ReadLine();
            while (inputProductQuantity == "") {
                Console.WriteLine("The number of products cannot be empty. Enter zero or a positive number please: ");
                inputProductQuantity = Console.ReadLine();
            }
            string result = Store.ModifyProductQuantity(inputProductNumber, inputProductQuantity);
            if (result != "") {
                Console.WriteLine(result);
                Console.ReadLine();
            }
            else {
                Console.WriteLine("The product's amount is now changed. Press enter to continue.");
                Console.ReadLine();
            }
        }

        private void SubMenu_6()
        {
            Console.Clear();
            Console.WriteLine("Change the price on a product");
            Console.Write("Enter product number: ");
            string inputProductNumber = Console.ReadLine();
            while (inputProductNumber == "") {
                Console.WriteLine("Product number cannot be empty. Enter a valid product number please: ");
                inputProductNumber = Console.ReadLine();
            }
            Console.Write("Enter the new price on the product: ");
            string inputProductPrice = Console.ReadLine();
            while (inputProductPrice == "") {
                Console.WriteLine("Product price cannot be empty. Enter a valid product price please: ");
                inputProductPrice = Console.ReadLine();
            }
            string result = Store.ModifyProductPrice(inputProductNumber, inputProductPrice);
            if (result != "")
            {
                Console.WriteLine(result);
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("The product price is now changed. Press enter to continue.");
                Console.ReadLine();
            }
        }

        private void SubMenu_7() {
            Console.Clear();
            Console.WriteLine("Register a new product");
            Console.Write("Enter product number: ");
            string inputProductNumber = Console.ReadLine();
            while (inputProductNumber == "") {
                Console.WriteLine("Product number cannot be empty. Enter a valid product number please: ");
                inputProductNumber = Console.ReadLine();
            }
            Console.Write("Enter product name: ");
            string inputProductName = Console.ReadLine();
            while (inputProductName == "") {
                Console.WriteLine("Product name cannot be empty. Enter a valid product name please: ");
                inputProductName = Console.ReadLine();
            }
            Console.Write("Enter product price: ");
            string inputProductPrice = Console.ReadLine();
            while (inputProductPrice == "") {
                Console.WriteLine("Product price cannot be empty. Enter a valid product price please: ");
                inputProductPrice = Console.ReadLine();
            }
            Console.Write("Enter product quantity: ");
            string inputProductQuantity = Console.ReadLine();
            while (inputProductQuantity == "") {
                Console.WriteLine("Product quantity cannot be empty. Enter a valid product quantity please: ");
                inputProductQuantity = Console.ReadLine();
            }
            
            var result = Store.AddProduct(inputProductNumber, inputProductName, inputProductPrice, inputProductQuantity);
            if (result == "")
            {
                Console.WriteLine("The new product " + inputProductName +
                                  " was successfully added in store. Press enter to continue.");
            }
            else
            {
                Console.WriteLine(result + " Press enter to continue.");
            }
            Console.ReadLine();
        }

        private void SubMenu_8()
        {
            Console.Clear();
            Console.WriteLine("Register a new customer");
            Console.Write("Enter customer name: ");
            string inputCustomerName = Console.ReadLine();
            while (inputCustomerName == "") {
                Console.WriteLine("Customer name cannot be empty. Enter a valid customer name please: ");
                inputCustomerName = Console.ReadLine();
            }
            Console.Write("Enter customer address: ");
            string inputCustomerAddress = Console.ReadLine();
            while (inputCustomerAddress == "") {
                Console.WriteLine("Customer address cannot be empty. Enter a valid customer address please: ");
                inputCustomerAddress = Console.ReadLine();
            }
            var result = Store.AddCustomer(inputCustomerName, inputCustomerAddress);
            switch (result)
            {
                case "Customer already exists.":
                    Console.WriteLine(result);
                    break;
                default:
                    Console.WriteLine("The new customer " + inputCustomerName + " was successfully added. Customer number is: " + result);
                    break;

            }
            
            Console.WriteLine("Press enter to continue.");
            Console.ReadLine();
        }
        
    }
}
