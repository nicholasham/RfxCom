using System;
using System.Threading;
using System.Threading.Tasks;
using RfxCom.Messages;
using RfxCom.Messages.Chimes;
using RfxCom.Messages.InterfaceControl;

namespace RfxCom.Sample
{
    internal class Program
    {
        private static async Task MainAsync(CancellationToken cancellationToken)
        {
            var transceiver = new Transceiver(new UsbDevice("COM3"), new MessageCodec());

            transceiver.Received.Subscribe(message => Console.WriteLine($"[Received]-{message}"));
            transceiver.Sent.Subscribe(message => Console.WriteLine($"[Sent]-{message}"));

            await transceiver.StartAsync(cancellationToken);

            await transceiver.SendAsync(new SetModeMessage(1, TransceiverType.RfxTrx43392, Protocol.ByronSx),
                CancellationToken.None);

            await transceiver.SendAsync(new ByronSxChimeMessage(1, 1, ChimeSound.BigBen), cancellationToken);

            Console.ReadLine();

            await transceiver.StopAsync(cancellationToken);
        }

        private static void Main(string[] args)
        {
            try
            {
                MainAsync(CancellationToken.None).ConfigureAwait(false).GetAwaiter().GetResult();
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