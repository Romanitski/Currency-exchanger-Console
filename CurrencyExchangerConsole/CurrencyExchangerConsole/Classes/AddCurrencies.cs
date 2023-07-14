using System;
using System.Data.SqlClient;

namespace CurrencyExchangerConsole.Classes
{
    public class AddCurrencies
    {
        public void AddCurrenciesFunction(int DigitalCurrencyCode, string AlphabeticCurrencyCode, string CurrencyName, string NumberOfCurrencyUnits)
        {
            string connectionString = "Data Source=.\\SQLEXPRESS;Database=CurrencyExchanger_db;Trusted_Connection=True;";
            string addCurrenciesQuery = "INSERT INTO Currencies VALUES(@DigitalCurrencyCode, @AlphabeticCurrencyCode, @CurrencyName, @NumberOfCurrencyUnits);";

            SqlConnection sqlConnection = new SqlConnection(connectionString);

            try
            {
                sqlConnection.Open();

                SqlCommand command = new SqlCommand(addCurrenciesQuery, sqlConnection);
                SqlParameter digitalCode = new SqlParameter
                {
                    ParameterName = "@DigitalCurrencyCode",
                    Value = DigitalCurrencyCode
                };
                SqlParameter alphabeticCode = new SqlParameter
                {
                    ParameterName = "@AlphabeticCurrencyCode",
                    Value = AlphabeticCurrencyCode
                };
                SqlParameter currenciesName = new SqlParameter
                {
                    ParameterName = "@CurrencyName",
                    Value = CurrencyName
                };
                SqlParameter numberOfCurrencyUnits = new SqlParameter
                {
                    ParameterName = "@NumberOfCurrencyUnits",
                    Value = NumberOfCurrencyUnits
                };

                command.Parameters.Add(digitalCode);
                command.Parameters.Add(alphabeticCode);
                command.Parameters.Add(currenciesName);
                command.Parameters.Add(numberOfCurrencyUnits);

                command.ExecuteNonQuery();

                sqlConnection.Close();

                Console.WriteLine($"Currency {CurrencyName} with parameters - {DigitalCurrencyCode} \\ {AlphabeticCurrencyCode} \\ {NumberOfCurrencyUnits} has been added!");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
