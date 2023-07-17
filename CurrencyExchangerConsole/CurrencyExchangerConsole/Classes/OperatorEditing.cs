using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace CurrencyExchangerConsole.Classes
{
    public class OperatorEditing
    {
        private Operator op;

        private int GetIdFunction(string OperatorName)
        {
            string getOperatorId = "SELECT Operator_Id FROM Operators WHERE Operator_Name = @OperatorName;";

            op = Operator.GetInstance();

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CurrencyExchanger_db"].ConnectionString))
            {
                try
                {
                    sqlConnection.Open();

                    SqlCommand commandGetId = new SqlCommand(getOperatorId, sqlConnection);
                    SqlParameter name = new SqlParameter
                    {
                        ParameterName = "@OperatorName",
                        Value = OperatorName
                    };

                    commandGetId.Parameters.Add(name);

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

        private bool CheckOperatorName(SqlConnection connection, string operatorName)
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

        public void OperatorEditingFunction(string OperatorName, string NewOperatorName, string NewOperatorPassword, string NewOperatorType)
        {
            string editingQuery = "UPDATE Operators SET Operator_Name = @OperatorName, Operator_Password = @OperatorPassword, Operator_Type = @OperatorType WHERE Operator_Id = @OperatorId";

            op = Operator.GetInstance();

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CurrencyExchanger_db"].ConnectionString))
            {
                try
                {
                    sqlConnection.Open();

                    if (CheckOperatorName(sqlConnection, NewOperatorName))
                    {
                        Console.WriteLine("Such a name already exists!");
                    }
                    else
                    {
                        int baseOperatorId = GetIdFunction(OperatorName);

                        SqlCommand commandEdit = new SqlCommand(editingQuery, sqlConnection);
                        SqlParameter operatorName = new SqlParameter
                        {
                            ParameterName = "@OperatorName",
                            Value = NewOperatorName
                        };
                        SqlParameter operatorType = new SqlParameter
                        {
                            ParameterName = "@OperatorType",
                            Value = NewOperatorType
                        };
                        SqlParameter operatorId = new SqlParameter
                        {
                            ParameterName = "@OperatorId",
                            Value = baseOperatorId
                        };

                        commandEdit.Parameters.Add(operatorName);
                        commandEdit.Parameters.Add("@OperatorPassword", SqlDbType.VarChar).Value = PasswordHasher.GetHesh(NewOperatorPassword);
                        commandEdit.Parameters.Add(operatorType);
                        commandEdit.Parameters.Add(operatorId);

                        commandEdit.ExecuteNonQuery();

                        sqlConnection.Close();

                        Console.WriteLine($"Old operator info : nema={OperatorName}\nNew operator info : name={NewOperatorName}");
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