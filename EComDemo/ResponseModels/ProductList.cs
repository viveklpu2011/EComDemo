using System;
using System.Collections.Generic;
using System.Text;

namespace EComDemo.ResponseModels
{
    
    public class ProductData
    {
        public int id { get; set; }
        public string name { get; set; }
        public string title { get; set; }
        public string image { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }
        public int ratecount { get; set; }
        public string category { get; set; }

    }
    public class ProductList
    {
        public bool status { get; set; }
        public string message { get; set; }
        public int totalProduct { get; set; }
        public string productName { get; set; }
        public IList<ProductData> data { get; set; }

    }
}
