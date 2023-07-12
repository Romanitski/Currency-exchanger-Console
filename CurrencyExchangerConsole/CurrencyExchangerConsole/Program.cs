using System;
using CurrencyExchangerConsole.Classes;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchangerConsole
{
    public class Program
    {
        static void Main(string[] args)
        {
            Operator user = new Operator();
            Registration registration = new Registration();

            user.operatorName = "Namesss";
            user.operatorPassword = "1234";
            user.operatorType = "A";

            registration.RegistrationFunction(user.operatorName, user.operatorPassword, user.operatorType);
            //registration.RegistrationFunction("Namess", "1234", "A");
        }
    }
}
