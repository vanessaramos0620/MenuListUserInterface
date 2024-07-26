using System;
using System.Collections.Generic;
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

            MenuDataService menuDataService = new MenuDataService();
            MenuService menuService = new MenuService(menuDataService);

            string input;
            do
            {
                Console.WriteLine("\nType 'order' to see the menu or 'admin' to manage the menu:");
                input = Console.ReadLine();

                if (input.Equals("order", StringComparison.OrdinalIgnoreCase))
                {
                    OrderMenu(menuService);
                }
                else if (input.Equals("admin", StringComparison.OrdinalIgnoreCase))
                {
                    ManageMenu(menuService);
                }
                else
                {
                    Console.WriteLine("Invalid input. Please type 'order' or 'admin'.");
                }

            } while (!input.Equals("exit", StringComparison.OrdinalIgnoreCase));
        }

        static void OrderMenu(MenuService menuService)
        {
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

        static void ManageMenu(MenuService menuService)
        {
            string input;

            do
            {
                Console.WriteLine("\nMenu Management");
                Console.WriteLine("1. View Menus");
                Console.WriteLine("2. Add Menus");
                Console.WriteLine("3. Update Menus");
                Console.WriteLine("4. Delete Menus");
                Console.WriteLine("5. Exit");
                Console.Write("Select an option: ");
                input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        ViewMenu(menuService);
                        break;

                    case "2":
                        AddMenu(menuService);
                        break;

                    case "3":
                        UpdateMenu(menuService);
                        break;

                    case "4":
                        DeleteMenu(menuService);
                        break;

                    case "5":
                        Console.WriteLine("Exiting menu management.");
                        break;

                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }

            } while (input != "5");
        }

        static void ViewMenu(MenuService menuService)
        {
            List<Menu> menus = menuService.GetAllMenus();
            DisplayMenu(menus);
        }

        static void AddMenu(MenuService menuService)
        {
            while (true)
            {
                Console.WriteLine("Enter the category of the new menu item (or type 'done' to finish):");
                string category = Console.ReadLine();
                if (category.Equals("done", StringComparison.OrdinalIgnoreCase)) break;

                Console.WriteLine("Enter the item name:");
                string item = Console.ReadLine();

                Console.WriteLine("Enter the price:");
                if (!decimal.TryParse(Console.ReadLine(), out decimal price))
                {
                    Console.WriteLine("Invalid price. Please enter a valid number.");
                    continue;
                }

                var menu = new Menu
                {
                    Category = category,
                    Item = item,
                    Price = price
                };

                menuService.AddMenu(menu);
                Console.WriteLine("Menu item added successfully.");
            }
        }

        static void UpdateMenu(MenuService menuService)
        {
            while (true)
            {
                Console.WriteLine("Enter the item name of the menu item to update (or type 'done' to finish):");
                string item = Console.ReadLine();
                if (item.Equals("done", StringComparison.OrdinalIgnoreCase)) break;

                Menu existingMenu = menuService.GetMenu(item);

                if (existingMenu != null)
                {
                    Console.WriteLine("Enter the new category:");
                    string category = Console.ReadLine();

                    Console.WriteLine("Enter the new item name:");
                    string newItem = Console.ReadLine();

                    Console.WriteLine("Enter the new price:");
                    if (!decimal.TryParse(Console.ReadLine(), out decimal price))
                    {
                        Console.WriteLine("Invalid price. Please enter a valid number.");
                        continue;
                    }

                    var updatedMenu = new Menu
                    {
                        Category = category,
                        Item = newItem,
                        Price = price
                    };

                    if (menuService.UpdateMenu(item, updatedMenu))
                    {
                        Console.WriteLine("Menu item updated successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Failed to update menu item.");
                    }
                }
                else
                {
                    Console.WriteLine("Menu item not found.");
                }
            }
        }

        static void DeleteMenu(MenuService menuService)
        {
            while (true)
            {
                Console.WriteLine("Enter the item name of the menu item to delete (or type 'done' to finish):");
                string item = Console.ReadLine();
                if (item.Equals("done", StringComparison.OrdinalIgnoreCase)) break;

                if (menuService.DeleteMenu(item))
                {
                    Console.WriteLine("Menu item deleted successfully.");
                }
                else
                {
                    Console.WriteLine("Failed to delete menu item.");
                }
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
                    Console.WriteLine($"{menu.Item}{new string(' ', spacing)}{menu.Price:C2}"); 
                }
            }
        }

        static List<string> GetOrders(List<Menu> menus)
        {
            List<string> orders = new List<string>();
            Console.WriteLine("\nEnter the items you want to order (type 'done' to finish):");
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
                        Console.WriteLine("Item not found in menu. Please try again.");
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
                            Console.WriteLine("Item not found in menu. Please try again.");
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
                            Console.WriteLine("Item not found in your order. Please try again.");
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
