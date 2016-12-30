using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessSystem
{
    // Class Store interface. Channel to Store class. ---
    public class StoreInterface
    {
        Store<Product> store = new Store<Product>();


        //==============================================================================================
        // Add product.
        // Input parameters are all string values from input source (console).
        // The parameters will be converted to respective format.
        // Returns an empty string (no error message) if added ok. 
        //==============================================================================================
        public string AddProduct(string number, string name, string price, string quantity)
        {
            string errorMessage = "";
            double priceDbl;    //- Price as double
            int quantityInt;    //- Quantity as integer

            //--- Make sure number and name not an empty string, price is double, quantity is integer. ---
            bool formatOk = ((number != null & number != "") & (name != null & name != "")
                & double.TryParse(price, out priceDbl) & int.TryParse(quantity, out quantityInt));

            if (formatOk)
            {
                Product product = new Product(number, name, priceDbl, quantityInt);

                //--- Add product if not already in the store (number or name must not exists). ---
                if (!store.AddProduct(product))
                {
                    errorMessage = "Product already in store.";
                }

            }
            else
            {
                errorMessage = "Illegal format. Number, name must not be empty, price needs to be a number (i.e. 12,50), quantity an integer number.";
            }

            return errorMessage;
        }
    }





}
