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
            StoreInterface stIF = new StoreInterface();
            /*            stIF.AddProduct("a", "b", "1,50", "10");
                        stIF.AddProduct("2", "xxx", "100", "20");
                        stIF.AddProduct("123-45", "skorpan", "5000", "1");

                        stIF.AddCustomer("Kalle", "Lillgatan 6");
                        stIF.AddCustomer("Olle", "Storgatan 8");

                        string message;
                        string orderNumber = stIF.AddOrder("1");
                        string rowNumber = stIF.AddOrderRow(orderNumber, "232", "10", out message);

            string message=stIF.ModifyProductPrice("232", "1,65");
            message = stIF.ModifyProductQuantity("232", "36");
            stIF.SaveAllToFile();
            */


            BusinessSystemUi ui = new BusinessSystemUi();
            ui.StartApplication();
        }
    }


}
