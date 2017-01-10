using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace BusinessSystem
{
    //--- Class Store interface. Channel to Store class. ---
    public class StoreInterface
    {
        Products<Product> products = new Products<Product>();
        Customers<Customer> customers = new Customers<Customer>();
        Orders<OrderRow> orders = new Orders<OrderRow>();

        static readonly string dataFolder = Directory.GetCurrentDirectory() + @"\Data";
        static readonly string productsFile = dataFolder + @"\products.txt";
        static readonly string customersFile = dataFolder + @"\customers.txt";
        static readonly string ordersFile = dataFolder + @"\orders.txt";
        const char separator = ';';


        //==============================================================================================
        // Constructor.
        //==============================================================================================
        public StoreInterface()
        {
            if (!Directory.Exists(dataFolder))
            {
                Directory.CreateDirectory(dataFolder);
            }

            //--- Read Products, Customers and Orders from file.
            ReadAllFromFile();

        }


        //==============================================================================================
        // Add product.
        // Input parameters are all string values from input source (console).
        // The parameters will be converted to respective format.
        // Returns an empty string (no error message) if added ok. 
        //==============================================================================================
        public string AddProduct(string number, string name, string price, string quantity)
        {
            string errorMessage = "";
            decimal priceD;    //- Price as double
            int quantityInt;    //- Quantity as integer

            //--- Make sure number and name not an empty string, price is double, quantity is integer. ---
            bool formatOk = ((number != null & number != "") & (name != null & name != "")
                & decimal.TryParse(price, out priceD) & int.TryParse(quantity, out quantityInt));

            if (formatOk)
            {
                //Product product = new Product(number, name, priceDbl, quantityInt);

                //--- Add product if not already in the store (number or name must not exists). ---
                if (!products.AddProduct(number, name, priceD, quantityInt))
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


        //==============================================================================================
        // Change price for product.
        // Returns empty string if succeeded, else an error message. 
        //==============================================================================================
        public string ModifyProductPrice(string productNumber, string priceNew)
        {
            string message = "";

            if (!products.ChangeProductPrice(productNumber, decimal.Parse(priceNew)))
            {
                message = "Product '" + productNumber + "' does not exists !";
            }

            return message;

        }


        //==============================================================================================
        // Change quantity for product. 
        // Returns empty string if succeeded, else an error message. 
        //==============================================================================================
        public string ModifyProductQuantity(string productNumber, string quantityNew)
        {
            string message = "";

            if (!products.ChangeProductQuantity(productNumber, int.Parse(quantityNew)))
            {
                message = "Product '" + productNumber + "' does not exists !";
            }

            return message;

        }


        //==============================================================================================
        // Add customer.
        // Input parameters are all string values from input source (console).
        // Returns given customer number (string format) if succeeded.  Otherwise error message. 
        //==============================================================================================
        public string AddCustomer(string name, string address)
        {
            string message = "";

            //--- Make sure name and address not an empty string. ---
            if ((name != null & name != "") & (address != null & address != ""))
            {
                //--- Try to add customer. If 0 returned customer already exists. ---
                int customerNumber = customers.AddCustomer(name, address);
                if (customerNumber == 0)
                {
                    message = "Customer already exists.";
                }
                else
                {
                    message = customerNumber.ToString();
                }
            }
            else
            {
                message = "Name or Address must not be empty.";
            }

            return message;

        }


        //==============================================================================================
        // Get Customer by Number and/or name and/or address.
        //==============================================================================================
        public Customer[] GetCustomer(string number, string name, string address)
        {
            //--- Get customer by number. Customer number is unique so only one hit possible. ---
            if (number != "")
            {
                int customerNumber = int.Parse(number);
                return customers.GetCustomerByNumber(customerNumber);
            }

            //--- Get customers by name. Multiple hits possible since name not unique. ---
            else if (name != "" & address == "")
            {
                return customers.GetCustomerByName(name);
            }

            //--- Get customers by address. Multiple hits possible since address not unique. ---
            else if (address != "" && name == "")
            {
                return customers.GetCustomerByAddress(address);
            }

            //--- Get customers by name and address. Customer name and address together is unique so only one hit possible. ---
            else if (name != "" & address != "")
            {
                return customers.GetCustomerByNameAddress(name, address);
            }

            //--- No paramters specified. ---
            else
            {
                return null;
            }

        }


        //==============================================================================================
        // Add order.
        // Returns Ordernumber for the order if succeeded, otherwise error message.
        //==============================================================================================
        public string AddOrder(string customerNumber)
        {
            //--- Check that customer exists before adding order. ---
            if (customers.GetCustomerByNumber(int.Parse(customerNumber)).Length > 0)
            {
                return orders.AddOrder(int.Parse(customerNumber)).ToString();
            }
            else
            {
                return "Customer number '" + customerNumber + "' does not exists !";
            }
        }


        //======================================================================================================
        // Add orderrow.
        // Returns Rownumber for the order if succeeded otherwise 0. If error message returns the error message.
        //======================================================================================================
        public string AddOrderRow(string orderNumber, string productNumber, string quantity, out string message)
        {
            Product product = products.GetProductByNumber(productNumber);
            message = "";

            if (product != null)
            {
                //--- Add order row. ---
                int rowNumber = orders.AddOrderRow(int.Parse(orderNumber), productNumber, product.name, product.price, int.Parse(quantity));

                //--- Check if ordered quantity greater than available in the store. ---
                if (int.Parse(quantity) > product.quantity)
                {
                    message = "Order may be delayed due to ordered quantity greater than quantity in the store.";
                }
                return rowNumber.ToString();
            }

            else
            {
                message = "Product '" + productNumber + "' does not exists !";
                return "0";
            }

        }


        //==============================================================================================
        // Save all to file.
        //==============================================================================================
        public void SaveAllToFile()
        {
            SaveToFile(productsFile, products.products);
            SaveToFile(customersFile, customers.customers);
            SaveToFile(ordersFile, orders.orderRows);
        }


        //==============================================================================================
        // Read all from file.
        //==============================================================================================
        public void ReadAllFromFile()
        {
            ReadFromFile(productsFile, "Product");
            ReadFromFile(customersFile, "Customer");
            ReadFromFile(ordersFile, "Order");
        }


        //==============================================================================================
        // Save generic list to file.
        //==============================================================================================
        public void SaveToFile(string file, dynamic list)
        {

            StreamWriter fileWriter = File.CreateText(file);

            //--- Loop through product list. ---
            foreach (object item in list)
            {
                //--- Clear line to be written to file. ---
                string line = "";

                //--- Loop through every property and add to line and append separator char. ---
                foreach (PropertyInfo property in item.GetType().GetProperties())
                {
                    line += property.GetValue(item).ToString() + separator;
                }

                //-- Remove last separator char. ---
                line = line.Substring(0, line.Length - 1);

                fileWriter.WriteLine(line);
            }

            fileWriter.Close();
        }


        //==============================================================================================
        // Read from file.
        //==============================================================================================
        public void ReadFromFile(string file, string category)
        {

            if (File.Exists(file))
            {
                StreamReader fileReader = File.OpenText(file);
                while (!fileReader.EndOfStream)
                {
                    string line = fileReader.ReadLine();
                    string[] parameters = line.Split(separator);

                    switch (category)
                    {
                        case "Product":
                            AddProduct(parameters[0], parameters[1], parameters[2], parameters[3]);
                            break;

                        case "Customer":
                            int customerNumber = int.Parse(parameters[0]);
                            customers.InsertCustomer(customerNumber, parameters[1], parameters[2]);
                            break;

                        case "Order":
                            int orderNumber = int.Parse(parameters[0]);
                            customerNumber = int.Parse(parameters[1]);
                            int rowNumber = int.Parse(parameters[2]);
                            decimal productPrice = decimal.Parse(parameters[5]);
                            int quantity = int.Parse(parameters[6]);
                            orders.InsertOrderRow(orderNumber, customerNumber, rowNumber, parameters[3], parameters[4], productPrice, quantity);
                            break;

                    }
                }

                fileReader.Close();

            }

        }

    }
}
