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

        //--- These lists and objects maybe be used in the Userinterface class when retrieving data. ---
        public List<Product> productList;
        public List<Customer> customerList;
        public List<OrderHeader> orderHeaderList;
        public List<OrderRow> orderRowList;
        public Product productObj;
        public Customer customerObj;
        public OrderHeader orderheaderObj;
        public OrderRow orderRowObj;


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

            //--- Make sure number and name not an empty string, price is decimal, quantity is integer. ---
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
        // Get all products.
        //==============================================================================================
        public List<Product> GetAllProducts()
        {
            return productsObj.GetAllProducts();
        }


        //==============================================================================================
        // Get specific product.
        //==============================================================================================
        public Product GetProductByNumber(string productNumber)
        {
            if (productNumber == null)
            { productNumber = ""; }

            return productsObj.GetProductByNumber(productNumber);
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
        // Get Customer by Number.
        //==============================================================================================
        public Customer GetCustomerByNumber(string customerNumber, out string message)
        {
            int customerNumberInt;
            message = "";

            if (int.TryParse(customerNumber, out customerNumberInt) && customerNumberInt > 0)
            {
                return customersObj.GetCustomerByNumber(customerNumberInt);
            }
            else
            {
                message = "Customer number must be integer and greater than 0!";
                return null;
            }
        }


        //==============================================================================================
        // Get Customer by name and/or address.
        //==============================================================================================
        public List<Customer> GetCustomers(string name, string address)
        {

            //--- Get customers by name. Multiple hits possible since name not unique. ---
            if (name != "" & address == "")
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
        // Delete order.
        //==============================================================================================
        public bool DeleteOrder(string orderNumber, out string message)
        {

            bool returnValue = false;
            message = "";

            //--- Check that ordernumber is ok. ---
            if (OrderParametersCheck(orderNumber, "", "", out message))
            {
                OrderHeader orderHeader = ordersObj.GetOrderHeaderByOrderNumber(int.Parse(orderNumber));
                if (orderHeader != null)
                {
                    //--- Delete all orderrows in the order. ---
                    foreach (OrderRow orderRow in ordersObj.GetOrderRowsByOrderNumber(int.Parse(orderNumber)))
                       DeleteOrderRow(orderRow.orderNumber,orderRow.rowNumber);

                    //--- Remove Orderheader. ---
                    ordersObj.orders.Remove(orderHeader);
                    returnValue = true;
                }

                else
                    message = "Order not found!";
            }

            return returnValue;

        }


        //==============================================================================================
        // Add orderheader from manual input.
        // Returns Ordernumber for the new orderheader if succeeded, otherwise error message.
        //==============================================================================================
        public string AddOrderHeader(string customerNumber, out string errorMessage)
        {
            errorMessage = "";
            int customerNumberInt;
            int orderNumber = 0;

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
                    Customer customer = customersObj.GetCustomerByNumber(customerNumberInt);
                    if (customer == null)
                    {
                        errorMessage = "Customer number does not exist!";
                    }
                    else
                    {
                        orderNumber = ordersObj.GetHighestOrderNumber() + 1;
                        AddOrderHeader(orderNumber, customerNumberInt);
                    }
                }
            }

            return orderNumber.ToString();

        }


        //==============================================================================================
        // Add orderheader to generic orders list.
        // Overloaded. All parameters in their real type.
        //==============================================================================================
        private void AddOrderHeader(int orderNumber, int customerNumber)
        {
            OrderHeader orderHeader = new OrderHeader(orderNumber, customerNumber);
            ordersObj.orders.Add(orderHeader);
        }


        //=================================================================================================
        // Get all orderheaders.
        //=================================================================================================
        public List<OrderHeader> GetAllOrderHeaders()
        {
            return ordersObj.GetAllOrderHeaders();
        }


        //=================================================================================================
        // Get orderheader for specific ordernumber.
        //=================================================================================================
        public OrderHeader GetOrderHeaderByOrderNumber(string orderNumber, out string message)
        {
            int orderNumberInt;
            message = "";

            //--- Convert ordernumber to integer. Get orderheader for specific ordernumber. ---
            if (int.TryParse(orderNumber, out orderNumberInt) && orderNumberInt > 0)
            {
                return ordersObj.GetOrderHeaderByOrderNumber(orderNumberInt);
            }
            else
            {
                message = "Ordernumber must be an integer and greater than 0!";
                return null;
            }
        }


        //=================================================================================================
        // Get orderheaders for specific customer.
        //=================================================================================================
        public List<OrderHeader> GetOrderHeadersByCustomerNumber(string customerNumber, out string message)
        {
            int customerNumberInt;
            message = "";

            //--- Convert inparameter customernumber to integer. Get orderheaers for customer if number an integer.
            if (int.TryParse(customerNumber, out customerNumberInt))
            {
                return ordersObj.GetOrderHeadersByCustomerNumber(customerNumberInt);
            }
            else
            {
                message = "Customernumber must be an integer!";
                return null;
            }

        }


        //======================================================================================================
        // Add orderrow from manually input.
        // Inparameters in string format.
        // Returns rownumber for the orderrow added if succeeded and a message specifying if order may be delayed.
        // If error, rownumber "0" returned and an errormessage.
        //======================================================================================================
        public string AddOrderRow(string orderNumber, string productNumber, string quantity, out string message)
        {
            string returnValue = "0";
            int orderNumberInt = 0, rowNumberInt = 0, quantityInt = 0;
            message = "";


            //--- Check that ordernumber and quantity is integer and greater than 0. ---
            if (!OrderParametersCheck(orderNumber, "", quantity, out message))
            {
                //--- Check failed, return 0. ---
                return "0";
            }


            //--- Ordernumber and quantity passed check. Get product details. ---
            Product product = productsObj.GetProductByNumber(productNumber);

            if (product != null)
            {
                orderNumberInt = int.Parse(orderNumber);
                quantityInt = int.Parse(quantity);
                //--- Check if ordered quantity greater than available in the store. ---
                if (quantityInt > product.quantity)
                {
                    message = "Order may be delayed due to ordered quantity greater than quantity in the store.";
                }

                //--- Set next rownumber for current order. ---
                rowNumberInt = ordersObj.GetHighestRowNumberByOrderNumber(orderNumberInt) + 1;

                //--- Add order row with parameters with it's real type. ---
                AddOrderRow(orderNumberInt, rowNumberInt, productNumber, product.name, product.price, quantityInt);

                //--- Change available quantity (subtract ordered quantity) in the store for the current product. ---
                productsObj.AddProductQuantity(productNumber, -quantityInt);

                returnValue = rowNumberInt.ToString();
            }
            else
                message = "Product '" + productNumber + "' does not exist!";

            return returnValue; ;

        }


        //===================================================================================================================================
        // Add orderrow. 
        //===================================================================================================================================
        private void AddOrderRow(int orderNumber, int rowNumber, string productNumber, string productName, decimal productPrice, int quantity)
        {
            OrderRow orderRow = new OrderRow(orderNumber, rowNumber, productNumber, productName, productPrice, quantity);
            ordersObj.orders.Add(orderRow);
        }


        //==============================================================================================
        // Get all orderrows for specific ordernumber.
        //==============================================================================================
        public List<OrderRow> GetOrderRows(string orderNumber, out string message)
        {
            int orderNumberInt;
            message = "";

            //--- Ordernumber must be an integer greater then 0. ---
            if (int.TryParse(orderNumber, out orderNumberInt) && orderNumberInt > 0)
            {
                return ordersObj.GetOrderRowsByOrderNumber(orderNumberInt);
            }
            else
            {
                message = "Ordernumber must be an integer and greater than zero!";
                return null;
            }

        }


        //===================================================================================================================================
        // Delete orderrow. Called from UI.
        //===================================================================================================================================
        public bool DeleteOrderRow(string orderNumber, string rowNumber, out string message)
        {
            bool returnValue = true;
            message = "";

            //--- Check that Order and rownumber in correct format. ---
            if (OrderParametersCheck(orderNumber, rowNumber, "", out message))
            {
                //--- Delete orderrow. ---
                if (!DeleteOrderRow(int.Parse(orderNumber), int.Parse(rowNumber)))
                {
                    message = "Orderrow does not exists!";
                    returnValue = false;
                }
            }
            else
                returnValue = false;

            return returnValue;
        }


        //===================================================================================================================================
        // Delete orderrow. True returned if orderrow existed.
        //===================================================================================================================================
        public bool DeleteOrderRow(int orderNumber, int rowNumber)
        {
            //--- Get orderrow. ---
            OrderRow orderRow = ordersObj.GetOrderRowByOrderNumber(orderNumber, (rowNumber));

            if (orderRow != null)
            {
                //--- Adjust quantity (add) for current product in store. ---
                productsObj.AddProductQuantity(orderRow.productNumber, orderRow.quantity);

                //--- Delete orderrow. ---
                ordersObj.orders.Remove(orderRow);

                return true;
            }
            else
                return false;
        }


        //================================================================================================================
        // Change orderrow for specific ordernumber. Quantity will be changed.
        //================================================================================================================
        public bool ChangeOrderRow(string orderNumber, string rowNumber, string quantity, out string message)
        {
            bool returnOk = true;
            message = "";


            //--- Check that ordernumber, rownumber and quantity is integer and greater than 0. ---
            returnOk = OrderParametersCheck(orderNumber, rowNumber, quantity, out message);


            //--- If Ordernumber, rownumber and quantity passed check. Get product details. ---
            if (returnOk)
            {
                OrderRow orderRow = ordersObj.GetOrderRowByOrderNumber(int.Parse(orderNumber), int.Parse(rowNumber));
                if (orderRow != null)
                {
                    //--- Convert new quantity to integer. ---
                    int quantityNew = int.Parse(quantity);

                    //--- Change available quantity for the current product, i.e. add the difference between the old and new quantity. ---
                    productsObj.AddProductQuantity(orderRow.productNumber, (orderRow.quantity - quantityNew));
                    //--- Set new quantity for current orderrow. ---
                    orderRow.quantity = quantityNew;

                }
                else
                {
                    message = "Orderrow could not be found !";
                    returnOk = false;
                }
            }

            return returnOk;

        }


        //===============================================================================================================
        // Check if specified Ordernumber, Rownumber and/or quantity is integer and greater than 0.
        // If any parameter empty it will not be checked. The parameters will be checked in the specified order.
        // If any fail it will return false.
        //===============================================================================================================
        private bool OrderParametersCheck(string orderNumber, string rowNumber, string quantity, out string message)
        {
            bool checkOk = true;
            int value=0;
            message = "";

            if (orderNumber != "" && !int.TryParse(orderNumber, out value) || value == 0)
            {
                message = " Ordernumber must be an integer value greater than 0 !";
                checkOk = false;
            }

            if (rowNumber != "" && !int.TryParse(rowNumber, out value) || value == 0)
            {
                message += " Rownumber must be an integer value greater than 0 !";
                checkOk = false;
            }

            if (quantity != "" && !int.TryParse(quantity, out value) || value == 0)
            {
                message += " Quantity must be an integer value greater than 0 !";
                checkOk = false;
            }

            return checkOk;

        }



        //==============================================================================================
        // Save all to file.
        //==============================================================================================
        public void SaveAllToFile()
        {
            SaveToFile(productsFile, typeof(Product), productsObj.products, "Create");
            SaveToFile(customersFile, typeof(Customer), customersObj.customers, "Create");
            SaveToFile(ordersFile, typeof(OrderHeader), ordersObj.orders.OfType<OrderHeader>().ToList(), "Create");
            SaveToFile(ordersFile, typeof(OrderRow), ordersObj.orders.OfType<OrderRow>().ToList(), "Append");
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
        private void SaveToFile(string file, Type type, dynamic list, string action)
        {

            StreamWriter fileWriter = null;

            //--- Create file if specified. ---
            if (action == "Create")
            {
                fileWriter = File.CreateText(file);
            }
            //--- Append file if specified. ---
            else if (action == "Append")
            {
                fileWriter = File.AppendText(file);
            }

            //--- Write line that specifies category. ---
            fileWriter.WriteLine("Category=" + type.Name);

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
                string category = "";

                while (!fileReader.EndOfStream)
                {
                    bool skipSwitch = false;
                    string[] parameters = null;
                    string line = fileReader.ReadLine();

                    //--- If line contains Category, set category. ---
                    if (line.ToLower().Contains("category="))
                    {
                        category = line.Split('=')[1].ToLower();
                        skipSwitch = true;
                    }
                    else
                    {
                        //--- Split read line into array of string parameters ---
                        parameters = line.Split(separator);
                    }

                    if (!skipSwitch)
                    {
                        //--- Create object for specified category and add to list. ---
                        switch (category)
                        {
                            case "product":
                                Product product = new Product();
                                AddToList(productsObj.products, product, parameters);
                                break;

                            case "customer":
                                Customer customer = new Customer();
                                AddToList(customersObj.customers, customer, parameters);
                                break;

                            case "orderheader":
                                OrderHeader orderheader = new BusinessSystem.OrderHeader();
                                AddToList(ordersObj.orders, orderheader, parameters);
                                break;

                            case "orderrow":
                                OrderRow orderrow = new OrderRow();
                                AddToList(ordersObj.orders, orderrow, parameters);
                                break;

                        }
                    }
                }

                fileReader.Close();

            }

        }


        //==============================================================================================
        // Add item (read from file) to appropriate list.
        //==============================================================================================
        private void AddToList(dynamic list, dynamic item, string[] parameters)
        {
            int index = 0;

            //--- Loop each property and set value. ---
            foreach (PropertyInfo property in item.GetType().GetProperties())
            {
                if (property.PropertyType.Name == "Int32")
                { property.SetValue(item, Convert.ToInt32(parameters[index++])); }

                else if (property.PropertyType.Name == "Decimal")
                { property.SetValue(item, Convert.ToDecimal(parameters[index++])); }

                else
                { property.SetValue(item, Convert.ToString(parameters[index++])); }
            }

            //--- Add to list. ---
            list.Add(item);
        }

    }
}
