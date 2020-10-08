using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace EComDemo.Models
{
    
    public class FavoriteItem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int ProductId { get; set; }
         
    }
}
