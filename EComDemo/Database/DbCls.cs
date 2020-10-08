using EComDemo.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace EComDemo.Database
{
    public class DbCls
    {
        readonly SQLiteConnection database;

        public DbCls(string dbPath)
        {
            try
            {
                database = new SQLiteConnection(dbPath);
                database.CreateTable<FavoriteItem>();
            }
            catch (Exception ex)
            {

            }
        }

        public FavoriteItem GetProduct(int id)
        {
            return database.Table<FavoriteItem>().Where(x=>x.ProductId==id).FirstOrDefault();
        }

        public int SaveProduct(FavoriteItem item)
        {
            return database.Insert(item);
        }

        public int ClearProduct(int id)
        {
            var status = 0;
            try
            {
                var data = database.Table<FavoriteItem>().Where(x=>x.ProductId==id).ToList();
                foreach (var item in data)
                {
                    status = database.Delete(item);
                }

            }
            catch (Exception ex)
            {

            }
            return status;
        }

    }
}
