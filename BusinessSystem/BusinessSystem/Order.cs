﻿using System;
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

        public int orderNumber { get { return _orderNumber; } }

        public Order(int orderNumber)
        {
            _orderNumber = orderNumber;
        }
    }

    //--- Class Orderheader, Customer added ---
    public class OrderHeader : Order
    {
        private int _customerNumber;
        private string _customerName;

        public int customerNumber { get { return _customerNumber; } }
        public string customerame { get { return _customerName; } }

        public OrderHeader(int orderNumber, int customerNumber, string customerName) : base(orderNumber)
        {
            _customerNumber = customerNumber;
            _customerName = customerName;
        }

    }


    //--- Class Orderrow, Product added ---
    public class OrderRow : Order
    {
        private int _rowNumber;
        private string _productNumber;
        private string _productName;
        private decimal _productPrice;
        private int _quantity;

        public int rowNumber { get { return _rowNumber; } set { _rowNumber = value; } }
        public string productNumber { get { return _productNumber; } }
        public string productName { get { return _productName; } }
        public decimal productPrice { get { return _productPrice; } }
        public int quantity { get { return _quantity; } set { _quantity = value; } }


        //--- Constructor ---
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
        private int GetHighestRowNumber(int orderNumber)
        {
            //--- Get the highest rownumber for specified order. ---
            return orders.OfType<OrderRow>().Where(item => item.orderNumber == orderNumber).Max(item => item.rowNumber);
        }


        //==================================================================================================================
        // Get Customernumber from the specified ordernumber.
        //==================================================================================================================
        private int GetCustomerNumberByOrderNumber(int orderNumber)
        {
            //--- Get the highest rownumber for specified order. ---
            return orders.OfType<OrderHeader>().Where(item => item.orderNumber == orderNumber).ToArray()[0].customerNumber;
        }


        //====================================================================================================================================
        // Insert Order.
        //====================================================================================================================================
        //public void InsertOrderHeader(int orderNumber, int customerNumber, string customerName)
        public void InsertOrderHeader(T orderHeader)
        {
            //OrderHeader orderHeader = new OrderHeader(orderNumber, customerNumber, customerName);
            this.orders.Add(orderHeader);
        }


        //==================================================================================================================
        // Add Order.
        // Order is an orderrow with rownumber equal to 0. Rows from 1 and upwards contains the products in the order.
        // Ordernumber is returned.
        //==================================================================================================================
        /*        public int AddOrderHeader(T orderHeader)
                { 
                    //--- Get next order number. ---
                    int orderNumber = GetHighestOrderNumber() + 1;

                    //--- Add orderheader to orderlist. ---
                    //InsertOrderHeader(orderNumber, customerNumber, customerName);
                    //T orderHeader = new OrderHeader(orderNumber, customerNumber, customerName);


                    return orderNumber;
                }

        */
        //==================================================================================================================
        // Add Orderrow.
        //==================================================================================================================
        /*        public int AddOrderRow(int orderNumber, string productNumber, string productName, decimal productPrice, int quantity)
                {
                    //--- Get customer number for the current order. ---
                    int customerNumber = GetCustomerNumberByOrderNumber(orderNumber);

                    //--- Get next rownumber for the current order. ---
                    int rowNumber = GetHighestRowNumber(orderNumber) + 1;

                    //--- Add orderrow to orderlist. ---
                    InsertOrderRow(orderNumber, customerNumber, rowNumber, productNumber, productName, productPrice, quantity);

                    return rowNumber;

                }
        */
    }


}
