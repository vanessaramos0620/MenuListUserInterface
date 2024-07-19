using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using MenuListBusinessLogic;
using MenuListDataLayer;
using MenuListModel;

namespace MenuListUserInterface
{
    class Program
    {
        static void Main(string[] args)
        {
          
            Console.WriteLine("░W░E░L░C░O░M░E░ ░t░o░ ░V░A░N░E░S░S░A░'░S░ ░R░E░S░T░A░U░R░A░N░T░");
            
            string input;
            do
            {
                Console.WriteLine("\nType 'order' to see the menu:");
                input = Console.ReadLine();
                
            } while (!input.Equals("order", StringComparison.OrdinalIgnoreCase));
            MenuDataService.Connect();

            MenuService menuService = new MenuService(new MenuDataService());
            List<Menu> menus = menuService.GetAllMenus();

            DisplayMenu(menus);
            List<string> orders = GetOrders(menus);

            if (orders.Count > 0)
            {
                ManageOrders(orders, menus);
                DisplayFinalOrder(orders);
                Console.WriteLine("\nThank you for ordering!");
            }
            
        }
        
        static void DisplayMenu(List<Menu> menus)
        {
            Console.WriteLine("\nVANESSA'S RESTAURANT MENU");
            var groupedMenus = menus.GroupBy(m => m.Category);
            foreach (var group in groupedMenus)
            {
                Console.WriteLine($"\n{group.Key}");
                Console.WriteLine("---------------------------");
                foreach (var menu in group)
                {
                    int spacing = 30 - menu.Item.Length;
                    Console.WriteLine($"{menu.Item}{new string(' ', spacing)}{menu.Price:C2}"); // Format price as currency
                }
            }
        }

        static List<string> GetOrders(List<Menu> menus)
        {
            List<string> orders = new List<string>();
            Console.WriteLine("\nPlease enter your orders (type 'done' to finish):");
            string input;

            do
            {
                input = Console.ReadLine();
                if (!input.Equals("done", StringComparison.OrdinalIgnoreCase))
                {
                    if (menus.Any(m => m.Item.Equals(input, StringComparison.OrdinalIgnoreCase)))
                    {
                        orders.Add(input);
                    }
                    else
                    {
                        Console.WriteLine("Item not in the menu. Please try again.");
                    }
                }
            } while (!input.Equals("done", StringComparison.OrdinalIgnoreCase));

            return orders;
        }

        static void ManageOrders(List<string> orders, List<Menu> menus)
        {
            string input;
            Console.WriteLine("\nYour Current Order:");
            foreach (var order in orders)
            {
                Console.WriteLine(order);
            }

            Console.WriteLine("\nWould you like to add more items to your order? (yes/no)");
            if (Console.ReadLine().Equals("yes", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Please enter additional orders (type 'done' to finish):");
                do
                {
                    input = Console.ReadLine();
                    if (!input.Equals("done", StringComparison.OrdinalIgnoreCase))
                    {
                        if (menus.Any(m => m.Item.Equals(input, StringComparison.OrdinalIgnoreCase)))
                        {
                            orders.Add(input);
                        }
                        else
                        {
                            Console.WriteLine("Item not in the menu. Please try again.");
                        }
                    }
                } while (!input.Equals("done", StringComparison.OrdinalIgnoreCase));

                Console.WriteLine("\nYour Updated Order:");
                foreach (var order in orders)
                {
                    Console.WriteLine(order);
                }
            }

            Console.WriteLine("\nWould you like to remove any items from your order? (yes/no)");
            if (Console.ReadLine().Equals("yes", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Enter the items you want to remove (type 'done' when finished):");
                do
                {
                    input = Console.ReadLine();
                    if (!input.Equals("done", StringComparison.OrdinalIgnoreCase))
                    {
                        if (orders.Contains(input))
                        {
                            orders.Remove(input);
                        }
                        else
                        {
                            Console.WriteLine("Item not found in the order. Please try again.");
                        }
                    }
                } while (!input.Equals("done", StringComparison.OrdinalIgnoreCase));

            }
        }

        static void DisplayFinalOrder(List<string> orders)
        {
            Console.WriteLine("\nYour Final Order:");
            foreach (var order in orders)
            {
                Console.WriteLine(order);
            }
        }
    }
}

