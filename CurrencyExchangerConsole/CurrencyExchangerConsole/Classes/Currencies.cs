﻿using System;
using System.Configuration;
using System.Data.SqlClient;

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

        public void AddNewCurrencies(int DigitalCurrencyCode, string AlphabeticCurrencyCode, string CurrencyName, string NumberOfCurrencyUnits)
        {
            string addCurrenciesQuery = "INSERT INTO Currencies VALUES(@DigitalCurrencyCode, @AlphabeticCurrencyCode, @CurrencyName, @NumberOfCurrencyUnits);";

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CurrencyExchanger_db"].ConnectionString))
            {
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
