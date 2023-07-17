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

        public Coefficients(string coefficient, DateTime dateOfIssue, bool coefficientActive)
        {
            this.coefficient = coefficient;
            this.dateOfIssue = dateOfIssue;
            this.coefficientActive = coefficientActive;
        }

        public Coefficients(int coefficientId, string coefficient, DateTime dateOfIssue, bool coefficientActive)
        {
            this.coefficientId = coefficientId;
            this.coefficient = coefficient;
            this.dateOfIssue = dateOfIssue;
            this.coefficientActive = coefficientActive;
        }

        public int coefficientId { get; set; }
        public string coefficient { get; set; }
        public DateTime dateOfIssue { get; set; }
        public bool coefficientActive { get; set; }
    }
}
