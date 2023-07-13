using System;

namespace CurrencyExchangerConsole.Classes
{
    public class Currencies
    {
        private static Currencies instance;

        public static Currencies GetInstance()
        {
            return instance ?? (instance = new Currencies());
        }

        public Currencies () { }

        public Currencies(string currencyName, string alphabeticCurrencyCode, string numberOfCurrencyUnits)
        {
            this.currencyName = currencyName;
            this.alphabeticCurrencyCode = alphabeticCurrencyCode;
            this.numberOfCurrencyUnits = numberOfCurrencyUnits;
        }

        public Currencies(int digitalCurrencyCode, string currencyName, string alphabeticCurrencyCode, string numberOfCurrencyUnits)
        {
            this.digitalCurrencyCode = digitalCurrencyCode;
            this.currencyName = currencyName;
            this.alphabeticCurrencyCode = alphabeticCurrencyCode;
            this.numberOfCurrencyUnits = numberOfCurrencyUnits;
        }

        public int digitalCurrencyCode { get; set; }
        public string alphabeticCurrencyCode { get; set; }
        public string currencyName { get; set; }
        public string numberOfCurrencyUnits { get; set; }
    }
}
