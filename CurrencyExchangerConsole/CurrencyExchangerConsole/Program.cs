using System;
using CurrencyExchangerConsole.Classes;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchangerConsole
{
    public class Program
    {
        static void Main(string[] args)
        {
            Operator user = new Operator();
            Registration registration = new Registration();
            Classes.Authorization authorization = new Classes.Authorization();
            OperatorEditing operatorEditing = new OperatorEditing();
            AddCurrencies addCurrencies = new AddCurrencies();
            AddCoefficient addCoefficient = new AddCoefficient();
            AddRatePurchase ratePurchase = new AddRatePurchase();
            AddRateSale rateSale = new AddRateSale();
            AddRateConversion rateConversion = new AddRateConversion();

            //registration.RegistrationFunction("TestA", "1234", "A", true);
            //registration.RegistrationFunction("TestB", "1234", "B", true);
            //registration.RegistrationFunction("TestC", "1234", "C", true);
            //registration.RegistrationFunction("TestInActive", "1234", "A", false);

            //authorization.AuthorizationFunction("TestA", "1234");
            //authorization.AuthorizationFunction("TestB", "1234");
            //authorization.AuthorizationFunction("TestC", "1234");
            //authorization.AuthorizationFunction("TestInActive", "1234");
            //authorization.AuthorizationFunction("TestError", "1234");

            //operatorEditing.OperatorActiveEditingfunction("TestInActive", false);
            //operatorEditing.OperatorEditingFunction("TestInActive1", "TestInActive", "1234", "A");

            //addCurrencies.AddCurrenciesFunction(643, "RUB", "Russian Ruble", "1/100");
            //addCurrencies.AddCurrenciesFunction(840, "USD", "US Dollar", "1");
            //addCurrencies.AddCurrenciesFunction(978, "EUR", "Euro", "1");

            //addCoefficient.AddCoefficientPurchaseSaleFunction("1,011", "RUB", "Purchase", true);
            //addCoefficient.AddCoefficientPurchaseSaleFunction("1,012", "RUB", "Sale", true);
            //addCoefficient.AddCoefficientConversionFunction("1,013", "RUB", "USD", "Conversion", true);
            //addCoefficient.AddCoefficientConversionFunction("1,014", "RUB", "EUR", "Conversion", true);

            //addCoefficient.AddCoefficientPurchaseSaleFunction("1,0132", "USD", "Purchase", true);
            //addCoefficient.AddCoefficientPurchaseSaleFunction("1,0142", "USD", "Sale", true);
            //addCoefficient.AddCoefficientConversionFunction("1,0152", "USD", "RUB", "Conversion", true);
            //addCoefficient.AddCoefficientConversionFunction("1,0153", "USD", "EUR", "Conversion", true);

            //addCoefficient.AddCoefficientPurchaseSaleFunction("1,06", "EUR", "Purchase", true);
            //addCoefficient.AddCoefficientPurchaseSaleFunction("1,07", "EUR", "Sale", true);
            //addCoefficient.AddCoefficientConversionFunction("1,08", "EUR", "RUB", "Conversion", true);
            //addCoefficient.AddCoefficientConversionFunction("1,09", "EUR", "USD", "Conversion", true);

            //ratePurchase.AddRatePurchaseFunction("RUB", "3,01", DateTime.Now);
            //ratePurchase.AddRatePurchaseFunction("USD", "3,21", DateTime.Now);
            //ratePurchase.AddRatePurchaseFunction("EUR", "3,47", DateTime.Now);

            //rateSale.AddRateSaleFunction("RUB", "2,95", DateTime.Now);
            //rateSale.AddRateSaleFunction("USD", "3,02", DateTime.Now);
            //rateSale.AddRateSaleFunction("EUR", "3,15", DateTime.Now);

            rateConversion.AddRateConversionFunction("RUB", "3,52", DateTime.Now);
        }
    }
}
