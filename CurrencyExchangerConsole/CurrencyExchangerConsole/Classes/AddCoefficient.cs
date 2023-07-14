using System;
using System.Data.SqlClient;

namespace CurrencyExchangerConsole.Classes
{
    public class AddCoefficient
    {
        private int GetDigitalCurrencyCode(string AlphabeticCurrencyCode)
        {
            string connectionString = "Data Source=.\\SQLEXPRESS;Database=CurrencyExchanger_db;Trusted_Connection=True;";
            string getCode = "SELECT Digital_Currency_Code FROM Currencies WHERE Alphabetic_Currency_Code = @AlphabeticCurrencyCode";

            SqlConnection sqlConnection = new SqlConnection(connectionString);

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
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                sqlConnection.Close();
                return (0);
            }
        }

        private string GetOperationType(string OperationName)
        {
            string connectionString = "Data Source=.\\SQLEXPRESS;Database=CurrencyExchanger_db;Trusted_Connection=True;";
            string getType = "SELECT Operation_Type FROM Operations_Type WHERE Operation_Name = @OperationName";

            SqlConnection sqlConnection = new SqlConnection(connectionString);

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

        public void AddCoefficientFunction(string Coefficient, string AlphabeticCurrencyCode, string OperationName, DateTime DateOfTheStartAction)
        {
            string connectionString = "Data Source=.\\SQLEXPRESS;Database=CurrencyExchanger_db;Trusted_Connection=True;";
            string addCoefficientQuery = "INSERT INTO Coefficients VALUES(@Coefficient, @DigitalCurrencyCode, @OperationType, @DateOfIssue, @DateOfTheStartAction);";

            SqlConnection sqlConnection = new SqlConnection(connectionString);

            try
            {
                sqlConnection.Open();

                int digitalCode = GetDigitalCurrencyCode(AlphabeticCurrencyCode);
                string operationTypeStr = GetOperationType(OperationName);

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
                SqlParameter dateOfTheStartAction = new SqlParameter
                {
                    ParameterName = "@DateOfTheStartAction",
                    Value = DateOfTheStartAction
                };

                addCommand.Parameters.Add(coefficient);
                addCommand.Parameters.Add(digitalCurrencyCode);
                addCommand.Parameters.Add(operationType);
                addCommand.Parameters.Add(dateOfIssue);
                addCommand.Parameters.Add(dateOfTheStartAction);

                addCommand.ExecuteNonQuery();

                sqlConnection.Close();

                Console.WriteLine($"Coefficient {Coefficient} with parameters - {AlphabeticCurrencyCode} \\ {OperationName} has been added!\nEffective date of the coefficient = {DateOfTheStartAction}");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}