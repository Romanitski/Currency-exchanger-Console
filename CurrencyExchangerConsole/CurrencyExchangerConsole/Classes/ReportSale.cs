using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace CurrencyExchangerConsole.Classes
{
    public class ReportSale
    {
        public void ReportFunction()
        {
            string saleQuery = "SELECT * FROM Rate_Sale";

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CurrencyExchanger_db"].ConnectionString))
            {
                try
                {
                    sqlConnection.Open();

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(saleQuery, sqlConnection);

                    DataSet dataSet = new DataSet();

                    dataAdapter.Fill(dataSet);

                    foreach(DataTable dataTable in dataSet.Tables)
                    {
                        Console.WriteLine(dataTable.TableName);

                        foreach(DataColumn column in dataTable.Columns)
                        {
                            Console.Write("\t{0}", column.ColumnName);
                        }

                        Console.WriteLine();

                        foreach (DataRow row in dataTable.Rows)
                        {
                            var cells = row.ItemArray;
                            foreach (object cell in cells)
                                Console.Write("\t{0}", cell);
                            Console.WriteLine();
                        }
                    }

                    sqlConnection.Close();
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

        }
    }
}
