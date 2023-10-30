using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Tortik
{
    class Program
    {
        static void Main()
        {
            Dictionary<string, Dictionary<string, double>> prices = new Dictionary<string, Dictionary<string, double>>
            {
                { "форма", new Dictionary<string, double> { { "круглый", 10 }, { "квадратный", 12 }, { "шар", 15 } } },
                { "размер", new Dictionary<string, double> { { "маленький", 5 }, { "средний", 8 }, { "большой", 10 } } },
                { "вкус", new Dictionary<string, double> { { "персиковый", 3 }, { "банановый", 2 }, { "вишневый", 4 } } },
                { "глазурь", new Dictionary<string, double> { { "персиковая", 2 }, { "шоколадная", 1 }, { "вишневая", 2 } } },
                { "декор", new Dictionary<string, double> { { "звезда", 5 }, { "бабочка", 4 }, { "вишня", 3 } } }
            };

            Order.SetPrices(prices);
            Order order = new Order();
            order.StartOrder();
        }
    }
}