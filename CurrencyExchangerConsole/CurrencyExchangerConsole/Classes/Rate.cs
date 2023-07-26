using System;
using System.Configuration;
using System.Data.SqlClient;

namespace CurrencyExchangerConsole.Classes
{
    public class Rate
    {
        private static Rate instance;

        public static Rate GetInstance()
        {
            return instance ?? (instance = new Rate());
        }

        public Rate() { }

        public int digitalCurrencyCode { get; set; }
        public int secondDigitalCurrencyCode { get; set; }
        public string ratePurchase { get; set; }
        public string rateSale { get; set; }
        public string rateConversion { get; set; }
        public DateTime DateOfIssue { get; set; }
        public DateTime DateOfTheStartAction { get; set; }

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

        public void AddNewRateSale(string AlphabeticCurrencyCode, string RateSale, DateTime DateOfTheStartAction)
        {
            string addRateQuery = "INSERT INTO Rate_Sale VALUES(@OperatorId, @DigitalCurrencyCode, @RateSale, @CoefficientId, @DateOfIssue, @DateOfTheStartAction);";

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CurrencyExchanger_db"].ConnectionString))
            {
                try
                {
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

        public void AddNewRatePurchase(string AlphabeticCurrencyCode, string RatePurchase, DateTime DateOfTheStartAction)
        {
            string addRateQuery = "INSERT INTO Rate_Purchase VALUES(@OperatorId, @DigitalCurrencyCode, @RatePurchase, @CoefficientId, @DateOfIssue, @DateOfTheStartAction);";

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CurrencyExchanger_db"].ConnectionString))
            {
                try
                {
                    sqlConnection.Open();

                    int digitalCode = GetDigitalCurrencyCode(AlphabeticCurrencyCode);
                    int id = GetOperatorId("TestB");
                    string coefficientValue = GetCoefficient(AlphabeticCurrencyCode, "B");
                    int coefficientIdValue = GetCoefficientId(AlphabeticCurrencyCode, "B", coefficientValue);

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
                    addRateCommand.Parameters.Add(ratePurchase);
                    addRateCommand.Parameters.Add(coefficient);
                    addRateCommand.Parameters.Add(dateOfIssue);
                    addRateCommand.Parameters.Add(dateOfTheStartAction);

                    addRateCommand.ExecuteNonQuery();

                    sqlConnection.Close();

                    Console.WriteLine($"The purchase rate - {RatePurchase} for the {AlphabeticCurrencyCode} currency were successfully added!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private string GetCoefficientConversion(string AlphabeticCurrencyCode, string SecondAlphabeticCurrencyCode, string OperationType)
        {
            string getCoefficient = "SELECT Coefficient FROM Coefficients WHERE Digital_Currency_Code = @DigitalCode AND Second_Digital_Currency_Code = @SecondDigitalCode AND Operation_Type = @OperationType;";

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CurrencyExchanger_db"].ConnectionString))
            {
                try
                {
                    sqlConnection.Open();

                    int digitalCode = GetDigitalCurrencyCode(AlphabeticCurrencyCode);
                    int secondDigitalCode = GetDigitalCurrencyCode(SecondAlphabeticCurrencyCode);

                    SqlCommand commandGetCoefficient = new SqlCommand(getCoefficient, sqlConnection);
                    SqlParameter digitalCodeParameter = new SqlParameter
                    {
                        ParameterName = "@DigitalCode",
                        Value = digitalCode
                    };
                    SqlParameter secondDigitalCodeParameter = new SqlParameter
                    {
                        ParameterName = "@SecondDigitalCode",
                        Value = secondDigitalCode
                    };
                    SqlParameter operationType = new SqlParameter
                    {
                        ParameterName = "@OperationType",
                        Value = OperationType
                    };

                    commandGetCoefficient.Parameters.Add(digitalCodeParameter);
                    commandGetCoefficient.Parameters.Add(secondDigitalCodeParameter);
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

        private int GetCoefficientIdConversion(string AlphabeticCurrencyCode, string SecondAlphabeticCurrencyCode, string OperationType, string coefficientValueStr)
        {
            string getCoefficientId = "SELECT CoefficientId FROM Coefficients WHERE Digital_Currency_Code = @DigitalCode AND Second_Digital_Currency_Code = @SecondDigitalCode AND Operation_Type = @OperationType AND Coefficient = @CoefficientValue;";

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CurrencyExchanger_db"].ConnectionString))
            {
                try
                {
                    sqlConnection.Open();

                    int digitalCode = GetDigitalCurrencyCode(AlphabeticCurrencyCode);
                    int secondDigitalCode = GetDigitalCurrencyCode(SecondAlphabeticCurrencyCode);

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
                    SqlParameter secondDigitalCodeParameter = new SqlParameter
                    {
                        ParameterName = "@SecondDigitalCode",
                        Value = secondDigitalCode
                    };
                    SqlParameter operationType = new SqlParameter
                    {
                        ParameterName = "@OperationType",
                        Value = OperationType
                    };

                    commandGetCoefficientId.Parameters.Add(coefficient);
                    commandGetCoefficientId.Parameters.Add(digitalCodeParameter);
                    commandGetCoefficientId.Parameters.Add(secondDigitalCodeParameter);
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

        public void AddNewRateConversion(string AlphabeticCurrencyCode, string SecondAlphabeticCurrencyCode, string RateConversion, DateTime DateOfTheStartAction)
        {
            string addRateQuery = "INSERT INTO Rate_Of_Conversion VALUES(@OperatorId, @DigitalCurrencyCode, @SecondDigitalCurrencyCode, @RateConversion, @CoefficientId, @DateOfIssue, @DateOfTheStartAction);";

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CurrencyExchanger_db"].ConnectionString))
            {
                try
                {
                    sqlConnection.Open();

                    int digitalCode = GetDigitalCurrencyCode(AlphabeticCurrencyCode);
                    int secondDigitalCode = GetDigitalCurrencyCode(SecondAlphabeticCurrencyCode);
                    int id = GetOperatorId("TestB");
                    string coefficientValue = GetCoefficientConversion(AlphabeticCurrencyCode, SecondAlphabeticCurrencyCode, "D");
                    int coefficientIdValue = GetCoefficientIdConversion(AlphabeticCurrencyCode, SecondAlphabeticCurrencyCode, "D", coefficientValue);

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
                    SqlParameter secondDigitalCodeParameter = new SqlParameter
                    {
                        ParameterName = "@SecondDigitalCurrencyCode",
                        Value = secondDigitalCode
                    };
                    SqlParameter rateConversion = new SqlParameter
                    {
                        ParameterName = "@RateConversion",
                        Value = RateConversion
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
                    addRateCommand.Parameters.Add(secondDigitalCodeParameter);
                    addRateCommand.Parameters.Add(rateConversion);
                    addRateCommand.Parameters.Add(coefficient);
                    addRateCommand.Parameters.Add(dateOfIssue);
                    addRateCommand.Parameters.Add(dateOfTheStartAction);

                    addRateCommand.ExecuteNonQuery();

                    sqlConnection.Close();

                    Console.WriteLine($"The conversion rate - {RateConversion} for the {AlphabeticCurrencyCode}/{SecondAlphabeticCurrencyCode} currency were successfully added!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}