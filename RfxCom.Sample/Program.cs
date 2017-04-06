using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using RfxCom.Messages;
using RfxCom.Messages.InterfaceControl;

namespace RfxCom.Sample
{
    class Program
    {


        static async Task MainAsync()
        {
            var transceiver = new Transceiver(new MessageCodec(), new UsbDevice("COM3"));
            await transceiver.SendAsync(new ResetMessage(), CancellationToken.None);
            await Task.Delay(TimeSpan.FromSeconds(5));
            await HandleReceive(transceiver);
            await transceiver.SendAsync(new SetModeMessage(1, TransceiverType.RfxTrx43392, Protocol.ByronSx),
                CancellationToken.None);
            await HandleReceive(transceiver);

            while (true)
            {
                await HandleReceive(transceiver);

                await Task.Delay(TimeSpan.FromSeconds(1));
            }

        }

        private static async Task HandleReceive(Transceiver transceiver)
        {
            foreach (var message in await transceiver.ReceiveAsync(CancellationToken.None))
            {
                Console.WriteLine(message.ToString());
            }
        }

        static void Main(string[] args)
        {
            try
            { 
                Task.WaitAll(MainAsync());
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