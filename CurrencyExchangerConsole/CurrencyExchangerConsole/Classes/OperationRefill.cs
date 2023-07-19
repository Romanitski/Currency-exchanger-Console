using System;
using System.Configuration;
using System.Data.SqlClient;

namespace CurrencyExchangerConsole.Classes
{
    public class OperationRefill
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
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return (null);
                }
            }
        }

        private void AddOperation(string AmountValue, string AlphabeticCurrencyCode)
        {
            string addOperationQuery = "INSERT INTO Operations VALUES(@OperatorId, @DigitalCurrencyCode, @DateOfIssue, @TransactionAmount, @TransactionDelivery, @OperationType);";

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CurrencyExchanger_db"].ConnectionString))
            {
                try
                {
                    sqlConnection.Open();

                    int id = GetOperatorId("TestC");
                    int digitalCode = GetDigitalCurrencyCode(AlphabeticCurrencyCode);
                    string type = GetOperationType("Refill");
                    
                    SqlCommand addOperationCommand = new SqlCommand(addOperationQuery, sqlConnection);
                    SqlParameter operatorId = new SqlParameter
                    {
                        ParameterName = "@OperatorId",
                        Value = id
                    };
                    SqlParameter currencyCode = new SqlParameter
                    {
                        ParameterName = "@DigitalCurrencyCode",
                        Value = digitalCode
                    };
                    SqlParameter dateOfIssue = new SqlParameter
                    {
                        ParameterName = "@DateOfIssue",
                        Value = DateTime.Now
                    };
                    SqlParameter transactionAmount = new SqlParameter
                    {
                        ParameterName = "@TransactionAmount",
                        Value = AmountValue
                    };
                    SqlParameter transactionDelivery = new SqlParameter
                    {
                        ParameterName = "@TransactionDelivery",
                        Value = "0"
                    };
                    SqlParameter operationType = new SqlParameter
                    {
                        ParameterName = "@OperationType",
                        Value = type
                    };

                    addOperationCommand.Parameters.Add(operatorId);
                    addOperationCommand.Parameters.Add(currencyCode);
                    addOperationCommand.Parameters.Add(dateOfIssue);
                    addOperationCommand.Parameters.Add(transactionAmount);
                    addOperationCommand.Parameters.Add(transactionDelivery);
                    addOperationCommand.Parameters.Add(operationType);

                    addOperationCommand.ExecuteNonQuery();

                    sqlConnection.Close();

                    Console.WriteLine($"The operation was successfully registered!");
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
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

                    string type = GetOperationType("Refill");

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

        private int GetOperationId()
        {
            string getIdQuery = "SELECT MAX(Operation_Id) FROM Operations WHERE Operation_Type = @OperationType;";

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CurrencyExchanger_db"].ConnectionString))
            {
                try
                {
                    sqlConnection.Open();

                    string type = GetOperationType("Refill");

                    SqlCommand getIdCommand = new SqlCommand(getIdQuery, sqlConnection);
                    SqlParameter operationType = new SqlParameter
                    {
                        ParameterName = "@OperationType",
                        Value = type
                    };

                    getIdCommand.Parameters.Add(operationType);

                    object idValue = getIdCommand.ExecuteScalar();

                    int idValueInt = Convert.ToInt32((idValue));

                    sqlConnection.Close();
                    return (idValueInt);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return (0);
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
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return (null);
                }
            }

        }

        public void RefillFinction(string AmountValue, string AlphabeticCurrencyCode)
        {
            string insertAmountQuery = "INSERT INTO Banking_Information VALUES(@BankAmount, @OperationId, @DigitalCurrencyCode);";

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CurrencyExchanger_db"].ConnectionString))
            {
                try
                {
                    sqlConnection.Open();

                    int digitalCode = GetDigitalCurrencyCode(AlphabeticCurrencyCode);
                    int newId = GetNewOperationId(digitalCode);

                    string amountValue = GetAmountValue(digitalCode, newId);

                    int firstValue = int.Parse(amountValue);
                    int secondValue = int.Parse(AmountValue);
                    int finallyValue = firstValue + secondValue;
                    string finalyAmount = finallyValue.ToString();

                    AddOperation(AmountValue, AlphabeticCurrencyCode);
                    int id = GetOperationId();

                    SqlCommand insertAmountCommand = new SqlCommand(insertAmountQuery, sqlConnection);
                    SqlParameter amount = new SqlParameter
                    {
                        ParameterName = "@BankAmount",
                        Value = finalyAmount
                    };
                    SqlParameter operationId = new SqlParameter
                    {
                        ParameterName = "@OperationId",
                        Value = id
                    };
                    SqlParameter currencyCode = new SqlParameter
                    {
                        ParameterName = "@DigitalCurrencyCode",
                        Value = digitalCode
                    };

                    insertAmountCommand.Parameters.Add(amount);
                    insertAmountCommand.Parameters.Add(operationId);
                    insertAmountCommand.Parameters.Add(currencyCode);

                    insertAmountCommand.ExecuteNonQuery();

                    sqlConnection.Close();

                    Console.WriteLine($"The replenishment operation for the amount - {AmountValue} {AlphabeticCurrencyCode} was successfully carried out");
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}