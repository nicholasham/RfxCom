using System;
using System.IO.Ports;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using RfxCom;
using RfxCom.Events;
using RfxCom.Messages;
using RfxCom.Messages.Handlers;

namespace RfxComSandpit
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            foreach (var portName in SerialPort.GetPortNames())
            {
                Console.WriteLine(portName);
            }

            var serialPort = new SerialPort
            {
                PortName = "COM3",
                BaudRate = 38400,
                DataBits = 8,
                StopBits = StopBits.One,
                Parity = Parity.None
            };

            serialPort.Open();

            var communicationInterface = new UsbInterface(serialPort);

            var consoleLogger = new ConsoleLogger();
            var transmitter = new Transceiver(communicationInterface, consoleLogger, new ReceiveHandlerFactory());


            transmitter.Receive(TimeSpan.FromMilliseconds(10), ThreadPoolScheduler.Instance).TimeInterval()
                .Subscribe(x =>
                {
                    var result = x.Value;
                    if (!(result is ErrorEvent))
                        consoleLogger.Info(result.ToString());
                    else
                        consoleLogger.Error(result.ToString());
                });

            transmitter.Initialize().Wait();
            transmitter.SetMode(Protocol.ByronSx).Wait();




            while (true)
            {

                var result = Console.ReadKey();
                if ((result.KeyChar == 'Y') || (result.KeyChar == 'y'))
                {
                    transmitter.SendChime(ChimeSubType.ByronSx, ChimeSound.Tubular3Notes).Wait();
                }
                else if ((result.KeyChar == 'Q'))
                {
                    Console.WriteLine("Quit");
                    break;
                }
            }

            
        }
    }
}