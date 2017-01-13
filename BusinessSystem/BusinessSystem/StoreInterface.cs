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
        static readonly string dataFolder = Directory.GetCurrentDirectory() + @"\Data";
        static readonly string productsFile = dataFolder + @"\products.txt";
        static readonly string customersFile = dataFolder + @"\customers.txt";
        static readonly string ordersFile = dataFolder + @"\orders.txt";
        const char separator = ';';

        Products<Product> productsObj = new Products<Product>();
        Customers<Customer> customersObj = new Customers<Customer>();
        Orders<Order> ordersObj = new Orders<Order>();

        public List<Product> productList;
        public List<Customer> customerList;
        public List<OrderHeader> orderHeaderList;
        public List<OrderRow> orderRowList;


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
            decimal priceDec; //- Price as decimal
            int quantityInt; //- Quantity as integer

            //--- Make sure number and name not an empty string, price is double, quantity is integer. ---
            bool formatOk = ((number != null & number != "") & (name != null & name != "")
                             & decimal.TryParse(price, out priceDec) & int.TryParse(quantity, out quantityInt));

            if (formatOk)
            {
                //Product product = new Product(number, name, priceDec, quantityInt);

                //--- Add product if not already in the store (number or name must not exists). ---
                if (!productsObj.AddProduct(number, name, priceDec, quantityInt))
                {
                    errorMessage = "Product already in store.";
                }

            }
            else
            {
                errorMessage =
                    "Illegal format. Number, name must not be empty, price needs to be a number (i.e. 12,50), quantity an integer number.";
            }

            return errorMessage;
        }



        //==============================================================================================
        // Get products.
        //==============================================================================================
        public List<Product> GetOrderHeaders()
        {
            return null;
        }

        //==============================================================================================
        // Change price for product.
        // Returns empty string if succeeded, else an error message. 
        //==============================================================================================
        public string ModifyProductPrice(string productNumber, string priceNew)
        {
            string message = "";

            if (!productsObj.ChangeProductPrice(productNumber, decimal.Parse(priceNew)))
            {
                message = "Product '" + productNumber + "' does not exist!";
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

            if (!productsObj.ChangeProductQuantity(productNumber, int.Parse(quantityNew)))
            {
                message = "Product '" + productNumber + "' does not exist!";
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
                int customerNumber = customersObj.AddCustomer(name, address);
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
        public List<Customer>GetCustomer(string number, string name, string address)
        {
            //--- Get customer by number. Customer number is unique so only one hit possible. ---
            if (number != "")
            {
                int customerNumber = int.Parse(number);
                return customersObj.GetCustomerByNumber(customerNumber).ToList();
            }

            //--- Get customers by name. Multiple hits possible since name not unique. ---
            else if (name != "" & address == "")
            {
                return customersObj.GetCustomerByName(name).ToList();
            }

            //--- Get customers by address. Multiple hits possible since address not unique. ---
            else if (address != "" && name == "")
            {
                return customersObj.GetCustomerByAddress(address).ToList();
            }

            //--- Get customers by name and address. Customer name and address together is unique so only one hit possible. ---
            else if (name != "" & address != "")
            {
                return customersObj.GetCustomerByNameAddress(name, address).ToList();
            }

            //--- No paramters specified. ---
            else
            {
                return null;
            }

        }


        //==============================================================================================
        // Add orderheader from manual input.
        // Returns Ordernumber for the new orderheader if succeeded, otherwise error message.
        //==============================================================================================
        public string AddOrderHeader(string customerNumber, out string errorMessage)
        {
            errorMessage = "";
            int customerNumberInt;
            int orderNumber=0;

            //--- Empty customernumber terminates input. ---
            if (customerNumber != "")
            {
                //--- Check if Customernumber is an integer. ---
                if (!int.TryParse(customerNumber, out customerNumberInt))
                {
                    errorMessage = "Customer number must be an integer value!";
                }
                else
                {
                    Customer[] customers = customersObj.GetCustomerByNumber(customerNumberInt);
                    if (customers.Length == 0)
                    {
                        errorMessage = "Customer number does not exist!";
                    }
                    else
                    {
                        orderNumber = ordersObj.GetHighestOrderNumber() + 1;
                        AddOrderHeader(orderNumber, customerNumberInt, customers[0].name);
                    }
                }
            }

            return orderNumber.ToString();

        }


        //==============================================================================================
        // Add orderheader to generic orders list.
        //==============================================================================================
        private void AddOrderHeader(int orderNumber, int customerNumber, string customerName)
        {
            OrderHeader orderHeader = new OrderHeader(orderNumber, customerNumber, customerName);
            ordersObj.orders.Add(orderHeader);
        }


        //==============================================================================================
        // Get orderheaders for specific customer.
        //==============================================================================================
        public List<OrderHeader> GetOrderHeaders(string customerNumber)
        {
            return null;
        }


        //======================================================================================================
        // Add orderrow from manually input.
        // Returns Rownumber for the order if succeeded otherwise 0. If error message returns the error message.
        //======================================================================================================
        public string AddOrderRow(string orderNumber, string productNumber, string quantity, out string message)
        {

            //--- Get product details. ---
            Product product = productsObj.GetProductByNumber(productNumber);
            message = "";

            if (product != null)
            {
                //--- Add order row. ---
                //int rowNumber = ordersObj.AddOrderRow(int.Parse(orderNumber), productNumber, product.name, product.price, int.Parse(quantity));
                int rowNumber = 0;

                //--- Check if ordered quantity greater than available in the store. ---
                if (int.Parse(quantity) > product.quantity)
                {
                    message = "Order may be delayed due to ordered quantity greater than quantity in the store.";
                }
                return rowNumber.ToString();
            }

            else
            {
                message = "Product '" + productNumber + "' does not exist!";
                return "0";
            }

        }


        //===================================================================================================================================
        // Add orderrow.
        //===================================================================================================================================
        public void AddOrderRow(int orderNumber, int rowNumber, string productNumber, string productName, decimal productPrice, int quantity)
        {
            OrderRow orderRow = new OrderRow(orderNumber, rowNumber, productNumber, productName, productPrice, quantity);
            ordersObj.orders.Add(orderRow);
        }


        //==============================================================================================
        // Get orderrows for specific ordernumber.
        //==============================================================================================
        public List<OrderRow> GetOrderRows(string orderNumber)
        {
            return null;
        }


        //==============================================================================================
        // Update orderrow for specific ordernumber.
        //==============================================================================================
        public void ChangeOrderRow(bool delete, string orderNumber, string orderRow)
        {
        }


        //==============================================================================================
        // Save all to file.
        //==============================================================================================
        public void SaveAllToFile()
        {
            SaveToFile(productsFile, productsObj.products, "Write");
            SaveToFile(customersFile, customersObj.customers, "Write");
            SaveToFile(ordersFile, ordersObj.orders.OfType<OrderHeader>().ToList(), "Write");
            SaveToFile(ordersFile, ordersObj.orders.OfType<OrderRow>().ToList(), "Append");
        }


        //==============================================================================================
        // Read all from file.
        //==============================================================================================
        public void ReadAllFromFile()
        {
            ReadFromFile(productsFile);
            ReadFromFile(customersFile);
            ReadFromFile(ordersFile);
        }


        //==============================================================================================
        // Save generic list to file.
        //==============================================================================================
        public void SaveToFile(string file, dynamic list, string action)
        {

            StreamWriter fileWriter = null;

            if (action == "Create")
            {
                fileWriter = File.CreateText(file);
            }
            else if (action == "Append")
            {
                fileWriter = File.AppendText(file);
            }

            string lineType = "Category=" + list.GetType().Name;
            fileWriter.WriteLine(lineType);

            //--- Loop through list and write one line per item. ---
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
        public void ReadFromFile(string file)
        {

            if (File.Exists(file))
            {
                StreamReader fileReader = File.OpenText(file);
                string category="";

                while (!fileReader.EndOfStream)
                {
                    bool skipSwitch = false;
                    string[] parameters = null;
                    string line = fileReader.ReadLine();
                    if (line.ToLower().Contains("category="))
                    {
                        category = line.Split('=')[1].ToLower();
                        skipSwitch = true;
                    }
                    else
                    {
                        parameters = line.Split(separator);
                    }

                    if (!skipSwitch)
                    {
                        switch (category)
                        {
                            case "product":
                                AddProduct(parameters[0], parameters[1], parameters[2], parameters[3]);
                                break;

                            case "customer":
                                int customerNumber = int.Parse(parameters[0]);
                                customersObj.InsertCustomer(customerNumber, parameters[1], parameters[2]);
                                break;

                            case "orderheader":
                                int orderNumber = int.Parse(parameters[0]);
                                customerNumber = int.Parse(parameters[1]);
                                AddOrderHeader(orderNumber, customerNumber, parameters[2]);
                                break;

                            case "orderrow":
                                orderNumber = int.Parse(parameters[0]);
                                int rowNumber = int.Parse(parameters[2]);
                                decimal productPrice = decimal.Parse(parameters[5]);
                                int quantity = int.Parse(parameters[6]);
                                AddOrderRow(orderNumber, rowNumber, parameters[3], parameters[4], productPrice, quantity);
                                break;

                        }
                    }
                }

                fileReader.Close();

            }

        }

    }
}
