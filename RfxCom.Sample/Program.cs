using System;
using System.Threading;
using System.Threading.Tasks;
using RfxCom.Messages;
using RfxCom.Messages.InterfaceControl;

namespace RfxCom.Sample
{
    internal class Program
    {
        private static async Task MainAsync()
        {
            using (var transceiver = new ReactiveTransceiver(new MessageCodec(), new UsbDevice("COM3")))
            {
                transceiver.Received.Subscribe(message => Console.WriteLine($"[Received]-{message}"));
                transceiver.Sent.Subscribe(message => Console.WriteLine($"[Sent]-{message}"));
                Console.ReadLine();
            }
        }

        private static void Main(string[] args)
        {
            try
            {
                Task.WaitAll(MainAsync());
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            Console.WriteLine("Hello World!");
        }
    }
}