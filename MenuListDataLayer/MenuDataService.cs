using System.Collections.Generic;
using System.Data.SqlClient;
using MenuListModel;

namespace MenuListDataLayer
{
    public class MenuDataService
    {
        private readonly string _connectionString;

        public MenuDataService()
        {
            _connectionString = "Server=tcp:20.189.122.105,1433; Database=VanessaRestaurant; User Id=sa; Password=Ramos.bsit21;";
        }

        public List<Menu> GetAllMenus()
        {
            List<Menu> menus = new List<Menu>();
            string query = "SELECT Id, Category, Item, Price FROM Menus";

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(query, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var menu = new Menu
                        {
                            Id = reader.GetInt32(0),
                            Category = reader.GetString(1),
                            Item = reader.GetString(2),
                            Price = reader.GetDecimal(3)
                        };
                        menus.Add(menu);
                    }
                }
            }

            return menus;
        }

        public Menu GetMenu(string item)
        {
            Menu menu = null;
            string query = "SELECT Id, Category, Item, Price FROM Menus WHERE Item = @Item";

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Item", item);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        menu = new Menu
                        {
                            Id = reader.GetInt32(0),
                            Category = reader.GetString(1),
                            Item = reader.GetString(2),
                            Price = reader.GetDecimal(3)
                        };
                    }
                }
            }

            return menu;
        }

        public void AddMenu(Menu menu)
        {
            string query = "INSERT INTO Menus (Category, Item, Price) VALUES (@Category, @Item, @Price)";

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Category", menu.Category);
                command.Parameters.AddWithValue("@Item", menu.Item);
                command.Parameters.AddWithValue("@Price", menu.Price);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public bool UpdateMenu(string item, Menu updatedMenu)
        {
            string query = "UPDATE Menus SET Category = @Category, Item = @Item, Price = @Price WHERE Item = @OriginalItem";

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Category", updatedMenu.Category);
                command.Parameters.AddWithValue("@Item", updatedMenu.Item);
                command.Parameters.AddWithValue("@Price", updatedMenu.Price);
                command.Parameters.AddWithValue("@OriginalItem", item);
                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }

        public bool DeleteMenu(string item)
        {
            string query = "DELETE FROM Menus WHERE Item = @Item";

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Item", item);
                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }
    }
}
