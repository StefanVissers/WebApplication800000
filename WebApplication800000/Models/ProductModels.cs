using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using System.Collections;
using WebApplication800000.Data;

namespace WebApplication800000.Models
{
    public class ProductModels
    {
        private MySqlConnection conn;
        private int product_id;
        private String name;
        private String catagory;
        private int price;
        private String manufactorer;
        private ArrayList data = new ArrayList();

        public List<int> intArr = new List<int> { 1, 2, 3, 4 };
        public List<int> intaddArr = new List<int> { 1, 2, 3, 4 };
        public List<int> intaddArrwish = new List<int> { 1, 2, 3, 4 };
        private List<ProductModels> products = new List<ProductModels>();
        private List<ProductModels> addedproducts = new List<ProductModels>();
        private List<ProductModels> wishlist = new List<ProductModels>();

        
        public void BuildProducts(int amount)
        {
            for (int x = 0; x < amount; x++)
            {
                products.Add(new ProductModels(intArr[x]));
            }
        }        
        public void AddedProducts(int amount)
        { 
            for (int x = 0; x < amount; x++)
            {
                addedproducts.Add(new ProductModels(intaddArr[x]));
            }
        }

        public void Wishlist(int amount)
        {
            for (int x = 0; x < amount; x++)
            {
                wishlist.Add(new ProductModels(intaddArrwish[x]));
            }
        }



        public ProductModels(int _id)
        {
            product_id = _id;
            {
                conn = Connection.Initialize();
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM products WHERE product_id=@product_id;", conn);
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@product_id", (product_id));
                using (MySqlDataReader pro = cmd.ExecuteReader())
                {
                    while (pro.Read())
                    {
                        data.Add(pro[0].ToString());
                        data.Add(pro[1].ToString());
                        data.Add(pro[2].ToString());
                        data.Add(pro[3].ToString());
                        data.Add(pro[4].ToString());

                    }
                }
                FillValues();
                conn.Close();
            }
        }
        private void FillValues()
        {
            product_id = Int32.Parse(data[0].ToString());
            name = data[1].ToString();
            catagory = data[2].ToString();
            price = Int32.Parse(data[3].ToString());
            manufactorer = data[4].ToString();

        }
        public int products_id
        {
            get { return product_id; }
        }
        public String Name
        {
            get { return name; }
        }
        public String Catagory
        {
            get { return catagory; }
        }
        public int Price
        {
            get { return price; }
        }
        public String Manufactorer
        {
            get { return manufactorer; }
        }
    }
}