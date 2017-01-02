using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessSystem {
    class Program {
        static void Main(string[] args) {
            BusinessSystemUi ui = new BusinessSystemUi();
            ui.StartApplication();
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
