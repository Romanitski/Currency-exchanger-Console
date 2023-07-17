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
            AddRateConversion rateConversion = new AddRateConversion();

            //registration.RegistrationFunction("TestA", "1234", "A", 1);
            //registration.RegistrationFunction("TestB", "1234", "B", 1);
            //registration.RegistrationFunction("TestC", "1234", "C", 1);
            //registration.RegistrationFunction("TestInActive", "1234", "A", false);

            //authorization.AuthorizationFunction("TestA","1234");
            //authorization.AuthorizationFunction("TestB","1234");
            //authorization.AuthorizationFunction("TestC","1234");
            //authorization.AuthorizationFunction("TestInActive", "1234");
            //authorization.AuthorizationFunction("TestError", "1234");

            //operatorEditing.OperatorActiveEditingfunction("TestInActive", false);
            //operatorEditing.OperatorEditingFunction("Nams", "TestInActive", "1234", "A");

            //addCurrencies.AddCurrenciesFunction(643, "RUB", "Russian Ruble", "1/100");
            //addCurrencies.AddCurrenciesFunction(840, "USD", "US Dollar", "1");
            //addCurrencies.AddCurrenciesFunction(978, "EUR", "Euro", "1");

            //addCoefficient.AddCoefficientFunction("1,01", "RUB", "Purchase", DateTime.Now);
            //addCoefficient.AddCoefficientFunction("1,01", "RUB", "Sale", DateTime.Now);
            //addCoefficient.AddCoefficientFunction("1,01", "RUB", "Conversion", DateTime.Now);

            //addCoefficient.AddCoefficientFunction("1,01", "USD", "Purchase", DateTime.Now);
            //addCoefficient.AddCoefficientFunction("1,01", "USD", "Sale", DateTime.Now);
            //addCoefficient.AddCoefficientFunction("1,01", "USD", "Conversion", DateTime.Now);

            //addCoefficient.AddCoefficientFunction("1,06", "EUR", "Purchase", DateTime.Now);
            //addCoefficient.AddCoefficientFunction("1,07", "EUR", "Sale", DateTime.Now);
            //addCoefficient.AddCoefficientFunction("1,08", "EUR", "Conversion", DateTime.Now);

            ratePurchase.AddRatePurchaseFunction("RUB", "3,01", DateTime.Now);
            ratePurchase.AddRatePurchaseFunction("USD", "3,21", DateTime.Now);
            ratePurchase.AddRatePurchaseFunction("EUR", "3,47", DateTime.Now);

            //rateConversion.AddRateConversionFunction("RUB", "3,52", DateTime.Now);
        }
    }
}
