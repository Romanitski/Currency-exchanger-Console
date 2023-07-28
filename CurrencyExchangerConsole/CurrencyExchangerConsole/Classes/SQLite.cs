using System;
using Microsoft.Data.Sqlite;

namespace CurrencyExchangerConsole.Classes
{
    class SQLite
    {
        public void connection()
        {
            using (var connection = new SqliteConnection("Data Source=currencyExchanger.db"))
            {
                connection.Open();
            }
        }
    }
}
