using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication800000.Data;

namespace WebApplication800000.Models
{
    public class UserModels
    {
        private MySqlConnection conn;
        private int id;
        private String name;
        private ArrayList data = new ArrayList();

        public List<int> intaddArruser = new List<int> { 1, 2 };
        private List<UserModels> userlist = new List<UserModels>();



        public void BuildUser(int amount)
        {
            for (int x = 0; x < amount; x++)
            {
                userlist.Add(new UserModels(intaddArruser[x]));
            }
        }

        public UserModels(int _id)
        {
            id = _id;
            {
                conn = Connection.Initialize();
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM customer_id WHERE id=@id;", conn);
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@id", (id));
                using (MySqlDataReader pro = cmd.ExecuteReader())
                {
                    while (pro.Read())
                    {
                        data.Add(pro[0].ToString());
                        data.Add(pro[1].ToString());

                    }
                }
                FillValues();
                conn.Close();
            }
        }
        private void FillValues()
        {
            id = Int32.Parse(data[0].ToString());
            name = data[1].ToString();

        }
        public int Id
        {
            get { return id; }
        }
        public String Name
        {
            get { return name; }
        }
    }


}