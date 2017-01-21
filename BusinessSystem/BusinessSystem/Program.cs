using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessSystem
{
    class Program
    {
        static void Main(string[] args)
        {

            BusinessSystemUi ui = new BusinessSystemUi();
            ui.StartApplication();
            ui.EndApplication();

            //Test.OrderList();
        }

        //--- Test purpose. --
        //private static class Test
        //{
        //    static StoreInterface Store = new StoreInterface();


        //    //================================================================================================================
        //    // Orderlist.
        //    //================================================================================================================
        //    public static void OrderList()
        //    {
        //        string orderNumber = "";
        //        int orderNumberInt;

        //        do
        //        {
        //            string message;
        //            Console.Clear();
        //            Console.WriteLine("\tOrdernumber\tCustnr\tCustomer\n\t----------------------------------");
        //            Store.orderHeaderList = Store.GetAllOrderHeaders();
        //            foreach (OrderHeader orderheader in Store.orderHeaderList)
        //            {
        //                string customernumber = orderheader.customerNumber.ToString();
        //                Console.WriteLine("\t  " + orderheader.orderNumber + "\t\t  " + customernumber + "\t" + Store.GetCustomerByNumber(customernumber, out message).name);
        //            }
        //            do
        //            {
        //                Console.Write("\n\tEnter ordernumber (exit with Enter): ");
        //                orderNumber = Console.ReadLine();
        //                if (int.TryParse(orderNumber, out orderNumberInt) && Store.orderHeaderList.Exists(item => item.orderNumber == orderNumberInt))
        //                {
        //                    ChangeOrder(Store, orderNumber);
        //                }

        //            } while (orderNumber != "" && orderNumberInt == 0);

        //        } while (orderNumber != "");

        //        Store.SaveAllToFile();

        //    }


        //    //================================================================================================================
        //    // Change Order.
        //    //================================================================================================================
        //    private static void ChangeOrder(StoreInterface store, string orderNumber)
        //    {
        //        string message, action = "", currentAction = "";

        //        store.orderheaderObj = store.GetOrderHeaderByOrderNumber(orderNumber, out message);
        //        store.customerObj = store.GetCustomerByNumber(store.orderheaderObj.customerNumber.ToString(), out message);
        //        do
        //        {
        //            Console.Clear();
        //            Console.WriteLine("\tOrdernumber: " + orderNumber + "  Customernumber: " + store.customerObj.number + "  Customer: " + store.customerObj.name);
        //            Console.WriteLine("\n\tRow\tProd.nr\tProd.name\tPrice\tQuantity");
        //            Console.WriteLine("\t-------------------------------------------------------------------------------");
        //            store.orderRowList = store.GetOrderRows(orderNumber, out message);
        //            foreach (OrderRow orderrow in store.orderRowList)
        //            {
        //                Console.WriteLine("\t" + orderrow.rowNumber + "\t" + orderrow.productNumber + "\t" + orderrow.productName + "\t\t" + orderrow.productPrice + "\t" + orderrow.quantity);
        //            }

        //            if (currentAction == "")
        //            {
        //                Console.Write("\n\t1-Add row  2-Change row  3-Delete row  4-Delete order  Enter-Exit: ");
        //                action = Console.ReadLine();
        //            }

        //            switch (action)
        //            {
        //                case "1":
        //                    currentAction = AddOrderRow(orderNumber);
        //                    break;

        //                case "2":
        //                    currentAction = ChangeOrderRow(orderNumber);
        //                    break;

        //                case "3":
        //                    currentAction = DeleteOrderRow(orderNumber);
        //                    break;

        //                case "4":
        //                    if (DeleteOrder(orderNumber))
        //                        action = "";
        //                    break;

        //            }
        //        } while (action != "");

        //    }


        //    //================================================================================================================
        //    // Add Orderrow.
        //    // If just return inputed on a parameter the method will return blank to indicate exit.
        //    //================================================================================================================
        //    private static string AddOrderRow(string orderNumber)
        //    {
        //        string message = "", rowNumber, productNumber, quantity;



        //        Console.WriteLine("\n\tAdd orderrow\n\t------------");
        //        Console.Write("\tEnter Productnumber (Enter=Exit): ");
        //        productNumber = Console.ReadLine();

        //        //--- Quit if blank. ---
        //        if (productNumber == "") return "";

        //        Store.productObj = Store.GetProductByNumber(productNumber);
        //        if (Store.productObj != null)
        //        {
        //            do
        //            {
        //                Console.Write("\tEnter quantity (Enter=Exit): ");
        //                quantity = Console.ReadLine();

        //                //--- Quit if blank. ---
        //                if (quantity == "") return "";

        //                rowNumber = Store.AddOrderRow(orderNumber, productNumber, quantity, out message);
        //                if (rowNumber == "0")
        //                {
        //                    Console.Write("\t" + message);
        //                    Console.ReadKey();
        //                    Console.WriteLine();
        //                }

        //            } while (rowNumber == "0");
        //        }
        //        else
        //        {
        //            Console.Write("\tProduct not found!");
        //            Console.ReadKey();
        //        }

        //        return "Remain";

        //    }


        //    //================================================================================================================
        //    // Change Orderrow.
        //    // If just return inputed on a parameter the method will return blank to indicate exit.
        //    //================================================================================================================
        //    private static string ChangeOrderRow(string orderNumber)
        //    {
        //        string message = "", rowNumber, quantity;
        //        int rowNumberInt;
        //        bool exit = true; ;

        //        Console.WriteLine("\n\tChange orderrow\n\t------------");

        //        do
        //        {
        //            Console.Write("\tEnter Rownumber (Enter=Exit): ");
        //            rowNumber = Console.ReadLine();

        //            //--- Quit if blank. ---
        //            if (rowNumber == "") return "";

        //            if (int.TryParse(rowNumber, out rowNumberInt))
        //            {
        //                if (!Store.orderRowList.Exists(item => item.rowNumber == rowNumberInt))
        //                {
        //                    Console.Write("\tRownumber does not exists!");
        //                    Console.ReadKey();
        //                    rowNumberInt = 0;
        //                }
        //            }
        //            else
        //            {
        //                Console.Write("\tRownumber must be an integer greater than 0 ! ");
        //                Console.ReadKey();
        //            }

        //        } while (rowNumberInt == 0);


        //        do
        //        {
        //            Console.Write("\tEnter new quantity (Enter=Exit): ");
        //            quantity = Console.ReadLine();

        //            //--- Quit if blank. ---
        //            if (quantity == "") return "";

        //            exit = Store.ChangeOrderRow(orderNumber, rowNumber, quantity, out message);

        //            if (!exit)
        //            {
        //                Console.Write("\t\t" + message);
        //                Console.ReadKey();
        //                Console.WriteLine();
        //            }

        //        } while (!exit);

        //        return "Remain";

        //    }


        //    //================================================================================================================
        //    // Delete Orderrow.
        //    //================================================================================================================
        //    private static string DeleteOrderRow(string orderNumber)
        //    {

        //        string message, rowNumber;

        //        Console.WriteLine("\n\tDelete orderrow\n\t---------------");
        //        Console.Write("\tEnter Rownumber (Enter=Exit): ");

        //        rowNumber = Console.ReadLine();
        //        if (rowNumber != "")
        //        {
        //            if (Store.DeleteOrderRow(orderNumber, rowNumber, out message))
        //            {
        //                Console.Write("\t\tOrderrow deleted.");
        //            }
        //            else
        //            {
        //                Console.Write("\t\t" + message);
        //            }
        //            Console.ReadKey();
        //        }

        //        if (rowNumber == "")
        //            return "";
        //        else
        //            return "Remain";

        //    }


        //    //================================================================================================================
        //    // Delete Order.
        //    //================================================================================================================
        //    private static bool DeleteOrder(string orderNumber)
        //    {
        //        bool returnValue = false;
        //        string message;


        //        //--- Delete order. ---
        //        if (Store.DeleteOrder(orderNumber, out message))
        //        {
        //            Console.Write("\n\tOrder deleted successfully!");
        //            returnValue = true;
        //        }
        //        else
        //            Console.Write("\n\tOrder delete failed!");

        //        Console.ReadKey();

        //        return returnValue;
        //    }

        //}

    }
}
