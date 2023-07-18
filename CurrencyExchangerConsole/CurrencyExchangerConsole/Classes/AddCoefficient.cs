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

        private int GetCoefficientIdConversion(string AlphabeticCurrencyCode, string SecondAlphabeticCurrencyCode, string OperationType)
        {
            string chekQuery = "SELECT CoefficientId FROM Coefficients WHERE Digital_Currency_Code = @DigitalCode AND Second_Digital_Currency_Code = @SecondDigitalCode AND Operation_Type = @OperationType AND Coefficient_Active = @CoefficientActive;";

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CurrencyExchanger_db"].ConnectionString))
            {
                try
                {
                    sqlConnection.Open();

                    int digitalCodeValue = GetDigitalCurrencyCode(AlphabeticCurrencyCode);
                    int secondDigitalCodeValue = GetDigitalCurrencyCode(SecondAlphabeticCurrencyCode);
                    string operationTypeStr = GetOperationType(OperationType);

                    SqlCommand addCoefficientIdCommand = new SqlCommand(chekQuery, sqlConnection);
                    SqlParameter digitalCodeParam = new SqlParameter
                    {
                        ParameterName = "@DigitalCode",
                        Value = digitalCodeValue
                    };
                    SqlParameter secondDigitalCodeParam = new SqlParameter
                    {
                        ParameterName = "@SecondDigitalCode",
                        Value = secondDigitalCodeValue
                    };
                    SqlParameter operationTypeParam = new SqlParameter
                    {
                        ParameterName = "@OperationType",
                        Value = operationTypeStr
                    };
                    SqlParameter coefficientParam = new SqlParameter
                    {
                        ParameterName = "@CoefficientActive",
                        Value = true
                    };

                    addCoefficientIdCommand.Parameters.Add(digitalCodeParam);
                    addCoefficientIdCommand.Parameters.Add(secondDigitalCodeParam);
                    addCoefficientIdCommand.Parameters.Add(operationTypeParam);
                    addCoefficientIdCommand.Parameters.Add(coefficientParam);

                    object idValue = addCoefficientIdCommand.ExecuteScalar();

                    int idValueInt = Convert.ToInt32((idValue));

                    sqlConnection.Close();

                    return (idValueInt);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return (0);
                }
            }
        }

        private void UpdateCoefficientActiveConversion(int CoefficientId)
        {
            string updateQuery = "UPDATE Coefficients SET Coefficient_Active = @CoefficientActive WHERE CoefficientId = @CoefficientId;";

            using(SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CurrencyExchanger_db"].ConnectionString))
            {
                try
                {
                    sqlConnection.Open();

                    SqlCommand updateCommand = new SqlCommand(updateQuery, sqlConnection);
                    SqlParameter coefficientActiveParam = new SqlParameter
                    {
                        ParameterName = "@CoefficientActive",
                        Value = false
                    };
                    SqlParameter coefficientIdParam = new SqlParameter
                    {
                        ParameterName = "@CoefficientId",
                        Value = CoefficientId
                    };

                    updateCommand.Parameters.Add(coefficientActiveParam);
                    updateCommand.Parameters.Add(coefficientIdParam);

                    updateCommand.ExecuteNonQuery();

                    sqlConnection.Close();
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void AddCoefficientPurchaseSaleFunction(string Coefficient, string AlphabeticCurrencyCode, string OperationType, bool CoefficientActive)
        {
            string addCoefficientQuery = "INSERT INTO Coefficients VALUES(@Coefficient, @DigitalCurrencyCode, '0', @OperationType, @DateOfIssue, @CoefficientActive);";

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

        public void AddCoefficientConversionFunction(string Coefficient, string AlphabeticCurrencyCode, string SecondAlphabeticCurrencyCode, string OperationType, bool CoefficientActive)
        {
            string addCoefficientQuery = "INSERT INTO Coefficients VALUES(@Coefficient, @DigitalCurrencyCode, @SecondDigitalCurrencyCode, @OperationType, @DateOfIssue, @CoefficientActive);";

            op = Coefficients.GetInstance();

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CurrencyExchanger_db"].ConnectionString))
            {
                try
                {
                    sqlConnection.Open();

                    int digitalCode = GetDigitalCurrencyCode(AlphabeticCurrencyCode);
                    int secondDigitalCode = GetDigitalCurrencyCode(SecondAlphabeticCurrencyCode);
                    string operationTypeStr = GetOperationType(OperationType);
                    int coefficientId = GetCoefficientIdConversion(AlphabeticCurrencyCode, SecondAlphabeticCurrencyCode, OperationType);
                    UpdateCoefficientActiveConversion(coefficientId);

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
                    SqlParameter secondDigitalCurrencyCode = new SqlParameter
                    {
                        ParameterName = "@SecondDigitalCurrencyCode",
                        Value = secondDigitalCode
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
                    addCommand.Parameters.Add(secondDigitalCurrencyCode);
                    addCommand.Parameters.Add(operationType);
                    addCommand.Parameters.Add(dateOfIssue);
                    addCommand.Parameters.Add(coefficientActive);

                    addCommand.ExecuteNonQuery();

                    sqlConnection.Close();

                    Console.WriteLine($"Coefficient {Coefficient} with parameters - {AlphabeticCurrencyCode}/{SecondAlphabeticCurrencyCode} - {OperationType} has been added!\n");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}