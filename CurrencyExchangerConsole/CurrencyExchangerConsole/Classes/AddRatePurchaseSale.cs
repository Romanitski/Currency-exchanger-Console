﻿using System;
using System.Configuration;
using System.Data.SqlClient;

namespace CurrencyExchangerConsole.Classes
{
    public class AddRatePurchaseSale
    {
        private int GetDigitalCurrencyCode(string AlphabeticCurrencyCode)
        {
            string getCode = "SELECT Digital_Currency_Code FROM Currencies WHERE Alphabetic_Currency_Code = @AlphabeticCurrencyCode;";

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CurrencyExchanger_db"].ConnectionString))
            {
                try
                {
                    sqlConnection.Open();

                    SqlCommand commandGetCode = new SqlCommand(getCode, sqlConnection);
                    SqlParameter code = new SqlParameter
                    {
                        ParameterName = "@AlphabeticCurrencyCode",
                        Value = AlphabeticCurrencyCode
                    };

                    commandGetCode.Parameters.Add(code);

                    object codeValue = commandGetCode.ExecuteScalar();

                    int codeValueInt = Convert.ToInt32((codeValue));

                    sqlConnection.Close();
                    return (codeValueInt);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    sqlConnection.Close();
                    return (0);
                }
            }
        }

        private int GetOperatorId(string OperatorName)
        {
            string getId = "SELECT Operator_Id FROM Operators WHERE Operator_Name = @OperatorName;";

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CurrencyExchanger_db"].ConnectionString))
            {
                try
                {
                    sqlConnection.Open();

                    SqlCommand commandGetId = new SqlCommand(getId, sqlConnection);
                    SqlParameter id = new SqlParameter
                    {
                        ParameterName = "@OperatorName",
                        Value = OperatorName
                    };

                    commandGetId.Parameters.Add(id);

                    object idValue = commandGetId.ExecuteScalar();

                    int idValueInt = Convert.ToInt32((idValue));

                    sqlConnection.Close();
                    return (idValueInt);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    sqlConnection.Close();
                    return (0);
                }
            }
        }

        private string GetCoefficient(string AlphabeticCurrencyCode, string OperationType)
        {
            string getCoefficient = "SELECT Coefficient FROM Coefficients WHERE Digital_Currency_Code = @DigitalCode AND Operation_Type = @OperationType;";

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CurrencyExchanger_db"].ConnectionString))
            {
                try
                {
                    sqlConnection.Open();

                    int digitalCode = GetDigitalCurrencyCode(AlphabeticCurrencyCode);

                    SqlCommand commandGetCoefficient = new SqlCommand(getCoefficient, sqlConnection);
                    SqlParameter digitalCodeParameter = new SqlParameter
                    {
                        ParameterName = "@DigitalCode",
                        Value = digitalCode
                    };
                    SqlParameter operationType = new SqlParameter
                    {
                        ParameterName = "@OperationType",
                        Value = OperationType
                    };

                    commandGetCoefficient.Parameters.Add(digitalCodeParameter);
                    commandGetCoefficient.Parameters.Add(operationType);

                    object coefficientValue = commandGetCoefficient.ExecuteScalar();

                    string coefficientValueStr = coefficientValue.ToString();

                    sqlConnection.Close();
                    return (coefficientValueStr);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    sqlConnection.Close();
                    return (null);
                }
            }
        }

        public void AddRatePurchaseSaleFunction(string AlphabeticCurrencyCode, string RatePurchase, string RateSale, DateTime DateOfTheStartAction)
        {
            string addRateQuery = "INSERT INTO Rate_Purchase_Sale VALUES(@OperatorId, @DigitalCurrencyCode, @RatePurchase, @RateSale, @Coefficient, @DateOfIssue, @DateOfTheStartAction);";

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CurrencyExchanger_db"].ConnectionString))
            {
                try
                {
                    sqlConnection.Open();

                    int digitalCode = GetDigitalCurrencyCode(AlphabeticCurrencyCode);
                    int id = GetOperatorId("TestB");
                    string coefficientValue = GetCoefficient(AlphabeticCurrencyCode, "B");

                    SqlCommand addRateCommand = new SqlCommand(addRateQuery, sqlConnection);
                    SqlParameter operatorId = new SqlParameter
                    {
                        ParameterName = "@OperatorId",
                        Value = id
                    };
                    SqlParameter digitalCodeParameter = new SqlParameter
                    {
                        ParameterName = "@DigitalCurrencyCode",
                        Value = digitalCode
                    };
                    SqlParameter ratePurchase = new SqlParameter
                    {
                        ParameterName = "@RatePurchase",
                        Value = RatePurchase
                    };
                    SqlParameter rateSale = new SqlParameter
                    {
                        ParameterName = "@RateSale",
                        Value = RateSale
                    };
                    SqlParameter coefficient = new SqlParameter
                    {
                        ParameterName = "@Coefficient",
                        Value = coefficientValue
                    };
                    SqlParameter dateOfIssue = new SqlParameter
                    {
                        ParameterName = "@DateOfIssue",
                        Value = DateTime.Now
                    };
                    SqlParameter dateOfTheStartAction = new SqlParameter
                    {
                        ParameterName = "@DateOfTheStartAction",
                        Value = DateOfTheStartAction
                    };

                    addRateCommand.Parameters.Add(operatorId);
                    addRateCommand.Parameters.Add(digitalCodeParameter);
                    addRateCommand.Parameters.Add(ratePurchase);
                    addRateCommand.Parameters.Add(rateSale);
                    addRateCommand.Parameters.Add(coefficient);
                    addRateCommand.Parameters.Add(dateOfIssue);
                    addRateCommand.Parameters.Add(dateOfTheStartAction);

                    addRateCommand.ExecuteNonQuery();

                    sqlConnection.Close();

                    Console.WriteLine($"The purchase rate - {RatePurchase} and the sale rate - {RateSale} for the {AlphabeticCurrencyCode} currency were successfully added!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }   
        }
    }
}