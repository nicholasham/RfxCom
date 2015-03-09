using System;
using RfxCom;

namespace RfxComSandpit
{
    public class ConsoleLogger : ILogger
    {
        public void Info(string message, params object[] arguments)
        {
            Console.WriteLine("{0} Info - {1} ", DateTime.Now.ToString("dd-MM-yyyy hh-mm-ss"), string.Format(message, arguments));
        }

        public void Debug(string message, params object[] arguments)
        {
            Console.WriteLine("{0} Debug - {1} ", DateTime.Now.ToString("dd-MM-yyyy hh-mm-ss"), string.Format(message, arguments));
        }

        public void Error(string message, params object[] arguments)
        {
            Console.WriteLine("{0} Error - {1} ", DateTime.Now.ToString("dd-MM-yyyy hh-mm-ss"), string.Format(message, arguments));
        }
    }
}