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
        private decimal _price;
        private int _quantity;

        public string number { get { return _number; } }
        public string name { get { return _name; } }
        public decimal price { get { return _price; } set { _price = value; } }
        public int quantity { get { return _quantity; } set { _quantity = value; } }

        //--- Constructor ---
        public Product(string number, string name, decimal price, int quantity)
        {
            _number = number;
            _name = name;
            _price = price;
            _quantity = quantity;
        }

    }


    //===========================================================================================
    // Generic Products class.
    //===========================================================================================
    public class Products<T> : IEnumerable where T : Product
    {
        public List<Product> products = new List<Product>();


        //--- Enumerator. ---
        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < products.Count; i++)
            {
                yield return products[i];
            }
        }


        //===========================================================================================
        // Get product by number.
        //===========================================================================================
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


        //===========================================================================================
        // Get product by name.
        //===========================================================================================
        public Product GetProductByName(string name)
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


        //===========================================================================================
        // Add product.
        //===========================================================================================
        public bool AddProduct(string productNumber, string productName, decimal price, int quantity)
        {
            //--- Make sure the artikel not already in the Store. ---
            if (GetProductByNumber(productNumber) == null & GetProductByName(productName) == null)
            {
                Product product = new Product(productNumber, productName, price, quantity);
                products.Add(product);
                return true;
            }
            else
            {
                return false;
            }
        }


        //===========================================================================================
        // Change product price.
        //===========================================================================================
        public bool ChangeProductPrice(string productNumber, decimal productPrice)
        {
            //--- Get product. Change price if exists. ---
            Product product = GetProductByNumber(productNumber);

            if(product!=null)
            {
                product.price = productPrice;
                return true;
            }
            else
            {
                return false;
            }

        }


        //===========================================================================================
        // Change product quantity.
        //===========================================================================================
        public bool ChangeProductQuantity(string productNumber, int productQuantity)
        {
            //--- Get product. Change price if exists. ---
            Product product = GetProductByNumber(productNumber);

            if (product != null)
            {
                product.quantity = productQuantity;
                return true;
            }
            else
            {
                return false;
            }

        }

    }
}