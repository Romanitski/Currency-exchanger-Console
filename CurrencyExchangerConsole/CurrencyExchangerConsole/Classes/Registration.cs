using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace CurrencyExchangerConsole.Classes
{
    public class Registration
    {
        public bool OperatorExists(SqlConnection connection, string operatorName)
        {
            string checkOperatorQuery = "SELECT * FROM Operators WHERE Operator_Name = @OperatorName";
            SqlCommand command = new SqlCommand(checkOperatorQuery, connection);
            SqlParameter operatorNameParameter = new SqlParameter
            {
                ParameterName = "@OperatorName",
                Value = operatorName
            };
            command.Parameters.Add(operatorNameParameter);
            var currentOperator = command.ExecuteReader();
            if (currentOperator.HasRows)
            {
                currentOperator.Close();
                return true;
            }
            else
            {
                currentOperator.Close();
                return false;
            }
        }

        public void RegistrationFunction(string OperatorName, string OperatorPassword, string OperatorType, bool OperatorActive)
        {
            string registrationProcedure = "RegistrationProcedure";

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CurrencyExchanger_db"].ConnectionString))
            {
                try
                {
                    sqlConnection.Open();

                    if (OperatorExists(sqlConnection, OperatorName))
                    {
                        Console.WriteLine("Such a name already exists!");
                    }
                    else
                    {
                        DataTable dataTable = new DataTable();
                        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                        SqlCommand sqlCommand = new SqlCommand(registrationProcedure, sqlConnection);
                        sqlCommand.CommandType = CommandType.StoredProcedure;

                        sqlCommand.Parameters.Add("@Operator_Name", SqlDbType.VarChar).Value = OperatorName;
                        sqlCommand.Parameters.Add("@Operator_Password", SqlDbType.VarChar).Value = PasswordHasher.GetHesh(OperatorPassword);
                        sqlCommand.Parameters.Add("@Operator_Type", SqlDbType.VarChar).Value = OperatorType;
                        sqlCommand.Parameters.Add("@Operator_Active", SqlDbType.Bit).Value = OperatorActive;

                        sqlDataAdapter.SelectCommand = sqlCommand;
                        sqlDataAdapter.Fill(dataTable);

                        Console.WriteLine("Registration sucsessful!");
                    }

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