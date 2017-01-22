using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Net;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using BusinessSystem;

namespace BusinessSystem
{
    //--- Base Class Order ---
    public class Order
    {
        private int _orderNumber;

        public int orderNumber { get { return _orderNumber; } set { _orderNumber = value; } }

        public Order() { }

        public Order(int orderNumber)
        {
            _orderNumber = orderNumber;
        }
    }

    //--- Class Orderheader, Customer ---
    public class OrderHeader : Order
    {
        private int _customerNumber;

        public int customerNumber { get { return _customerNumber; } set { _customerNumber = value; } }

        //--- Constructors ---
        public OrderHeader() { }

        public OrderHeader(int orderNumber, int customerNumber) : base(orderNumber)
        {
            _customerNumber = customerNumber;
        }

    }


    //--- Class Orderrow, Product ---
    public class OrderRow : Order
    {
        private int _rowNumber;
        private string _productNumber;
        private string _productName;
        private decimal _productPrice;
        private int _quantity;

        public int rowNumber { get { return _rowNumber; } set { _rowNumber = value; } }
        public string productNumber { get { return _productNumber; } set { _productNumber = value; } }
        public string productName { get { return _productName; } set { _productName = value; } }
        public decimal productPrice { get { return _productPrice; } set { _productPrice = value; } }
        public int quantity { get { return _quantity; } set { _quantity = value; } }


        //--- Constructors ---
        public OrderRow() { }

        public OrderRow(int orderNumber, int rowNumber, string productNumber, string productName, decimal productPrice, int quantity) : base(orderNumber)
        {
            _rowNumber = rowNumber;
            _productNumber = productNumber;
            _productName = productName;
            _productPrice = productPrice;
            _quantity = quantity;
        }
    }


    //===========================================================================================
    // Generic Orders class.
    //===========================================================================================
    class Orders<T> : IEnumerable where T : Order
    {
        public List<T> orders = new List<T>();


        //--- Enumerator. ---
        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < orders.Count; i++)
            {

                yield return orders[i];
            }
        }


        //==================================================================================================================
        // Get highest used Ordernumber.
        //==================================================================================================================
        public int GetHighestOrderNumber()
        {
            //--- Get the highest order number and return. Return 0 if first. ---
            if (orders.Count > 0)
            {
                return orders.Max(item => item.orderNumber);
            }
            else
            {
                return 0;
            }
        }


        //==================================================================================================================
        // Get highest used Rownumber in the specified order.
        //==================================================================================================================
        public int GetHighestRowNumberByOrderNumber(int orderNumber)
        {
            //--- Get the highest rownumber for specified order. ---
            List<OrderRow> orderrows = orders.OfType<OrderRow>().Where(item => item.orderNumber == orderNumber).ToList();
            if (orderrows.Count > 0)
                return orderrows.Max(item => item.rowNumber);
            else
                return 0;
        }


        //==================================================================================================================
        // Get Customernumber from the specified ordernumber.
        //==================================================================================================================
        public int GetCustomerNumberByOrderNumber(int orderNumber)
        {
            //--- Get the customer for specified ordernumber. ---
            return orders.OfType<OrderHeader>().Where(item => item.orderNumber == orderNumber).ToArray()[0].customerNumber;
        }


        //==================================================================================================================
        // Get all orderheaders.
        //==================================================================================================================
        public List<OrderHeader> GetAllOrderHeaders()
        {
            //--- Get all orderheaders. ---
            return orders.OfType<OrderHeader>().ToList();
        }


        //==================================================================================================================
        // Get orderheader for specific ordernumber.
        //==================================================================================================================
        public OrderHeader GetOrderHeaderByOrderNumber(int orderNumber)
        {
            //--- Get orderheader for specific ordernumber. ---
            List<OrderHeader> orderHeaders = orders.OfType<OrderHeader>().Where(item => item.orderNumber == orderNumber).ToList();
            if (orderHeaders.Count == 1)
                return orderHeaders[0];
            else
                return null;
        }


        //==================================================================================================================
        // Get orderheaders for specific customernumber.
        //==================================================================================================================
        public List<OrderHeader> GetOrderHeadersByCustomerNumber(int customerNumber)
        {
            //--- Get orderheader for specific customernumber. ---
            return orders.OfType<OrderHeader>().Where(item => item.customerNumber == customerNumber).ToList();
        }


        //==================================================================================================================
        // Get all orderrows for specific ordernumber.
        //==================================================================================================================
        public List<OrderRow> GetOrderRowsByOrderNumber(int orderNumber)
        {
            //--- Return all orderrows. ---
            return orders.OfType<OrderRow>().Where(item => item.orderNumber == orderNumber).ToList();

        }


        //==================================================================================================================
        // Get specific orderrow for specific ordernumber.
        //==================================================================================================================
        public OrderRow GetOrderRowByOrderNumber(int orderNumber, int rowNumber)
        {
            //--- Return specific orderrow. ---
            List<OrderRow> orderRows = orders.OfType<OrderRow>().Where(item => item.orderNumber == orderNumber && item.rowNumber == rowNumber).ToList();
            if (orderRows.Count == 1)
                return orderRows[0];
            else
                return null;

        }


        //==================================================================================================================
        // Delete orderrow.
        //==================================================================================================================
        public bool DeleteOrderRow(OrderRow orderRow)
        {
            return false;
        }


        //==================================================================================================================
        // Delete orderrow for specific ordernumber.
        //==================================================================================================================
        public bool DeleteOrderRow(int orderNumber, int rowNumber)
        {
            //--- If row exists, delete. ---
            if (orders.OfType<OrderRow>().Where(item => item.orderNumber == orderNumber && item.rowNumber == rowNumber).Count() > 0)
            {
                orders.OfType<OrderRow>().Where(item => item.orderNumber == orderNumber && item.rowNumber == rowNumber);
                return true;
            }
            else
                return false;

        }

    }

}
