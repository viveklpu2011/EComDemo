using System;
using System.Collections.Generic;
using System.Text;

namespace EComDemo.ResponseModels
{
    public class UBProductList
    {
        public UBProductList()
        {
            Data = new List<UBProduct>();
        }
        public bool Status { get; set; }
        public string Message { get; set; }
        public List<UBProduct> Data { get; set; }
    }
    public class UBProduct
    {
        public int Id { get; set; }
        public string PName { get; set; }
        public string PImg1 { get; set; }
        public string PImg2 { get; set; }
        public string PImg3 { get; set; }
        public string PImg4 { get; set; }
        public int TotalProduct { get; set; }

        public bool NewCollection { get; set; }
        public bool ProductCollection { get; set; }
    }
}
