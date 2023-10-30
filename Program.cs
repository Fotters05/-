using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static Dictionary<string, Dictionary<string, double>> prices = new Dictionary<string, Dictionary<string, double>>
    {
        { "форма", new Dictionary<string, double> { { "круглый", 10 }, { "квадратный", 12 }, { "шар", 15 } } },
        { "размер", new Dictionary<string, double> { { "маленький", 5 }, { "средний", 8 }, { "большой", 10 } } },
        { "вкус", new Dictionary<string, double> { { "персиковый", 3 }, { "банановый", 2 }, { "вишневый", 4 } } },
        { "глазурь", new Dictionary<string, double> { { "персиковая", 2 }, { "шоколадная", 1 }, { "вишневая", 2 } } },
        { "декор", new Dictionary<string, double> { { "звезда", 5 }, { "бабочка", 4 }, { "вишня", 3 } } }
    };

    static Dictionary<string, string> order = new Dictionary<string, string>
    {
        { "форма", "" },
        { "размер", "" },
        { "вкус", "" },
        { "глазурь", "" },
        { "декор", "" }
    };

    static void Main()
    {
        bool continueOrdering = true;
        while (continueOrdering)
        {
            Console.Clear();
            Console.WriteLine("Меню заказа тортов:");
            Console.WriteLine("Выберите опцию и нажмите Enter:");

            string selectedOption = ShowMenuAndGetSelection(order.Keys);
            if (selectedOption == "Выход")
            {
                continueOrdering = false;
                continue;
            }

            order[selectedOption] = ShowSubMenuAndGetSelection(prices[selectedOption].Keys);
        }

        double totalCost = CalculateTotalCost(order, prices);
        SaveOrderToHistory(order, totalCost);

        Console.WriteLine("Заказ сохранен в истории заказов.");
        Console.WriteLine($"Итоговая стоимость заказа: {totalCost} рублей");

        Console.WriteLine("Хотите сделать еще один заказ? (Да/Нет)");
        string anotherOrder = Console.ReadLine().ToLower();
        if (anotherOrder == "да")
        {
            Main();
        }
    }

    static string ShowMenuAndGetSelection(ICollection<string> options)
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

    static string ShowSubMenuAndGetSelection(ICollection<string> options)
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
                return "Назад";
            else if (key == ConsoleKey.UpArrow)
                selectedIndex = Math.Max(0, selectedIndex - 1);
            else if (key == ConsoleKey.DownArrow)
                selectedIndex = Math.Min(options.Count - 1, selectedIndex + 1);
        }
    }

    static double CalculateTotalCost(Dictionary<string, string> order, Dictionary<string, Dictionary<string, double>> prices)
    {
        double totalCost = 0;
        foreach (var item in order)
        {
            totalCost += prices[item.Key][item.Value];
        }
        return totalCost;
    }

    static void SaveOrderToHistory(Dictionary<string, string> order, double totalCost)
    {
        using (StreamWriter writer = new StreamWriter("История заказов.txt", true))
        {
            writer.WriteLine("Заказ:");
            foreach (var item in order)
            {
                writer.WriteLine($"{item.Key}: {item.Value}");
            }
            writer.WriteLine($"Сумма заказа: {totalCost} руб.");
            writer.WriteLine();
        }
    }
}