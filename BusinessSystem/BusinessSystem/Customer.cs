using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessSystem
{
    public class Customer
    {
        private int _number;
        private string _name;
        private string _address;

        public int number { get { return _number; } set { _number = value; } }
        public string name { get { return _name; } }
        public string address { get { return _address; } }


        //--- Constructor ---
        public Customer(int number, string name, string address)
        {
            _number = number;
            _name = name;
            _address = address;
        }
    }


    //===========================================================================================
    // Generic Customers class.
    //===========================================================================================
    class Customers<T> : IEnumerable where T : Customer
    {
        public List<Customer> customers = new List<Customer>();


        //--- Enumerator. ---
        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < customers.Count; i++)
            {
                yield return customers[i];
            }
        }



        //===========================================================================================
        // Get highest used customernumber.
        //===========================================================================================
        private int GetCustomerHighestNumber()
        {
            //--- Get the highest customer number and return. Return 0 if first. ---
            if (customers.Count > 0)
            {
                return customers.Max(item => item.number);
            }
            else
            {
                return 0;
            }
        }


        //====================================================================================================================================
        // Insert Customer
        //====================================================================================================================================
        public void InsertCustomer(int customerNumber, string name, string address)
        {
            Customer customerNew = new Customer(customerNumber, name,address);
            customers.Add(customerNew);
        }


        //===========================================================================================
        // Add customer
        //===========================================================================================
        public int AddCustomer(string name, string address)
        {
            //--- Make sure the customer not already in the customerlist. ---
                if (GetCustomerByNameAddress(name, address).Length==0)
                {
                    //--- Get the highest customer number and set to new customer object. ---
                    int customerNumber = GetCustomerHighestNumber() + 1;

                //--- Add customer. ---
                InsertCustomer(customerNumber, name, address);
                return customerNumber;
            }
            else
            {
                return 0;
            }
        }


       //===========================================================================================
        // Get customer by number.
        //===========================================================================================
        public Customer[] GetCustomerByNumber(int number)
        {
            //--- Select customer from customers list that corresponds to the given number. ---
            return customers.Where(item => item.number == number).ToArray();
        }


        //===========================================================================================
        // Get customer by Name.
        //===========================================================================================
        public Customer[] GetCustomerByName(string name)
        {
            //--- Select customer from customers list that corresponds to the given name. ---
            return customers.Where(item => item.name.ToLower() == name.ToLower()).ToArray();
        }


        //===========================================================================================
        // Get customer by Address.
        //===========================================================================================
        public Customer[] GetCustomerByAddress(string address)
        {
            //--- Select customer from customers list that corresponds to the given address. ---
            return customers.Where(item => item.address.ToLower() == address.ToLower()).ToArray();

        }


        //===========================================================================================
        // Get customer by Name and Address.
        //===========================================================================================
        public Customer[] GetCustomerByNameAddress(string name,string address)
        {
            //--- Select customer from customers list that corresponds to the given address. ---
            return customers.Where(item => item.name.ToLower() == name.ToLower() && item.address.ToLower() == address.ToLower() ).ToArray();

        }

    }

}
