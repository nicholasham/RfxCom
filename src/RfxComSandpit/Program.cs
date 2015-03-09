using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Concurrency;

using System.Text;
using System.Threading;
using RfxCom;
using RfxCom.Events;
using RfxCom.Messages;
using RfxCom.Messages.Handlers;

namespace RfxComSandpit
{
    class Program
    {
        static void Main(string[] args)
        {

            var serialPort = new SerialPort()
            {
                PortName = "COM3",
                BaudRate = 38400,
                DataBits = 8,
                StopBits = StopBits.One,
                Parity = Parity.None
            };

            serialPort.Open();

            var communicationInterface = new SerialPortInterface(serialPort);

            var consoleLogger = new ConsoleLogger();
            var transmitter = new Transmitter(communicationInterface, consoleLogger, new ReceiveHandlerFactory());



           transmitter.Initialize().Wait();
           transmitter.Enable(Protocol.ByronSx).Wait();
            
            transmitter.Receive(TimeSpan.FromSeconds(1), ThreadPoolScheduler.Instance).TimeInterval()
           .Subscribe(x =>
           {
               var result = x.Value;
               if (!(result is ErrorEvent))
                   consoleLogger.Info(" Success: " + result.ToString());
               else
                   consoleLogger.Error(" Exception: " + result.ToString());
           });

            
            
            Console.ReadLine();
        }
    }
}
