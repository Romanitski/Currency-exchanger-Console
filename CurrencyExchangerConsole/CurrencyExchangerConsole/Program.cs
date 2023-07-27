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
            Operator opp = new Operator();
            //opp.Registration("zdsfhuoasdif", "1234", "A");
            //opp.Authorization("TestAff", "1234");
            //opp.OperatorAllEditing("zdsfhuoasdif", "TestEditing", "1234", "A");
            //opp.OperatorActiveEditing("TestEditing", false);

            Coefficients coefficients = new Coefficients();
            //coefficients.AddCoefficientConversion("1,029", "RUB", "USD", "Conversion");
            //coefficients.AddCoefficientConversion("1,015", "RUB", "EUR", "Conversion");
            //coefficients.AddCoefficientConversion("1,0152", "USD", "RUB", "Conversion");
            //coefficients.AddCoefficientConversion("1,0153", "USD", "EUR", "Conversion");
            //coefficients.AddCoefficientConversion("1,08", "EUR", "RUB", "Conversion");
            //coefficients.AddCoefficientConversion("1,09", "EUR", "USD", "Conversion");

            //coefficients.AddCoefficientPurchaseSale("1,076", "RUB", "Purchase");
            //coefficients.AddCoefficientPurchaseSale("1,012", "RUB", "Sale");
            //coefficients.AddCoefficientPurchaseSale("1,0132", "USD", "Purchase");
            //coefficients.AddCoefficientPurchaseSale("1,0142", "USD", "Sale");
            //coefficients.AddCoefficientPurchaseSale("1,06", "EUR", "Purchase");
            //coefficients.AddCoefficientPurchaseSale("1,07", "EUR", "Sale");

            Currencies currencies = new Currencies();
            //currencies.AddNewCurrencies(643, "RUB", "Russian Ruble", "1/100");
            //currencies.AddNewCurrencies(840, "USD", "US Dollar", "1");
            //currencies.AddNewCurrencies(978, "EUR", "Euro", "1");
            //currencies.AddNewCurrencies(933, "BYN", "Belarusian Ruble", "1");

            Rate rate = new Rate();
            //rate.AddNewRateConversion("RUB", "USD", "3,52", DateTime.Now);
            //rate.AddNewRateConversion("RUB", "EUR", "3,33", DateTime.Now);
            //rate.AddNewRateConversion("USD", "RUB", "3,33", DateTime.Now);
            //rate.AddNewRateConversion("USD", "EUR", "3,33", DateTime.Now);
            //rate.AddNewRateConversion("EUR", "RUB", "3,33", DateTime.Now);
            //rate.AddNewRateConversion("EUR", "USD", "3,34", DateTime.Now);

            //rate.AddNewRatePurchase("RUB", "3", DateTime.Now);
            //rate.AddNewRatePurchase("USD", "3,23", DateTime.Now);
            //rate.AddNewRatePurchase("EUR", "3,45", DateTime.Now);

            //rate.AddNewRateSale("RUB", "2,96", DateTime.Now);
            //rate.AddNewRateSale("USD", "3,03", DateTime.Now);
            //rate.AddNewRateSale("EUR", "3,16", DateTime.Now);

            Operation operation = new Operation();
            //operation.Refill("5000", "RUB");
            //operation.Refill("50000", "USD");
            //operation.Refill("5000", "EUR");
            //operation.Refill("5000", "BYN");

            //operation.Conversion("10", "EUR", "USD");
            //operation.Sale("50", "USD");
            //operation.Purchase("10", "EUR");

            Report report = new Report();
            //report.ReportSale();

            GetCurrencies test = new GetCurrencies();
            //test.GetAllCurrencies();
        }
    }
}