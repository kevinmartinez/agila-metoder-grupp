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

/*
namespace BusinessSystem
{
    public class OrderRow
    {
        private int _orderNumber;
        private int _customerNumber;
        private int _rowNumber;
        private string _productNumber;
        private string _productName;
        private decimal _productPrice;
        private int _quantity;

        public int orderNumber { get { return _orderNumber; } }
        public int customerNumber { get { return _customerNumber; } }
        public int rowNumber { get { return _rowNumber; } }
        public string productNumber { get { return _productNumber; } }
        public string productName { get { return _productName; } }
        public decimal productPrice { get { return _productPrice; } }
        public int quantity { get { return _quantity; } set { _quantity = value; } }


        //--- Constructor ---
        public OrderRow(int orderNumber, int customerNumber, int rowNumber, string productNumber, string productName, decimal productPrice, int quantity)
        {
            _orderNumber = orderNumber;
            _customerNumber = customerNumber;
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
    class OrderObj<T> where T : OrderRow
    {
        public List<OrderRow> orderRows = new List<OrderRow>();


        //--- Enumerator. ---
        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < orderRows.Count; i++)
            {

                yield return orderRows[i];
            }
        }


        //==================================================================================================================
        // Get highest used Ordernumber.
        //==================================================================================================================
        private int GetHighestOrderNumber()
        {
            //--- Get the highest order number and return. Return 0 if first. ---
            if (orderRows.Count > 0)
            {
                return orderRows.Max(item => item.orderNumber);
            }
            else
            {
                return 0;
            }
        }


        //==================================================================================================================
        // Get highest used Rownumber in the specified order.
        //==================================================================================================================
        private int GetHighestRowNumber(int orderNumber)
        {
            //--- Get the highest rownumber for specified order and return. ---
            return orderRows.Where(item => item.orderNumber == orderNumber).Max(item => item.rowNumber);
        }


        //==================================================================================================================
        // Get Customernumber from the specified ordernumber.
        //==================================================================================================================
        private int GetCustomerNumberByOrderNumber(int orderNumber)
        {
            //--- Get the highest rownumber for specified order and return. ---
            List<OrderRow> orderRow = orderRows.Where(item => item.orderNumber == orderNumber && item.rowNumber == 0).ToList();

            if (orderRow.Count > 0)
            {
                return orderRow[0].customerNumber;
            }
            else
            {
                return 0;
            }

        }


        //====================================================================================================================================
        // Insert Orderrow.
        //====================================================================================================================================
        public void InsertOrderRow(int orderNumber, int customerNumber, int rowNumber, string productNumber, string productName
                                , decimal productPrice, int quantity)
        {
            OrderRow orderRow = new OrderRow(orderNumber, customerNumber, rowNumber, productNumber, productName, productPrice, quantity);
            orderRows.Add(orderRow);
        }


        //==================================================================================================================
        // Add Order.
        // Order is an orderrow with rownumber equal to 0. Rows from 1 and upwards contains the products in the order.
        // Ordernumber is returned.
        //==================================================================================================================
        public int AddOrder(int customerNumber)
        {
            //--- Get next order number. ---
            int orderNumber = GetHighestOrderNumber() + 1;

            //--- Add orderrow to orderlist. ---
            InsertOrderRow(orderNumber, customerNumber, 0, "", "", 0, 0);

            return orderNumber;
        }


        //==================================================================================================================
        // Add Orderrow.
        //==================================================================================================================
        public int AddOrderRow(int orderNumber, string productNumber, string productName, decimal productPrice, int quantity)
        {
            //--- Get customer number for the current order. ---
            int customerNumber = GetCustomerNumberByOrderNumber(orderNumber);

            //--- Get next rownumber for the current order. ---
            int rowNumber = GetHighestRowNumber(orderNumber) + 1;

            //--- Add orderrow to orderlist. ---
            InsertOrderRow(orderNumber, customerNumber, rowNumber, productNumber, productName, productPrice, quantity);

            return rowNumber;

        }

    }


}
*/
