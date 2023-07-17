using System;
using System.Configuration;
using System.Data.SqlClient;
namespace CurrencyExchangerConsole.Classes
{
    public class AddRateSale
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

        private int GetCoefficientId(string AlphabeticCurrencyCode, string OperationType, string coefficientValueStr)
        {
            string getCoefficientId = "SELECT CoefficientId FROM Coefficients WHERE Digital_Currency_Code = @DigitalCode AND Operation_Type = @OperationType AND Coefficient = @CoefficientValue;";

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CurrencyExchanger_db"].ConnectionString))
            {
                try
                {
                    sqlConnection.Open();

                    int digitalCode = GetDigitalCurrencyCode(AlphabeticCurrencyCode);

                    SqlCommand commandGetCoefficientId = new SqlCommand(getCoefficientId, sqlConnection);
                    SqlParameter coefficient = new SqlParameter
                    {
                        ParameterName = "@CoefficientValue",
                        Value = coefficientValueStr
                    };
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

                    commandGetCoefficientId.Parameters.Add(coefficient);
                    commandGetCoefficientId.Parameters.Add(digitalCodeParameter);
                    commandGetCoefficientId.Parameters.Add(operationType);

                    object coefficientIdValue = commandGetCoefficientId.ExecuteScalar();

                    int coefficientIdValueInt = Convert.ToInt32((coefficientIdValue));

                    sqlConnection.Close();
                    return (coefficientIdValueInt);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    sqlConnection.Close();
                    return (0);
                }
            }
        }

        public void AddRateSaleFunction(string AlphabeticCurrencyCode, string RateSale, DateTime DateOfTheStartAction)
        {
            string addRateQuery = "INSERT INTO Rate_Sale VALUES(@OperatorId, @DigitalCurrencyCode, @RateSale, @CoefficientId, @DateOfIssue, @DateOfTheStartAction);";

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CurrencyExchanger_db"].ConnectionString))
            {
                try
                {
                    //Make a selection for the active coefficient!!!

                    sqlConnection.Open();

                    int digitalCode = GetDigitalCurrencyCode(AlphabeticCurrencyCode);
                    int id = GetOperatorId("TestB");
                    string coefficientValue = GetCoefficient(AlphabeticCurrencyCode, "C");
                    int coefficientIdValue = GetCoefficientId(AlphabeticCurrencyCode, "C", coefficientValue);

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
                    SqlParameter rateSale = new SqlParameter
                    {
                        ParameterName = "@Ratesale",
                        Value = RateSale
                    };
                    SqlParameter coefficient = new SqlParameter
                    {
                        ParameterName = "@CoefficientId",
                        Value = coefficientIdValue
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
                    addRateCommand.Parameters.Add(rateSale);
                    addRateCommand.Parameters.Add(coefficient);
                    addRateCommand.Parameters.Add(dateOfIssue);
                    addRateCommand.Parameters.Add(dateOfTheStartAction);

                    addRateCommand.ExecuteNonQuery();

                    sqlConnection.Close();

                    Console.WriteLine($"The sale rate - {RateSale} for the {AlphabeticCurrencyCode} currency were successfully added!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
