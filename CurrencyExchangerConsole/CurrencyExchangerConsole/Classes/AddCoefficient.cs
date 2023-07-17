using System;
using System.Configuration;
using System.Data.SqlClient;

namespace CurrencyExchangerConsole.Classes
{
    public class AddCoefficient
    {
        private Coefficients op;

        private int GetDigitalCurrencyCode(string AlphabeticCurrencyCode)
        {
            string getCode = "SELECT Digital_Currency_Code FROM Currencies WHERE Alphabetic_Currency_Code = @AlphabeticCurrencyCode";

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

        private string GetOperationType(string OperationName)
        {
            string getType = "SELECT Operation_Type FROM Operations_Type WHERE Operation_Name = @OperationName";
            
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CurrencyExchanger_db"].ConnectionString))
            {
                try
                {
                    sqlConnection.Open();

                    SqlCommand commandGetType = new SqlCommand(getType, sqlConnection);
                    SqlParameter typeParameter = new SqlParameter
                    {
                        ParameterName = "@OperationName",
                        Value = OperationName
                    };

                    commandGetType.Parameters.Add(typeParameter);

                    object typeValue = commandGetType.ExecuteScalar();

                    string codeValueStr = typeValue.ToString();

                    sqlConnection.Close();
                    return (codeValueStr);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    sqlConnection.Close();
                    return (null);
                }
            }
        }

        public void AddCoefficientFunction(string Coefficient, string AlphabeticCurrencyCode, string OperationType, bool CoefficientActive)
        {
            string addCoefficientQuery = "INSERT INTO Coefficients VALUES(@Coefficient, @DigitalCurrencyCode, @OperationType, @DateOfIssue, @CoefficientActive);";

            op = Coefficients.GetInstance();

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CurrencyExchanger_db"].ConnectionString))
            {
                try
                {
                    sqlConnection.Open();

                    int digitalCode = GetDigitalCurrencyCode(AlphabeticCurrencyCode);
                    string operationTypeStr = GetOperationType(OperationType);

                    SqlCommand addCommand = new SqlCommand(addCoefficientQuery, sqlConnection);
                    SqlParameter coefficient = new SqlParameter
                    {
                        ParameterName = "@Coefficient",
                        Value = Coefficient
                    };
                    SqlParameter digitalCurrencyCode = new SqlParameter
                    {
                        ParameterName = "@DigitalCurrencyCode",
                        Value = digitalCode
                    };
                    SqlParameter operationType = new SqlParameter
                    {
                        ParameterName = "@OperationType",
                        Value = operationTypeStr
                    };
                    SqlParameter dateOfIssue = new SqlParameter
                    {
                        ParameterName = "@DateOfIssue",
                        Value = DateTime.Now
                    };
                    SqlParameter coefficientActive = new SqlParameter
                    {
                        ParameterName = "@CoefficientActive",
                        Value = CoefficientActive
                    };

                    addCommand.Parameters.Add(coefficient);
                    addCommand.Parameters.Add(digitalCurrencyCode);
                    addCommand.Parameters.Add(operationType);
                    addCommand.Parameters.Add(dateOfIssue);
                    addCommand.Parameters.Add(coefficientActive);

                    addCommand.ExecuteNonQuery();

                    sqlConnection.Close();

                    Console.WriteLine($"Coefficient {Coefficient} with parameters - {AlphabeticCurrencyCode} \\ {OperationType} has been added!\n");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}