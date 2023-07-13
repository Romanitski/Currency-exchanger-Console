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
            Classes.Authorization authorization = new Classes.Authorization();
            OperatorEditing operatorEditing = new OperatorEditing();

            user.operatorName = "Namesss";
            user.operatorPassword = "1234";
            user.operatorType = "A";

            //registration.RegistrationFunction(user.operatorName, user.operatorPassword, user.operatorType);
            //registration.RegistrationFunction("TestA", "1234", "A");

            //authorization.AuthorizationFunction("TestA","1234");
            //authorization.AuthorizationFunction("TestB","1234");
            //authorization.AuthorizationFunction("TestC","1234");
            //authorization.AuthorizationFunction("TestERROR","1234");
            //authorization.AuthorizationFunction(user.operatorName, user.operatorPassword);

            operatorEditing.OperatorEditingFunction("Nameszshjb", "Nams", "1234", "C");

        }
    }
}
