using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaniTrader.Fyers
{
    public class CurrencyTypeRepository
    {
        // List of all currencies with their properties.
        public static readonly Dictionary<string, Currency> Currencies =
            new Dictionary<string, Currency>()
            {
            {"AUD", new Currency("AUD", false, "Australian dollar", "$",        2, 2, ".", ",")},
            {"CAD", new Currency("CAD", false, "Canadian dollar", "$",          2, 2, ".", ",")},
            {"CNY", new Currency("CNY", false, "Renminbi", "¥",                 2, 2, ".", ",")},
            {"EUR", new Currency("EUR", false, "Euro", "€" ,                    2, 2, ".", ",")},
            {"GBP", new Currency("GBP", false, "Pound sterling", "£",           2, 2, ".", ",")},
            {"HKD", new Currency("HKD", false, "Hong Kong dollar", "HKD$",      2, 2, ".", ",")},
            {"IDR", new Currency("IDR", false, "Indonesian rupiah", "Rp",       2, 2, ".", ",")},
            {"INR", new Currency("INR", false, "Indian rupee", "Rs",            2, 2, ".", ",")},
            {"JPY", new Currency("JPY", false, "Japanese yen", "¥",             0, 0, ".", ",")},
            {"KRW", new Currency("KRW", false, "South Korean won", "₩",         0, 0, ".", ",")},
            {"MOP", new Currency("MOP", false, "Pataca", "MOP$",                2, 2, ".", ",")},
            {"NZD", new Currency("NZD", false, "New Zealand dollar", "$",       2, 2, ".", ",")},
            {"PHP", new Currency("PHP", false, "Philippine peso", "P",          2, 2, ".", ",")},
            {"RUB", new Currency("RUB", false, "Russian ruble", "PP",           2, 2, ".", ",")},
            {"SGD", new Currency("SGD", false, "Singapore dollar", "S$",        2, 2, ".", ",")},
            {"TWD", new Currency("TWD", false, "New Taiwan dollar", "$",        2, 2, ".", ",")},
            {"USD", new Currency("USD", false, "US dollar", "$",                2, 2, ".", ",")},
            {"VND", new Currency("VND", false, "Vietnamese dong", "₫",          2, 2, ".", ",")},
            {"ZAR", new Currency("ZAR", false, "South African rand", "R",       2, 2, ".", ",")}
            };

        public Currency Get(string isoCode)
        {
            if (Currencies.ContainsKey(isoCode))
            {
                return Currencies[isoCode];
            }
            else
            {
                return null;
            }
        }

        public bool Exists(string isoCode)
        {
            return Currencies.ContainsKey(isoCode);
        }
    }
}
