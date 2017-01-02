using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessSystem
{

    //===========================================================================================
    // Product Class.
    //===========================================================================================
    public class Product
    {
        private string _number;
        private string _name;
        private double _price;
        private int _quantity;

        public string number { get { return _number; } }
        public string name { get { return _name; } }
        public double price { get { return _price; } }
        public int quantity { get { return _quantity; } }

        //--- Constructor ---
        public Product(string number, string name, double price, int quantity)
        {
            _number = number;
            _name = name;
            _price = price;
            _quantity = quantity;
        }
    }


    //===========================================================================================
    // Generic Store class.
    //===========================================================================================
    public class Store<T> : IEnumerable where T : Product
    {
        public List<T> products = new List<T>();


        //--- Enumerator. ---
        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < products.Count; i++)
            {
                yield return products[i];
            }
        }


        //--- Add product. ---
        public bool AddProduct(T product)
        {
            //--- Make sure the artikel not already in the Store. ---
            if (GetProductByNumber(product.number) == null & GetProductBylName(product.name) == null)
            {
                products.Add(product);
                return true;
            }
            else
            {
                return false;
            }
        }


        //--- Get product by number. ---
        public Product GetProductByNumber(string number)
        {
            //--- Select products from store that corresponds to the given number (either zero or one product). ---
            Product[] productGet = products.Where(item => item.number.ToLower() == number.ToLower()).ToArray();

            if (productGet.Length > 0)
            {
                return productGet[0];
            }
            else
            {
                return null;
            }

        }


        //--- Get product by name. ---
        public Product GetProductBylName(string name)
        {
            //--- Select products from store that corresponds to the given name (either zero or one product). ---
            Product[] productGet = products.Where(item => item.name.ToLower() == name.ToLower()).ToArray();

            if (productGet.Length > 0)
            {
                return productGet[0];
            }
            else
            {
                return null;
            }
        }

    }

}
