using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace CurrencyExchangerConsole.Classes
{
    public class Operator
    {
        private static Operator instance;

        public static Operator GetInstance()
        {
            return instance ?? (instance = new Operator());
        }

        public Operator() { }

        public Operator(string operatorName, string operatorPassword)
        {
            this.operatorName = operatorName;
            this.operatorPassword = operatorPassword;
        }

        public Operator(string operatorName, string operatorPassword, string operatorType)
        {
            this.operatorName = operatorName;
            this.operatorPassword = operatorPassword;
            this.operatorType = operatorType;
        }

        public Operator(string operatorName, string operatorPassword, string operatorType, bool operatorActive)
        {
            this.operatorName = operatorName;
            this.operatorPassword = operatorPassword;
            this.operatorType = operatorType;
            this.operatorActive = operatorActive;
        }

        public Operator(int operatorId, string operatorName, string operatorPassword, string operatorType, bool operatorActive)
        {
            this.operatorId = operatorId;
            this.operatorName = operatorName;
            this.operatorPassword = operatorPassword;
            this.operatorType = operatorType;
            this.operatorActive = operatorActive;
        }

        public int operatorId { get; set; }
        public string operatorName { get; set; }
        public string operatorPassword { get; set; }
        public string operatorType { get; set; }
        public bool operatorActive { get; set; }

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

        public void Registration(string OperatorName, string OperatorPassword, string OperatorType)
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
                        sqlCommand.Parameters.Add("@Operator_Active", SqlDbType.Bit).Value = true;

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

        private Operator op;
        bool operatorExist = false;

        public void Authorization(string OperatorName, string OperatorPassword)
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

        private int GetId(string OperatorName)
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

        public void OperatorActiveEditing(string OperatorName, bool NewOperatorActive)
        {
            string editingQuery = "UPDATE Operators SET Operator_Active = @OperatorActive WHERE Operator_Id = @OperatorId;";

            op = Operator.GetInstance();

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CurrencyExchanger_db"].ConnectionString))
            {
                try
                {
                    sqlConnection.Open();

                    int baseOperatorId = GetId(OperatorName);

                    SqlCommand commandEditActive = new SqlCommand(editingQuery, sqlConnection);
                    SqlParameter operatorActive = new SqlParameter
                    {
                        ParameterName = "@OperatorActive",
                        Value = NewOperatorActive
                    };
                    SqlParameter operatorId = new SqlParameter
                    {
                        ParameterName = "@OperatorId",
                        Value = baseOperatorId
                    };

                    commandEditActive.Parameters.Add(operatorActive);
                    commandEditActive.Parameters.Add(operatorId);

                    commandEditActive.ExecuteNonQuery();

                    sqlConnection.Close();

                    Console.WriteLine($"Operator - {OperatorName} is active = {NewOperatorActive}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void OperatorAllEditing(string OperatorName, string NewOperatorName, string NewOperatorPassword, string NewOperatorType)
        {
            string editingQuery = "UPDATE Operators SET Operator_Name = @OperatorName, Operator_Password = @OperatorPassword, Operator_Type = @OperatorType WHERE Operator_Id = @OperatorId;";

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
                        int baseOperatorId = GetId(OperatorName);

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

                        Console.WriteLine($"Old operator info : nema = {OperatorName}\nNew operator info : name = {NewOperatorName}");
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