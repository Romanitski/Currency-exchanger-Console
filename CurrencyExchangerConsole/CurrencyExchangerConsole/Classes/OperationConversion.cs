using System;
using System.Configuration;
using System.Data.SqlClient;

namespace CurrencyExchangerConsole.Classes
{
    public class OperationConversion
    {
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
            string getTypeQuery = "SELECT Operation_Type FROM Operations_Type WHERE Operation_Name = @OperationName;";

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CurrencyExchanger_db"].ConnectionString))
            {
                try
                {
                    sqlConnection.Open();

                    SqlCommand addOperationCommand = new SqlCommand(getTypeQuery, sqlConnection);
                    SqlParameter operationName = new SqlParameter
                    {
                        ParameterName = "@OperationName",
                        Value = OperationName
                    };

                    addOperationCommand.Parameters.Add(operationName);

                    object typeValue = addOperationCommand.ExecuteScalar();

                    string typeValueStr = typeValue.ToString();

                    sqlConnection.Close();
                    return (typeValueStr);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return (null);
                }
            }
        }

        private int GetNewOperationId(int CurrencyCode)
        {
            string getIdQuery = "SELECT MAX(Operation_Id) FROM Operations WHERE Digital_Currency_Code = @CurrencyCode AND Operation_Type = @OperationType;";

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CurrencyExchanger_db"].ConnectionString))
            {
                try
                {
                    sqlConnection.Open();

                    string type = GetOperationType("Conversion");

                    SqlCommand getIdCommand = new SqlCommand(getIdQuery, sqlConnection);
                    SqlParameter currencyCode = new SqlParameter
                    {
                        ParameterName = "@CurrencyCode",
                        Value = CurrencyCode
                    };
                    SqlParameter operationType = new SqlParameter
                    {
                        ParameterName = "@OperationType",
                        Value = type
                    };

                    getIdCommand.Parameters.Add(currencyCode);
                    getIdCommand.Parameters.Add(operationType);

                    object idValue = getIdCommand.ExecuteScalar();

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

        public void ConversionFunction(string AmountValue, string AlphabeticCurrencyCode)
        {
            string addOperationQuery = "INSERT INTO Operations VALUES(@OperatorId, @DigitalCurrencyCode, @DateOfIssue, @TransactionAmount, @TransactionDelivery, @OperationType);";

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CurrencyExchanger_db"].ConnectionString))
            {
                try
                {

                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private string GetAmountValue(int DigitalCurrencyCode, int idOperation)
        {
            string getAmountQuery = "SELECT Bank_Amount FROM Banking_Information WHERE Operation_Id = @OperationId AND Digital_Currency_Code = @CurrencyCode;";

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CurrencyExchanger_db"].ConnectionString))
            {
                try
                {
                    sqlConnection.Open();

                    SqlCommand getAmountCommand = new SqlCommand(getAmountQuery, sqlConnection);
                    SqlParameter code = new SqlParameter
                    {
                        ParameterName = "@CurrencyCode",
                        Value = DigitalCurrencyCode
                    };
                    SqlParameter id = new SqlParameter
                    {
                        ParameterName = "@OperationId",
                        Value = idOperation
                    };

                    getAmountCommand.Parameters.Add(code);
                    getAmountCommand.Parameters.Add(id);

                    object amountValue = getAmountCommand.ExecuteScalar();

                    string amountValueStr = amountValue.ToString();

                    sqlConnection.Close();
                    return (amountValueStr);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return (null);
                }
            }
        }

        private void AddAmountFunction(string AlphabeticCurrencyCode, string AddValue)
        {
            string addQuery = "INSERT INTO Banking_Information VALUES(@BankAmount, @OperationId, @DigitalCurrencyCode);";

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CurrencyExchanger_db"].ConnectionString))
            {
                try
                {
                    sqlConnection.Open();

                    int digitalCode = GetDigitalCurrencyCode(AlphabeticCurrencyCode);
                    int newId = GetNewOperationId(digitalCode);

                    string amountValue = GetAmountValue(digitalCode, newId);

                    int firstValue = int.Parse(amountValue);
                    int secondValue = int.Parse(AddValue);
                    int finallyValue = firstValue + secondValue;
                    string finalyAmount = finallyValue.ToString();

                    SqlCommand insertAmountCommand = new SqlCommand(addQuery, sqlConnection);
                    SqlParameter amount = new SqlParameter
                    {
                        ParameterName = "@BankAmount",
                        Value = finalyAmount
                    };
                    SqlParameter code = new SqlParameter
                    {
                        ParameterName = "@DigitalCurrencyCode",
                        Value = digitalCode
                    };
                    SqlParameter id = new SqlParameter
                    {
                        ParameterName = "@OperationId",
                        Value = newId
                    };

                    insertAmountCommand.Parameters.Add(amount);
                    insertAmountCommand.Parameters.Add(code);
                    insertAmountCommand.Parameters.Add(id);

                    insertAmountCommand.ExecuteNonQuery();

                    sqlConnection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void ReleaseAmountFunction(string AlphabeticCurrencyCode, string AddValue)
        {
            string addQuery = "INSERT INTO Banking_Information VALUES(@BankAmount, @OperationId, @DigitalCurrencyCode);";

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CurrencyExchanger_db"].ConnectionString))
            {
                try
                {
                    sqlConnection.Open();

                    int digitalCode = GetDigitalCurrencyCode(AlphabeticCurrencyCode);
                    int newId = GetNewOperationId(digitalCode); //нужно получить id той же операции

                    string amountValue = GetAmountValue(digitalCode, newId);

                    int firstValue = int.Parse(amountValue);
                    int secondValue = int.Parse(AddValue);
                    int finallyValue = firstValue - secondValue;
                    string finalyAmount = finallyValue.ToString();

                    SqlCommand insertAmountCommand = new SqlCommand(addQuery, sqlConnection);
                    SqlParameter amount = new SqlParameter
                    {
                        ParameterName = "@BankAmount",
                        Value = finalyAmount
                    };
                    SqlParameter code = new SqlParameter
                    {
                        ParameterName = "@DigitalCurrencyCode",
                        Value = digitalCode
                    };
                    SqlParameter id = new SqlParameter
                    {
                        ParameterName = "@OperationId",
                        Value = newId
                    };

                    insertAmountCommand.Parameters.Add(amount);
                    insertAmountCommand.Parameters.Add(code);
                    insertAmountCommand.Parameters.Add(id);

                    insertAmountCommand.ExecuteNonQuery();

                    sqlConnection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}