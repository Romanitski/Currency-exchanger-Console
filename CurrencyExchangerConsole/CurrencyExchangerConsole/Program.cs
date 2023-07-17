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
            AddRatePurchaseSale purchaseSale = new AddRatePurchaseSale();
            AddRateConversion rateConversion = new AddRateConversion();

            //registration.RegistrationFunction("TestA", "1234", "A", 1);
            //registration.RegistrationFunction("TestB", "1234", "B", 1);
            //registration.RegistrationFunction("TestC", "1234", "C", 1);
            registration.RegistrationFunction("TestInActive", "1234", "A", false);

            //authorization.AuthorizationFunction("TestA","1234");
            //authorization.AuthorizationFunction("TestB","1234");
            //authorization.AuthorizationFunction("TestC","1234");
            //authorization.AuthorizationFunction("TestERROR","1234");
            //authorization.AuthorizationFunction(user.operatorName, user.operatorPassword);

            //operatorEditing.OperatorEditingFunction("Nameszshjb", "Nams", "1234", "C");

            //addCurrencies.AddCurrenciesFunction(840, "USD", "Доллар США", "1");
            //addCurrencies.AddCurrenciesFunction(978, "EUR", "Евро", "1");

            //addCoefficient.AddCoefficientFunction("1,03", "RUB", "Purchase", DateTime.Now);
            //addCoefficient.AddCoefficientFunction("1,04", "RUB", "Sale", DateTime.Now);
            //addCoefficient.AddCoefficientFunction("1,05", "RUB", "Conversion", DateTime.Now);

            //addCoefficient.AddCoefficientFunction("1,01", "USD", "Purchase", DateTime.Now);
            //addCoefficient.AddCoefficientFunction("1,02", "USD", "Sale", DateTime.Now);
            //addCoefficient.AddCoefficientFunction("1,011", "USD", "Conversion", DateTime.Now);

            //addCoefficient.AddCoefficientFunction("1,06", "EUR", "Purchase", DateTime.Now);
            //addCoefficient.AddCoefficientFunction("1,07", "EUR", "Sale", DateTime.Now);
            //addCoefficient.AddCoefficientFunction("1,08", "EUR", "Conversion", DateTime.Now);

            //purchaseSale.AddRatePurchaseSaleFunction("RUB", "2,99", "3,33", DateTime.Now);

            //rateConversion.AddRateConversionFunction("RUB", "3,52", DateTime.Now);
        }
    }
}
