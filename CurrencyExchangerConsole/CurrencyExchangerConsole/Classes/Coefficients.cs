using System;

namespace CurrencyExchangerConsole.Classes
{
    public class Coefficients
    {
        private static Coefficients instance;

        public static Coefficients GetInstance()
        {
            return instance ?? (instance = new Coefficients());
        }

        public Coefficients() { }

        public Coefficients(string coefficient, DateTime dateOfIssue, DateTime dateOfTheStartAction)
        {
            this.coefficient = coefficient;
            this.dateOfIssue = dateOfIssue;
            this.dateOfTheStartAction = dateOfTheStartAction;
        }

        public Coefficients(int coefficientId, string coefficient, DateTime dateOfIssue, DateTime dateOfTheStartAction)
        {
            this.coefficientId = coefficientId;
            this.coefficient = coefficient;
            this.dateOfIssue = dateOfIssue;
            this.dateOfTheStartAction = dateOfTheStartAction;
        }

        public int coefficientId { get; set; }
        public string coefficient { get; set; }
        public DateTime dateOfIssue { get; set; }
        public DateTime dateOfTheStartAction { get; set; }
    }
}
