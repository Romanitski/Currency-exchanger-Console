using System;

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
    }
}
