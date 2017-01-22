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
        public string name { get { return _name; } set { _name = value; } }
        public string address { get { return _address; } set { _address = value; } }


        //--- Constructors ---
        public Customer() { }

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



        //===========================================================================================
        // Add customer
        //===========================================================================================
        public int AddCustomer(string name, string address)
        {
            //--- Make sure the customer not already in the customerlist. ---
            if (GetCustomerByNameAddress(name, address).Count == 0)
            {
                //--- Get the highest customer number and set to new customer object. ---
                int customerNumber = GetCustomerHighestNumber() + 1;

                //--- Add customer. ---
                Customer customerNew = new Customer(customerNumber, name, address);
                customers.Add(customerNew);
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
        public List<Customer> GetAllCustomer()
        {
            return customers;
        }


        //===========================================================================================
        // Get customer by number.
        //===========================================================================================
        public Customer GetCustomerByNumber(int number)
        {
            //--- Select customer from customers list that corresponds to the given number. ---
            List<Customer> customersList = customers.Where(item => item.number == number).ToList();
            if (customersList.Count == 1)
                return customersList[0];
            else
                return null;
        }


        //===========================================================================================
        // Get customer by Name.
        //===========================================================================================
        public List<Customer> GetCustomerByName(string name)
        {
            //--- Select customer from customers list that corresponds to the given name. ---
            return customers.Where(item => item.name.ToLower() == name.ToLower()).ToList();
        }


        //===========================================================================================
        // Get customer by Address.
        //===========================================================================================
        public List<Customer> GetCustomerByAddress(string address)
        {
            //--- Select customer from customers list that corresponds to the given address. ---
            return customers.Where(item => item.address.ToLower() == address.ToLower()).ToList();

        }


        //===========================================================================================
        // Get customer by Name and Address.
        //===========================================================================================
        public List<Customer> GetCustomerByNameAddress(string name, string address)
        {
            //--- Select customer from customers list that corresponds to the given address. ---
            return customers.Where(item => item.name.ToLower() == name.ToLower() && item.address.ToLower() == address.ToLower()).ToList();

        }

    }

}
