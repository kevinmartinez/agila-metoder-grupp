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
            StoreInterface stIf = new BusinessSystem.StoreInterface();
            stIf.AddCustomer("kalle", "gågatan 2");
            stIf.AddCustomer("olle", "liggatan 2");

            BusinessSystemUi ui = new BusinessSystemUi();
            ui.StartApplication();
        }
    }


}
