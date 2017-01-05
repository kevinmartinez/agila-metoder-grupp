using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessSystem
{
    class Customer
    {
        private int _number;
        private string _name;
        private string _address;

        public int number { get { return _number; } set { _number = value; } }
        public string name { get { return _name; } }
        public string address { get { return _address; } }


        //--- Constructor ---
        public Customer(string name, string address)
        {
            _name = name;
            _address = address;
        }
    }


    //===========================================================================================
    // Generic Customers class.
    //===========================================================================================
    class Customers<T> : IEnumerable where T : Customer
    {
        public List<T> customers = new List<T>();


        //--- Enumerator. ---
        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < customers.Count; i++)
            {
                yield return customers[i];
            }
        }


        //--- Add customer. ---
        public bool AddCustomer(T customer)
        {
            //--- Make sure the customer not already in the customerlist. ---
            if (GetCustomerByName(customer.name) == null & GetCustomerByAddress(customer.address) == null)
            {
                //- Get the highest customer number -
                customer.number = GetCustomerHighestNumber() + 1;
                customers.Add(customer);
                return true;
            }
            else
            {
                return false;
            }
        }


        //--- Get customer by number. ---
        private int GetCustomerHighestNumber()
        {
            //--- Get the highest customer number and return. ---
            if (customers.Count > 0)
            {
                return customers.Max(item => item.number);
            }
            else
            {
                return 0;
            }
        }


        //--- Get customer by name. ---
        public Customer GetCustomerByName(string name)
        {
            //--- Select products from store that corresponds to the given name (either zero or one product). ---
            Customer[] customerGet = customers.Where(item => item.name.ToLower() == name.ToLower()).ToArray();

            if (customerGet.Length > 0)
            {
                return customerGet[0];
            }
            else
            {
                return null;
            }
        }


        //--- Get customer by address. ---
        public Customer GetCustomerByAddress(string address)
        {
            //--- Select products from store that corresponds to the given name (either zero or one product). ---
            Customer[] customerGet = customers.Where(item => item.address.ToLower() == address.ToLower()).ToArray();

            if (customerGet.Length > 0)
            {
                return customerGet[0];
            }
            else
            {
                return null;
            }
        }

    }

}
