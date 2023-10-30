using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Tortik
{
    public class Order
    {
        private Dictionary<string, string> orderDetails;
        private static Dictionary<string, Dictionary<string, double>> prices;

        public Order()
        {
            orderDetails = new Dictionary<string, string>
            {
                { "форма", "" },
                { "размер", "" },
                { "вкус", "" },
                { "глазурь", "" },
                { "декор", "" }
            };
        }

        public void StartOrder()
        {
            bool continueOrdering = true;
            while (continueOrdering)
            {
                Console.Clear();
                Console.WriteLine("Меню заказа тортов:");
                Console.WriteLine("Выберите опцию и нажмите Enter:");

                string selectedOption = Menu.ShowMenuAndGetSelection(orderDetails.Keys);
                if (selectedOption == "Выход")
                {
                    continueOrdering = false;
                    continue;
                }

                orderDetails[selectedOption] = Menu.ShowSubMenuAndGetSelection(prices[selectedOption]);
            }

            double totalCost = CalculateTotalCost();
            SaveOrderToHistory(totalCost);

            Console.WriteLine("Заказ сохранен в истории заказов.");
            Console.WriteLine($"Итоговая стоимость заказа: {totalCost} рублей");
        }

        private double CalculateTotalCost()
        {
            double totalCost = 0;
            foreach (var item in orderDetails)
            {
                totalCost += prices[item.Key][item.Value];
            }
            return totalCost;
        }

        private void SaveOrderToHistory(double totalCost)
        {
            using (StreamWriter writer = new StreamWriter("История заказов.txt", true))
            {
                writer.WriteLine("Заказ:");
                foreach (var item in orderDetails)
                {
                    writer.WriteLine($"{item.Key}: {item.Value}");
                }
                writer.WriteLine($"Сумма заказа: {totalCost} руб.");
                writer.WriteLine();
            }
        }

        public static void SetPrices(Dictionary<string, Dictionary<string, double>> priceList)
        {
            prices = priceList;
        }

        public class MenuItem
        {
            public string Description { get; set; }
            public double Price { get; set; }

            public MenuItem(string description, double price)
            {
                Description = description;
                Price = price;
            }
        }
    }

    public class Menu
    {
        public static string ShowMenuAndGetSelection(ICollection<string> options)
        {
            int selectedIndex = 0;
            string[] optionsArray = options.ToArray();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Выберите опцию:");
                for (int i = 0; i < options.Count; i++)
                {
                    if (i == selectedIndex)
                        Console.WriteLine($"> {optionsArray[i]}");
                    else
                        Console.WriteLine($"  {optionsArray[i]}");
                }

                var key = Console.ReadKey().Key;
                if (key == ConsoleKey.Enter)
                    return optionsArray[selectedIndex];
                else if (key == ConsoleKey.Escape)
                    return "Выход";
                else if (key == ConsoleKey.UpArrow)
                    selectedIndex = Math.Max(0, selectedIndex - 1);
                else if (key == ConsoleKey.DownArrow)
                    selectedIndex = Math.Min(options.Count - 1, selectedIndex + 1);
            }
        }

        public static string ShowSubMenuAndGetSelection(Dictionary<string, double> options)
        {
            int selectedIndex = 0;
            string[] optionDescriptions = options.Keys.ToArray();
            Order.MenuItem[] menuItems = options.Select(pair => new Order.MenuItem(pair.Key, pair.Value)).ToArray();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Выберите опцию:");
                for (int i = 0; i < options.Count; i++)
                {
                    if (i == selectedIndex)
                        Console.WriteLine($"> {menuItems[i].Description} - {menuItems[i].Price} руб.");
                    else
                        Console.WriteLine($"  {menuItems[i].Description} - {menuItems[i].Price} руб.");
                }

                var key = Console.ReadKey().Key;
                if (key == ConsoleKey.Enter)
                    return optionDescriptions[selectedIndex];
                else if (key == ConsoleKey.Escape)
                    return "Назад";
                else if (key == ConsoleKey.UpArrow)
                    selectedIndex = Math.Max(0, selectedIndex - 1);
                else if (key == ConsoleKey.DownArrow)
                    selectedIndex = Math.Min(options.Count - 1, selectedIndex + 1);
            }
        }
    }

    class Program
    {
        static void Main()
        {
            Dictionary<string, Dictionary<string, double>> prices = new Dictionary<string, Dictionary<string, double>>
            {
                { "форма", new Dictionary<string, double> { { "круглый", 10 }, { "квадратный", 12 }, { "сердце", 15 } } },
                { "размер", new Dictionary<string, double> { { "маленький", 5 }, { "средний", 8 }, { "большой", 10 } } },
                { "вкус", new Dictionary<string, double> { { "шоколадный", 3 }, { "ванильный", 2 }, { "фруктовый", 4 } } },
                { "глазурь", new Dictionary<string, double> { { "шоколадная", 2 }, { "ванильная", 1 }, { "клубничная", 2 } } },
                { "декор", new Dictionary<string, double> { { "цветы", 5 }, { "фигуры", 4 }, { "шоколадные чипсы", 3 } } }
            };

            Order.SetPrices(prices);
            Order order = new Order();
            order.StartOrder();
        }
    }
}
