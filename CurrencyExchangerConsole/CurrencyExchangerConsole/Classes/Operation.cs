using System;
using System.Configuration;
using System.Data.SqlClient;

namespace CurrencyExchangerConsole.Classes
{
    public class Operation
    {
        private static Operation instance;

        public static Operation GetInstance()
        {
            return instance ?? (instance = new Operation());
        }

        public Operation() { }

        public int digitalCurrencyCode { get; set; }
        public DateTime dateOfIssue { get; set; }
        public string transactionAmount { get; set; }
        public string transactionDelivery { get; set; }
        public string operationType { get; set; }

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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return (null);
                }
            }
        }

        private string GetAmountValue(int DigitalCurrencyCode)
        {
            string getAmountQuery = "SELECT Bank_Amount FROM Banking_Information WHERE Digital_Currency_Code = @CurrencyCode;";

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

                    getAmountCommand.Parameters.Add(code);

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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void Refill(string AmountValue, string AlphabeticCurrencyCode)
        {
            string updateAmountQuery = "UPDATE Banking_Information SET Bank_Amount = @BankAmount WHERE Digital_Currency_Code = @DigitalCurrencyCode;";

            SqlTransaction transaction = null;

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CurrencyExchanger_db"].ConnectionString))
            {
                try
                {
                    sqlConnection.Open();
                    transaction = sqlConnection.BeginTransaction();

                    AddOperation(AmountValue, AlphabeticCurrencyCode);

                    int digitalCode = GetDigitalCurrencyCode(AlphabeticCurrencyCode);

                    string amountValue = GetAmountValue(digitalCode);

                    int firstValue = int.Parse(amountValue);
                    int secondValue = int.Parse(AmountValue);
                    int finallyValue = firstValue + secondValue;
                    string finalyAmount = finallyValue.ToString();

                    SqlCommand insertAmountCommand = new SqlCommand(updateAmountQuery, sqlConnection);
                    SqlParameter amount = new SqlParameter
                    {
                        ParameterName = "@BankAmount",
                        Value = finalyAmount
                    };
                    SqlParameter currencyCode = new SqlParameter
                    {
                        ParameterName = "@DigitalCurrencyCode",
                        Value = digitalCode
                    };

                    insertAmountCommand.Parameters.Add(amount);
                    insertAmountCommand.Parameters.Add(currencyCode);

                    insertAmountCommand.Transaction = transaction;

                    insertAmountCommand.ExecuteNonQuery();

                    transaction.Commit();

                    Console.WriteLine($"The replenishment operation for the amount - {AmountValue} {AlphabeticCurrencyCode} was successfully carried out!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    transaction.Rollback();
                }
                finally
                {
                    sqlConnection.Close();
                }
            }
        }

        private string GetRatePurchase(int DigitalCode)
        {
            string getRateQuery = "SELECT Rate_Purchase FROM Rate_Purchase WHERE Date_Of_The_Start_Action = (SELECT MAX(Date_Of_The_Start_Action) FROM Rate_Purchase WHERE Digital_Currency_Code = @DigitalCode AND Date_Of_The_Start_Action <= (SELECT SYSDATETIME()));";

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CurrencyExchanger_db"].ConnectionString))
            {
                try
                {
                    sqlConnection.Open();

                    SqlCommand command = new SqlCommand(getRateQuery, sqlConnection);
                    SqlParameter code = new SqlParameter
                    {
                        ParameterName = "@DigitalCode",
                        Value = DigitalCode
                    };

                    command.Parameters.Add(code);

                    object codeValue = command.ExecuteScalar();

                    string codeValueStr = codeValue.ToString();

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

        private string GetRateConversion(int DigitalCode, int SecondDigitalCode)
        {
            string getRateQuery = "SELECT Rate_Conversion FROM Rate_Of_Conversion WHERE Date_Of_The_Start_Action = (SELECT MAX(Date_Of_The_Start_Action) FROM Rate_Of_Conversion WHERE Digital_Currency_Code = @DigitalCode AND Second_Digital_Currency_Code = @SecondDigitalCode AND Date_Of_The_Start_Action <= (SELECT SYSDATETIME()));";

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CurrencyExchanger_db"].ConnectionString))
            {
                try
                {
                    sqlConnection.Open();

                    SqlCommand command = new SqlCommand(getRateQuery, sqlConnection);
                    SqlParameter code = new SqlParameter
                    {
                        ParameterName = "@DigitalCode",
                        Value = DigitalCode
                    };
                    SqlParameter secondCode = new SqlParameter
                    {
                        ParameterName = "@SecondDigitalCode",
                        Value = SecondDigitalCode
                    };

                    command.Parameters.Add(code);
                    command.Parameters.Add(secondCode);

                    object codeValue = command.ExecuteScalar();

                    string codeValueStr = codeValue.ToString();

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

        private string GetCoefficientConversion(int DigitalCode, int SecondDigitalCode)
        {
            string getRateQuery = "SELECT Coefficient FROM Coefficients WHERE Digital_Currency_Code = @FirstCode AND Second_Digital_Currency_Code = @SecondCode AND Coefficient_Active = '1';";

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CurrencyExchanger_db"].ConnectionString))
            {
                try
                {
                    sqlConnection.Open();

                    SqlCommand command = new SqlCommand(getRateQuery, sqlConnection);
                    SqlParameter code = new SqlParameter
                    {
                        ParameterName = "@FirstCode",
                        Value = DigitalCode
                    };
                    SqlParameter secondCode = new SqlParameter
                    {
                        ParameterName = "@SecondCode",
                        Value = SecondDigitalCode
                    };

                    command.Parameters.Add(code);
                    command.Parameters.Add(secondCode);

                    object codeValue = command.ExecuteScalar();

                    string codeValueStr = codeValue.ToString();

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

        private void AddOperation(string AmountValue, string DeliveryValue, string AlphabeticCurrencyCode)
        {
            string addOperationQuery = "INSERT INTO Operations VALUES(@OperatorId, @DigitalCurrencyCode, @DateOfIssue, @TransactionAmount, @TransactionDelivery, @OperationType);";

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CurrencyExchanger_db"].ConnectionString))
            {
                try
                {
                    sqlConnection.Open();

                    int id = GetOperatorId("TestC");
                    int digitalCode = GetDigitalCurrencyCode(AlphabeticCurrencyCode);
                    string type = GetOperationType("Conversion");

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
                        Value = DeliveryValue
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void Conversion(string AmountValue, string FirstAlphabeticCurrencyCode, string SecondAlphabeticCurrencyCode)
        {
            string updateOperationQuery = "UPDATE Banking_Information SET Bank_Amount = @BankAmount WHERE Digital_Currency_Code = @DigitalCurrencyCode;";

            SqlTransaction transaction = null;

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CurrencyExchanger_db"].ConnectionString))
            {
                try
                {
                    sqlConnection.Open();
                    transaction = sqlConnection.BeginTransaction();

                    int digitalCode1 = GetDigitalCurrencyCode(FirstAlphabeticCurrencyCode);
                    int digitalCode2 = GetDigitalCurrencyCode(SecondAlphabeticCurrencyCode);

                    string rateStr = GetRatePurchase(digitalCode1);
                    string newRateStr = rateStr.Replace('.', ',');
                    double newRateDb = Convert.ToDouble(newRateStr);
                    double amountDb = Convert.ToDouble(AmountValue);
                    double amountBYN = amountDb * newRateDb;

                    string rateConversion = GetRateConversion(digitalCode1, digitalCode2);
                    string newRateConversion = rateStr.Replace('.', ',');
                    double newRateConversionDb = Convert.ToDouble(newRateConversion);

                    string coefficient = GetCoefficientConversion(digitalCode1, digitalCode2);
                    string newCoefficientConversion = coefficient.Replace('.', ',');
                    double newCoefficientConversionDb = Convert.ToDouble(newCoefficientConversion);

                    double finallyValue = amountBYN * newRateConversionDb * newCoefficientConversionDb;

                    double resultBEFORE = Math.Truncate(finallyValue);  //integer part of a number
                    double resultAFTER = finallyValue - resultBEFORE;   //fractional part of a number
                    string resultAFTERstr = resultAFTER.ToString();

                    string amountValue1 = GetAmountValue(933);
                    string newAmountValue1 = amountValue1.Replace('.', ',');
                    double newAmountValue1Db = Convert.ToDouble(newAmountValue1);
                    double finallyNewAmountValue1Db = newAmountValue1Db + resultBEFORE;

                    string amountValue2 = GetAmountValue(digitalCode2);
                    string newAmountValue2 = amountValue2.Replace('.', ',');
                    double newAmountValue2Db = Convert.ToDouble(newAmountValue2);
                    double finallyNewAmountValue2Db = newAmountValue2Db - amountDb;

                    if (finallyValue > newAmountValue1Db)
                    {
                        Console.WriteLine("There are not enough funds in the exchanger!");
                    }
                    else
                    {
                        AddOperation(AmountValue, resultAFTERstr, SecondAlphabeticCurrencyCode);

                        SqlCommand command = new SqlCommand(updateOperationQuery, sqlConnection);
                        SqlParameter amount = new SqlParameter
                        {
                            ParameterName = "@BankAmount",
                            Value = finallyNewAmountValue1Db
                        };
                        SqlParameter code = new SqlParameter
                        {
                            ParameterName = "@DigitalCurrencyCode",
                            Value = 933
                        };

                        command.Parameters.Add(amount);
                        command.Parameters.Add(code);

                        SqlCommand command1 = new SqlCommand(updateOperationQuery, sqlConnection);
                        SqlParameter amount1 = new SqlParameter
                        {
                            ParameterName = "@BankAmount",
                            Value = finallyNewAmountValue2Db
                        };
                        SqlParameter code1 = new SqlParameter
                        {
                            ParameterName = "@DigitalCurrencyCode",
                            Value = digitalCode2
                        };

                        command1.Parameters.Add(amount1);
                        command1.Parameters.Add(code1);

                        command.Transaction = transaction;
                        command1.Transaction = transaction;

                        command.ExecuteNonQuery();
                        command1.ExecuteNonQuery();

                        transaction.Commit();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    transaction.Rollback();
                }
                finally
                {
                    sqlConnection.Close();
                }
            }
        }

        private string GetCoefficientSale(int DigitalCode)
        {
            string getRateQuery = "SELECT Coefficient FROM Coefficients WHERE Digital_Currency_Code = @Code AND Operation_Type = 'C' AND Coefficient_Active = '1';";

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CurrencyExchanger_db"].ConnectionString))
            {
                try
                {
                    sqlConnection.Open();

                    SqlCommand command = new SqlCommand(getRateQuery, sqlConnection);
                    SqlParameter code = new SqlParameter
                    {
                        ParameterName = "@Code",
                        Value = DigitalCode
                    };

                    command.Parameters.Add(code);

                    object codeValue = command.ExecuteScalar();

                    string codeValueStr = codeValue.ToString();

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

        private string GetRateSale(int DigitalCode)
        {
            string getRateQuery = "SELECT Rate_Sale FROM Rate_Sale WHERE Date_Of_The_Start_Action = (SELECT MAX(Date_Of_The_Start_Action) FROM Rate_Sale WHERE Digital_Currency_Code = @DigitalCode AND Date_Of_The_Start_Action <= (SELECT SYSDATETIME()));";

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CurrencyExchanger_db"].ConnectionString))
            {
                try
                {
                    sqlConnection.Open();

                    SqlCommand command = new SqlCommand(getRateQuery, sqlConnection);
                    SqlParameter code = new SqlParameter
                    {
                        ParameterName = "@DigitalCode",
                        Value = DigitalCode
                    };

                    command.Parameters.Add(code);

                    object codeValue = command.ExecuteScalar();

                    string codeValueStr = codeValue.ToString();

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

        private void AddOperationSale(string AmountValue, string DeliveryValue, string AlphabeticCurrencyCode)
        {
            string addOperationQuery = "INSERT INTO Operations VALUES(@OperatorId, @DigitalCurrencyCode, @DateOfIssue, @TransactionAmount, @TransactionDelivery, @OperationType);";

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CurrencyExchanger_db"].ConnectionString))
            {
                try
                {
                    sqlConnection.Open();

                    int id = GetOperatorId("TestC");
                    int digitalCode = GetDigitalCurrencyCode(AlphabeticCurrencyCode);
                    string type = GetOperationType("Sale");

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
                        Value = DeliveryValue
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void Sale(string AmountValue, string AlphabeticCurrencyCode)
        {
            string updateOperationQuery = "UPDATE Banking_Information SET Bank_Amount = @BankAmount WHERE Digital_Currency_Code = @DigitalCurrencyCode;";

            SqlTransaction transaction = null;

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CurrencyExchanger_db"].ConnectionString))
            {
                try
                {
                    sqlConnection.Open();
                    transaction = sqlConnection.BeginTransaction();

                    int digitalCode = GetDigitalCurrencyCode(AlphabeticCurrencyCode);
                    string type = GetOperationType("Sale");

                    string rateStr = GetRateSale(digitalCode);
                    string newRateStr = rateStr.Replace('.', ',');
                    double newRateDb = Convert.ToDouble(newRateStr);

                    double amountDb = Convert.ToDouble(AmountValue);
                    double amount = amountDb % newRateDb;

                    string coefficient = GetCoefficientSale(digitalCode);
                    string newCoefficientSale = coefficient.Replace('.', ',');
                    double newCoefficientSaleDb = Convert.ToDouble(newCoefficientSale);

                    double finallyValue = amount * newCoefficientSaleDb;

                    double resultBEFORE = Math.Truncate(finallyValue);  //integer part of a number
                    double resultAFTER = finallyValue - resultBEFORE;   //fractional part of a number
                    string resultAFTERstr = resultAFTER.ToString();

                    string amountValue1 = GetAmountValue(933);
                    string newAmountValue1 = amountValue1.Replace('.', ',');
                    double newAmountValue1Db = Convert.ToDouble(newAmountValue1);
                    double finallyNewAmountValue1Db = newAmountValue1Db + amountDb;

                    string amountValue2 = GetAmountValue(digitalCode);
                    string newAmountValue2 = amountValue2.Replace('.', ',');
                    double newAmountValue2Db = Convert.ToDouble(newAmountValue2);
                    double finallyNewAmountValue2Db = newAmountValue2Db - resultBEFORE;

                    if (finallyValue > newAmountValue2Db)
                    {
                        Console.WriteLine("There are not enough funds in the exchanger!");
                    }
                    else
                    {
                        AddOperationSale(AmountValue, resultAFTERstr, AlphabeticCurrencyCode);

                        SqlCommand command = new SqlCommand(updateOperationQuery, sqlConnection);
                        SqlParameter amountAdd = new SqlParameter
                        {
                            ParameterName = "@BankAmount",
                            Value = finallyNewAmountValue1Db
                        };
                        SqlParameter code = new SqlParameter
                        {
                            ParameterName = "@DigitalCurrencyCode",
                            Value = 933
                        };

                        command.Parameters.Add(amountAdd);
                        command.Parameters.Add(code);

                        SqlCommand command1 = new SqlCommand(updateOperationQuery, sqlConnection);
                        SqlParameter amountRelease = new SqlParameter
                        {
                            ParameterName = "@BankAmount",
                            Value = finallyNewAmountValue2Db
                        };
                        SqlParameter code1 = new SqlParameter
                        {
                            ParameterName = "@DigitalCurrencyCode",
                            Value = digitalCode
                        };

                        command1.Parameters.Add(amountRelease);
                        command1.Parameters.Add(code1);

                        command.Transaction = transaction;
                        command1.Transaction = transaction;

                        command.ExecuteNonQuery();
                        command1.ExecuteNonQuery();

                        transaction.Commit();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    transaction.Rollback();
                }
                finally
                {
                    sqlConnection.Close();
                }
            }
        }

        private string GetCoefficientPurchase(int DigitalCode)
        {
            string getRateQuery = "SELECT Coefficient FROM Coefficients WHERE Digital_Currency_Code = @Code AND Operation_Type = 'B' AND Coefficient_Active = '1';";

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CurrencyExchanger_db"].ConnectionString))
            {
                try
                {
                    sqlConnection.Open();

                    SqlCommand command = new SqlCommand(getRateQuery, sqlConnection);
                    SqlParameter code = new SqlParameter
                    {
                        ParameterName = "@Code",
                        Value = DigitalCode
                    };

                    command.Parameters.Add(code);

                    object codeValue = command.ExecuteScalar();

                    string codeValueStr = codeValue.ToString();

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

        private void AddOperationPurchase(string AmountValue, string DeliveryValue, string AlphabeticCurrencyCode)
        {
            string addOperationQuery = "INSERT INTO Operations VALUES(@OperatorId, @DigitalCurrencyCode, @DateOfIssue, @TransactionAmount, @TransactionDelivery, @OperationType);";

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CurrencyExchanger_db"].ConnectionString))
            {
                try
                {
                    sqlConnection.Open();

                    int id = GetOperatorId("TestC");
                    int digitalCode = GetDigitalCurrencyCode(AlphabeticCurrencyCode);
                    string type = GetOperationType("Purchase");

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
                        Value = DeliveryValue
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void Purchase(string AmountValue, string AlphabeticCurrencyCode)
        {
            string updateOperationQuery = "UPDATE Banking_Information SET Bank_Amount = @BankAmount WHERE Digital_Currency_Code = @DigitalCurrencyCode;";

            SqlTransaction transaction = null;

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CurrencyExchanger_db"].ConnectionString))
            {
                try
                {
                    sqlConnection.Open();
                    transaction = sqlConnection.BeginTransaction();

                    int digitalCode = GetDigitalCurrencyCode(AlphabeticCurrencyCode);
                    string type = GetOperationType("Purchase");

                    string rateStr = GetRatePurchase(digitalCode);
                    string newRateStr = rateStr.Replace('.', ',');
                    double newRateDb = Convert.ToDouble(newRateStr);

                    double amountDb = Convert.ToDouble(AmountValue);
                    double amount = amountDb * newRateDb;

                    string coefficient = GetCoefficientPurchase(digitalCode);
                    string newCoefficientSale = coefficient.Replace('.', ',');
                    double newCoefficientSaleDb = Convert.ToDouble(newCoefficientSale);

                    double finallyValue = amount * newCoefficientSaleDb;

                    double resultBEFORE = Math.Truncate(finallyValue);  //integer part of a number
                    double resultAFTER = finallyValue - resultBEFORE;   //fractional part of a number
                    string resultAFTERstr = resultAFTER.ToString();

                    string amountValue1 = GetAmountValue(933);
                    string newAmountValue1 = amountValue1.Replace('.', ',');
                    double newAmountValue1Db = Convert.ToDouble(newAmountValue1);
                    double finallyNewAmountValue1Db = newAmountValue1Db + amountDb;

                    string amountValue2 = GetAmountValue(digitalCode);
                    string newAmountValue2 = amountValue2.Replace('.', ',');
                    double newAmountValue2Db = Convert.ToDouble(newAmountValue2);
                    double finallyNewAmountValue2Db = newAmountValue2Db - resultBEFORE;

                    if (finallyValue > newAmountValue2Db)
                    {
                        Console.WriteLine("There are not enough funds in the exchanger!");
                    }
                    else
                    {
                        AddOperationPurchase(AmountValue, resultAFTERstr, AlphabeticCurrencyCode);

                        SqlCommand command = new SqlCommand(updateOperationQuery, sqlConnection);
                        SqlParameter amountAdd = new SqlParameter
                        {
                            ParameterName = "@BankAmount",
                            Value = finallyNewAmountValue1Db
                        };
                        SqlParameter code = new SqlParameter
                        {
                            ParameterName = "@DigitalCurrencyCode",
                            Value = 933
                        };

                        command.Parameters.Add(amountAdd);
                        command.Parameters.Add(code);

                        SqlCommand command1 = new SqlCommand(updateOperationQuery, sqlConnection);
                        SqlParameter amountRelease = new SqlParameter
                        {
                            ParameterName = "@BankAmount",
                            Value = finallyNewAmountValue2Db
                        };
                        SqlParameter code1 = new SqlParameter
                        {
                            ParameterName = "@DigitalCurrencyCode",
                            Value = digitalCode
                        };

                        command1.Parameters.Add(amountRelease);
                        command1.Parameters.Add(code1);

                        command.Transaction = transaction;
                        command1.Transaction = transaction;

                        command.ExecuteNonQuery();
                        command1.ExecuteNonQuery();

                        transaction.Commit();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    transaction.Rollback();
                }
                finally
                {
                    sqlConnection.Close();
                }
            }
        }
    }
}