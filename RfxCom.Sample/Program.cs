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
        static void Main(string[] args)
        {
            try
            { 
                var transceiver = new Transceiver(new MessageCodec(), new UsbInterface("COM3"));
                transceiver.Send(new ResetMessage(), CancellationToken.None);
                Task.WaitAll(Task.Delay(TimeSpan.FromSeconds(5)));
                transceiver.Send(new SetModeMessage(1, TransceiverType.RfxTrx43392, Protocol.ByronSx), CancellationToken.None);
                transceiver.Receive(CancellationToken.None).Repeat().Subscribe(Console.WriteLine);

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