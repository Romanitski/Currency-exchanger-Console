using System;
using System.Configuration;
using System.Data.SqlClient;

namespace CurrencyExchangerConsole.Classes
{
    public class Authorization
    {
        private Operator op;
        bool operatorExist = false;

        public void AuthorizationFunction(string OperatorName, string OperatorPassword)
        {
            string authorizationQuery = "SELECT * FROM Operators WHERE Operator_Name = @OperatorName AND Operator_Password = @OperatorPassword;";

            op = Operator.GetInstance();

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CurrencyExchanger_db"].ConnectionString))
            {
                try
                {
                    sqlConnection.Open();

                    SqlCommand command = new SqlCommand(authorizationQuery, sqlConnection);
                    SqlParameter login = new SqlParameter
                    {
                        ParameterName = "@OperatorName",
                        Value = OperatorName
                    };
                    SqlParameter password = new SqlParameter
                    {
                        ParameterName = "@OperatorPassword",
                        Value = PasswordHasher.GetHesh(OperatorPassword)
                    };

                    command.Parameters.Add(login);
                    command.Parameters.Add(password);

                    var currentOperator = command.ExecuteReader();
                    if (currentOperator.HasRows)
                    {
                        operatorExist = true;
                        while (currentOperator.Read())
                        {
                            op.operatorId = currentOperator.GetInt32(0);
                            op.operatorName = currentOperator.GetString(1);
                            op.operatorPassword = currentOperator.GetString(2);
                            op.operatorType = currentOperator.GetString(3);
                            op.operatorActive = currentOperator.GetBoolean(4);
                        }
                    }
                    currentOperator.Close();

                    sqlConnection.Close();

                    if (operatorExist && op.operatorActive != false)
                    {
                        if (op.operatorType == "A") Console.WriteLine($"{op.operatorName} = Administrator!\n");
                        else if (op.operatorType == "B") Console.WriteLine($"{op.operatorName} = Course operator!\n");
                        else Console.WriteLine($"{op.operatorName} = Operator-cashier!\n");
                    }
                    else
                    {
                        Console.WriteLine("There is no such user!");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}