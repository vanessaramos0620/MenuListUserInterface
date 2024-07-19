using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using MenuListModel;

namespace MenuListDataLayer
{
    public class MenuDataService
    {
        static string connectionString
            //= "Data Source=LAPTOP-2V85TBH6\\SQLEXPRESS01;Initial Catalog=VanessaRestaurant;Integrated Security=True;";
        ="Server=tcp:20.189.122.105,1433; Database=VanessaRestaurant; User Id=sa; Password=Ramos.bsit21;";
        static SqlConnection sqlConnection = new SqlConnection(connectionString);
        static public void Connect()
        {
            sqlConnection.Open();
        }

        public List<Menu> GetMenus()
        {
            List<Menu> menus = new List<Menu>();
            string query = "SELECT * FROM MenuList";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    menus.Add(new Menu
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Category = reader["Category"].ToString(),
                        Item = reader["Item"].ToString(),
                        Price = reader.GetDecimal(reader.GetOrdinal("Price"))
                    });
                }
            }

            return menus;
        }

        public Menu GetMenu(string order)
        {
            Menu menu = null;
            string query = "SELECT * FROM MenuList WHERE Item = @Item";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Item", order);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    menu = new Menu
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Category = reader["Category"].ToString(),
                        Item = reader["Item"].ToString(),
                        Price = reader.GetDecimal(reader.GetOrdinal("Price"))
                    };
                }
            }

            return menu;
        }
    }
}