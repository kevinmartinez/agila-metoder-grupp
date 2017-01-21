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
                string userInput = "";
                Console.WriteLine();
                while (userInput == "")
                {
                    Console.Write("Please select a number from the menu: ");
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
                try
                {
                    int menuSelection = Convert.ToInt32(userInput);
                    DisplaySubMenu(menuSelection);
                }
                catch (Exception exception)
                {
                    //TODO: logga exception
                }
            } while (userInputInt <= 8 && userInputInt >= 1);


        }

        private string PrintMainMenu()
        {
            //string menuMessage = "Main menu. Select a number:"
            //                      + "\n1. To register a new order."
            //                      + "\n2. To remove an order."
            //                      + "\n3. To update an order"
            //                      + "\n4. To list all orders by customer."
            //                      + "\n5. To change the number of products in store."
            //                      + "\n6. To change the price of a product."
            //                      + "\n7. To register a new product."
            //                      + "\n8. To register a new customer"
            //                      + "\n0. Exit the application.";

            string menuMessage = "Main menu. Select a number:"
                                  + "\n1. To register a new order."
                                  + "\n2. To change an order."
                                  + "\n3. To list all orders by customer."
                                  + "\n4. To change the number of products in store."
                                  + "\n5. To change the price of a product."
                                  + "\n6. To register a new product."
                                  + "\n7. To register a new customer"
                                  + "\n0. Exit the application.";
            return menuMessage;
        }

        private void DisplaySubMenu(int menuSelection)
        {
            switch (menuSelection)
            {
                case 1:
                    //ropa på registrera ny order
                    NewOrder();
                    break;
                case 2:
                    //ändra order
                    OrderList();
                    break;
                case 3:
                    //ropa på lista order per kund
                    ListOrderForCustomer();
                    break;
                case 4:
                    //ropa på ändra antalet produkter i lager
                    ChangeProductQuantity();
                    break;
                case 5:
                    //ropa på byta pris på produkt
                    ChangeProductPrice();
                    break;
                case 6:
                    //ropa på registrera ny produkt
                    AddProduct();
                    break;
                case 7:
                    //ropa på registrera ny kund
                    NewCustomer();
                    break;
            }
        }


        //===========================================================================================================================
        private void NewOrder()
        {
            string customerNumber, orderNumber, message;

            Console.Clear();
            Console.WriteLine("\tNew order\n\t---------");

            //Check for customer.
            //do
            //{
            //    Console.WriteLine("\tEnter customer number (Enter-Quit): ");
            //    customerNumber = Console.ReadLine();

            //    //Quit if blank.
            //    if (customerNumber == "")
            //        return;

            //    //Get customer.
            //    Store.customerObj = Store.GetCustomerByNumber(customerNumber, out message);

            //    if (Store.customerObj == null)
            //    {
            //        Console.Write("\t  Customer not found!");
            //        Console.ReadKey();
            //    }
            //} while (Store.customerObj == null);


            //Input customernumber.
            customerNumber = InputCustomerNumber();

            if (customerNumber != "")
            {
                //Add order.
                orderNumber=Store.AddOrderHeader(customerNumber, out message);

                if(orderNumber != "0")
                    Console.Write("\t  Order added. Ordernumber=" + orderNumber);
                else
                    Console.Write("\t  Order could not be added!");

                Console.ReadKey();

            }


            ////string result = Store.AddOrderHeader(input);
            //string result = "";

            //switch (result)
            //{
            //    case "Error":
            //        Console.WriteLine("An error occurred, please try again. ");
            //        break;
            //    case "Customer number does not exist!":
            //        Console.WriteLine(result);
            //        break;
            //    default:
            //        //continue to add rows in order:
            //        string message = "";
            //        bool continueToAddRows = true;
            //        do {
            //            //AskForInputAndAddRowToOrder();

            //            Console.WriteLine("Enter product number: ");
            //            string inputProductNumber = Console.ReadLine();
            //            while (inputProductNumber == "") {
            //                Console.WriteLine("Product number cannot be empty. You must enter a valid product number please: ");
            //                inputProductNumber = Console.ReadLine();
            //            }
            //            Console.WriteLine("Enter the number of products: ");
            //string inputProductQuantity = Console.ReadLine();
            //while (inputProductQuantity == "") {
            //    Console.WriteLine("You must enter how many products you want to buy. Enter a number please: ");
            //    inputProductQuantity = Console.ReadLine();
            //}
            //string rowResult = Store.AddOrderRow(result, inputProductNumber, inputProductQuantity, out message);
            //if (message != "") {
            //    Console.WriteLine(message);

            //}
            //if (rowResult != "0") {
            //    Console.WriteLine("The row " + rowResult + " was added to the order.");
            //}

            //  continueToAddRows = CheckIfUserWillContinueToAddRows();
            //    Console.Clear();
            //} while (continueToAddRows);

            //break;



        }

        //===========================================================================================================================
        //private bool CheckIfUserWillContinueToAddRows()
        //{
        //    Console.WriteLine("Do you want to continue adding rows in this order? (Y/N)");
        //    string answer = Console.ReadLine();
        //    while (answer == "")
        //    {
        //        Console.WriteLine("You must answer Y or N if you want to continue adding rows or not please.");
        //        answer = Console.ReadLine();
        //    }

        //    if (answer.ToLower() == "y")
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }

        //}


        //===========================================================================================================================
        //List all orders. Select order to change.
        public void OrderList()
        {
            string orderNumber = "";
            int orderNumberInt;

            do
            {
                string message;
                Console.Clear();
                Console.WriteLine("\tOrdernumber\tCustnr\tCustomer\n\t----------------------------------");
                Store.orderHeaderList = Store.GetAllOrderHeaders();
                foreach (OrderHeader orderheader in Store.orderHeaderList)
                {
                    string customernumber = orderheader.customerNumber.ToString();
                    Console.WriteLine("\t  " + orderheader.orderNumber + "\t\t  " + customernumber + "\t" + Store.GetCustomerByNumber(customernumber, out message).name);
                }
                do
                {
                    Console.Write("\n\tEnter ordernumber (Just Enter for quit): ");
                    orderNumber = Console.ReadLine();
                    if (int.TryParse(orderNumber, out orderNumberInt) && Store.orderHeaderList.Exists(item => item.orderNumber == orderNumberInt))
                    {
                        ChangeOrder(Store, orderNumber);
                    }

                } while (orderNumber != "" && orderNumberInt == 0);

            } while (orderNumber != "");

        }



        //===========================================================================================================================
        //List all orders for customer
        private void ListOrderForCustomer()
        {
            string customerNumber, message;

            Console.Clear();
            Console.WriteLine("\tList all ordernumbers for customer\n\t------------------------------------------");

            //Input customernumber.
            customerNumber = InputCustomerNumber();
            if (customerNumber != "")
            {
                Store.orderHeaderList = Store.GetOrderHeadersByCustomerNumber(customerNumber, out message);
                Console.WriteLine("\n\tOrderNr\n\t-------");
                foreach (OrderHeader orderHeader in Store.orderHeaderList)
                {
                    Console.WriteLine("\t  " + orderHeader.orderNumber);
                }
            }
            Console.ReadKey();

        }


        //===========================================================================================================================
        //Change available qunatity for a product.
        private void ChangeProductQuantity()
        {
            Console.Clear();
            Console.WriteLine("Change the number of products in store: ");
            Console.Write("Enter product number: ");
            string inputProductNumber = Console.ReadLine();
            while (inputProductNumber == "")
            {
                Console.WriteLine("Product number cannot be empty. Enter a valid product number please: ");
                inputProductNumber = Console.ReadLine();
            }
            Console.Write("Enter the new amount for this product: ");
            string inputProductQuantity = Console.ReadLine();
            while (inputProductQuantity == "")
            {
                Console.WriteLine("The number of products cannot be empty. Enter zero or a positive number please: ");
                inputProductQuantity = Console.ReadLine();
            }
            string result = Store.ModifyProductQuantity(inputProductNumber, inputProductQuantity);
            if (result != "")
            {
                Console.WriteLine(result);
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("The product's amount is now changed. Press enter to continue.");
                Console.ReadLine();
            }
        }


        //===========================================================================================================================
        //Change price for a product.
        private void ChangeProductPrice()
        {
            Console.Clear();
            Console.WriteLine("Change the price on a product");
            Console.Write("Enter product number: ");
            string inputProductNumber = Console.ReadLine();
            while (inputProductNumber == "")
            {
                Console.WriteLine("Product number cannot be empty. Enter a valid product number please: ");
                inputProductNumber = Console.ReadLine();
            }
            Console.Write("Enter the new price on the product: ");
            string inputProductPrice = Console.ReadLine();
            while (inputProductPrice == "")
            {
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

        //===========================================================================================================================
        //Add a product
        private void AddProduct()
        {
            Console.Clear();
            Console.WriteLine("Register a new product");
            Console.Write("Enter product number: ");
            string inputProductNumber = Console.ReadLine();
            while (inputProductNumber == "")
            {
                Console.WriteLine("Product number cannot be empty. Enter a valid product number please: ");
                inputProductNumber = Console.ReadLine();
            }
            Console.Write("Enter product name: ");
            string inputProductName = Console.ReadLine();
            while (inputProductName == "")
            {
                Console.WriteLine("Product name cannot be empty. Enter a valid product name please: ");
                inputProductName = Console.ReadLine();
            }
            Console.Write("Enter product price: ");
            string inputProductPrice = Console.ReadLine();
            while (inputProductPrice == "")
            {
                Console.WriteLine("Product price cannot be empty. Enter a valid product price please: ");
                inputProductPrice = Console.ReadLine();
            }
            Console.Write("Enter product quantity: ");
            string inputProductQuantity = Console.ReadLine();
            while (inputProductQuantity == "")
            {
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


        //===========================================================================================================================
        //Add customer.
        private void NewCustomer()
        {
            Console.Clear();
            Console.WriteLine("Register a new customer");
            Console.Write("Enter customer name: ");
            string inputCustomerName = Console.ReadLine();
            while (inputCustomerName == "")
            {
                Console.WriteLine("Customer name cannot be empty. Enter a valid customer name please: ");
                inputCustomerName = Console.ReadLine();
            }
            Console.Write("Enter customer address: ");
            string inputCustomerAddress = Console.ReadLine();
            while (inputCustomerAddress == "")
            {
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




        //**********************************************************************************************************************************
        // Submetoder till menyerna.
        //**********************************************************************************************************************************


        //**********************************************************************************************************************************
        private string InputCustomerNumber()
        {
            string customerNumber, message;

            //Check for customer.
            do
            {
                Console.Write("\tEnter customer number (Enter-Quit): ");
                customerNumber = Console.ReadLine();

                //Quit if blank.
                if (customerNumber == "")
                    return "";

                //Get customer.
                Store.customerObj = Store.GetCustomerByNumber(customerNumber, out message);

                if (Store.customerObj == null)
                {
                    Console.Write("\t  Customer not found!");
                    Console.ReadKey();
                }
            } while (Store.customerObj == null);

            return customerNumber;

        }


        //================================================================================================================
        // Change Order.
        //================================================================================================================
        private void ChangeOrder(StoreInterface store, string orderNumber)
        {
            string message, action = "", currentAction = "";

            store.orderheaderObj = store.GetOrderHeaderByOrderNumber(orderNumber, out message);
            store.customerObj = store.GetCustomerByNumber(store.orderheaderObj.customerNumber.ToString(), out message);
            do
            {
                Console.Clear();
                Console.WriteLine("\tOrdernumber: " + orderNumber + "  Customernumber: " + store.customerObj.number + "  Customer: " + store.customerObj.name);
                Console.WriteLine("\n\tRow\tProd.nr\tProd.name\tPrice\tQuantity");
                Console.WriteLine("\t-------------------------------------------------------------------------------");
                store.orderRowList = store.GetOrderRows(orderNumber, out message);
                foreach (OrderRow orderrow in store.orderRowList)
                {
                    Console.WriteLine("\t" + orderrow.rowNumber + "\t" + orderrow.productNumber + "\t" + orderrow.productName + "\t\t" + orderrow.productPrice + "\t" + orderrow.quantity);
                }

                if (currentAction == "")
                {
                    Console.Write("\n\t1-Add row  2-Change row  3-Delete row  4-Delete order  Enter-Exit: ");
                    action = Console.ReadLine();
                }

                switch (action)
                {
                    case "1":
                        currentAction = AddOrderRow(orderNumber);
                        break;

                    case "2":
                        currentAction = ChangeOrderRow(orderNumber);
                        break;

                    case "3":
                        currentAction = DeleteOrderRow(orderNumber);
                        break;

                    case "4":
                        if (DeleteOrder(orderNumber))
                            action = "";
                        break;

                }
            } while (action != "");

        }


        //================================================================================================================
        // Add Orderrow.
        // If just return inputed on a parameter the method will return blank to indicate exit.
        //================================================================================================================
        private string AddOrderRow(string orderNumber)
        {
            string message = "", rowNumber, productNumber, quantity;



            Console.WriteLine("\n\tAdd orderrow\n\t------------");
            Console.Write("\tEnter Productnumber (Enter=Exit): ");
            productNumber = Console.ReadLine();

            //--- Quit if blank. ---
            if (productNumber == "") return "";

            Store.productObj = Store.GetProductByNumber(productNumber);
            if (Store.productObj != null)
            {
                do
                {
                    Console.Write("\tEnter quantity (Enter=Exit): ");
                    quantity = Console.ReadLine();

                    //--- Quit if blank. ---
                    if (quantity == "") return "";

                    rowNumber = Store.AddOrderRow(orderNumber, productNumber, quantity, out message);
                    if (rowNumber == "0")
                    {
                        Console.Write("\t" + message);
                        Console.ReadKey();
                        Console.WriteLine();
                    }

                } while (rowNumber == "0");
            }
            else
            {
                Console.Write("\tProduct not found!");
                Console.ReadKey();
            }

            return "Remain";

        }


        //================================================================================================================
        // Change Orderrow.
        // If just return inputed on a parameter the method will return blank to indicate exit.
        //================================================================================================================
        private string ChangeOrderRow(string orderNumber)
        {
            string message = "", rowNumber, quantity;
            int rowNumberInt;
            bool exit = true; ;

            Console.WriteLine("\n\tChange orderrow\n\t------------");

            do
            {
                Console.Write("\tEnter Rownumber (Enter=Exit): ");
                rowNumber = Console.ReadLine();

                //--- Quit if blank. ---
                if (rowNumber == "") return "";

                if (int.TryParse(rowNumber, out rowNumberInt))
                {
                    if (!Store.orderRowList.Exists(item => item.rowNumber == rowNumberInt))
                    {
                        Console.Write("\tRownumber does not exists!");
                        Console.ReadKey();
                        rowNumberInt = 0;
                    }
                }
                else
                {
                    Console.Write("\tRownumber must be an integer greater than 0 ! ");
                    Console.ReadKey();
                }

            } while (rowNumberInt == 0);


            do
            {
                Console.Write("\tEnter new quantity (Enter=Exit): ");
                quantity = Console.ReadLine();

                //--- Quit if blank. ---
                if (quantity == "") return "";

                exit = Store.ChangeOrderRow(orderNumber, rowNumber, quantity, out message);

                if (!exit)
                {
                    Console.Write("\t\t" + message);
                    Console.ReadKey();
                    Console.WriteLine();
                }

            } while (!exit);

            return "Remain";

        }


        //================================================================================================================
        // Delete Orderrow.
        //================================================================================================================
        private string DeleteOrderRow(string orderNumber)
        {

            string message, rowNumber;

            Console.WriteLine("\n\tDelete orderrow\n\t---------------");
            Console.Write("\tEnter Rownumber (Enter=Exit): ");

            rowNumber = Console.ReadLine();
            if (rowNumber != "")
            {
                if (Store.DeleteOrderRow(orderNumber, rowNumber, out message))
                {
                    Console.Write("\t\tOrderrow deleted.");
                }
                else
                {
                    Console.Write("\t\t" + message);
                }
                Console.ReadKey();
            }

            if (rowNumber == "")
                return "";
            else
                return "Remain";

        }


        //================================================================================================================
        // Delete Order.
        //================================================================================================================
        private bool DeleteOrder(string orderNumber)
        {
            bool returnValue = false;
            string message;


            //--- Delete order. ---
            if (Store.DeleteOrder(orderNumber, out message))
            {
                Console.Write("\n\tOrder deleted successfully!");
                returnValue = true;
            }
            else
                Console.Write("\n\tOrder delete failed!");

            Console.ReadKey();

            return returnValue;
        }

    }
}
