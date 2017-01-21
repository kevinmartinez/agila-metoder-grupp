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

        public string number { get { return _number; } set { _number = value; } }
        public string name { get { return _name; } set { _name = value; } }
        public decimal price { get { return _price; } set { _price = value; } }
        public int quantity { get { return _quantity; } set { _quantity = value; } }

        //--- Constructors ---
        public Product() { }

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
        // Get all products.
        //===========================================================================================
        public List<Product> GetAllProducts()
        {
            return products;
        }


        //===========================================================================================
        // Get product by number.
        //===========================================================================================
        public Product GetProductByNumber(string number)
        {
            //--- Select products from store that corresponds to the given number. ---
            List<Product> productsList = products.Where(item => item.number.ToLower() == number.ToLower()).ToList();
            if (productsList.Count == 1)
                return productsList[0];
            else
                return null;

        }


        //===========================================================================================
        // Get product by name.
        //===========================================================================================
        public List<Product> GetProductsByName(string name)
        {
            //--- Select products from store that corresponds to the given name. ---
            return products.Where(item => item.name.ToLower() == name.ToLower()).ToList();

        }


        //===========================================================================================
        // Add product.
        //===========================================================================================
        public bool AddProduct(string productNumber, string productName, decimal price, int quantity)
        {
            //--- Make sure the artikel not already in the Store. ---
            if (GetProductByNumber(productNumber) == null & GetProductsByName(productName).Count == 0)
            {
                Product product = new Product(productNumber, productName, price, quantity);
                products.Add(product);
                return true;
            }
            else
                return false;
        }


        //===========================================================================================
        // Change product price.
        //===========================================================================================
        public bool ChangeProductPrice(string productNumber, decimal productPrice)
        {
            //--- Get product. Change price if exists. ---
            Product product = GetProductByNumber(productNumber);

            if (product != null)
            {
                product.price = productPrice;
                return true;
            }
            else
                return false;

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
                return false;

        }


        //===========================================================================================
        // Add or delete from product quantity.
        //===========================================================================================
        public bool AddProductQuantity(string productNumber, int productQuantityToAdd)
        {
            //--- Get product. Change price if exists. ---
            Product product = GetProductByNumber(productNumber);

            if (product != null)
            {
                product.quantity += productQuantityToAdd;
                return true;
            }
            else
                return false;

        }

    }
}