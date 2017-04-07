using System;
using System.Threading;
using System.Threading.Tasks;
using RfxCom.Messages;
using RfxCom.Messages.InterfaceControl;

namespace RfxCom.Sample
{
    internal class Program
    {
        private static async Task MainAsync(CancellationToken cancellationToken)
        {
            using (var transceiver = new Transceiver(new UsbDevice("COM3"), new MessageCodec()))
            {
                transceiver.Received.Subscribe(message => Console.WriteLine($"[Received]-{message}"));
                transceiver.Sent.Subscribe(message => Console.WriteLine($"[Sent]-{message}"));

                await transceiver.StartAsync(cancellationToken);

                await transceiver.SendAsync(new SetModeMessage(1, TransceiverType.RfxTrx43392, Protocol.ByronSx),
                    CancellationToken.None);

                Console.ReadLine();

                await transceiver.StopAsync(cancellationToken);
            }
        }

        private static void Main(string[] args)
        {
            try
            {
                Task.WaitAll(MainAsync(CancellationToken.None));
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