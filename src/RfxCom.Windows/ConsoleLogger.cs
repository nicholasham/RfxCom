using System;
using RfxCom;
using RfxCom.Messages;

namespace RfxComSandpit
{
    public class ConsoleLogger : ILogger
    {
        public void Info(string message, params object[] arguments)
        {
            Log("Info", message, arguments);
        }
        
        public void Error(string message, params object[] arguments)
        {
            Log("Error", message, arguments);
        }

        private void Log(string level, string message , params object[] arguments)
        {
            Console.WriteLine("{0} {1} - {2} ", DateTime.Now.ToString("dd-MM-yyyy hh-mm-ss"), level, string.Format(message, arguments));
        }
    }
}