using RfxCom.Messages;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using RfxCom.Messages.Chimes;
using RfxCom.Messages.InterfaceControl;

namespace RfxCom.Samples.Doorbell
{
    class Program
    {
        private static async Task MainAsync(CancellationToken cancellationToken)
        {
            var transceiver = new Transceiver(new UsbDevice("COM3"), new MessageCodec());

            transceiver.Received.Subscribe(message => Console.WriteLine($"[Received]-{message}"));
            transceiver.Sent.Subscribe(message => Console.WriteLine($"[Sent]-{message}"));

            await transceiver.StartAsync(cancellationToken);

            await transceiver.SendAsync(new SetModeMessage(1, TransceiverType.RfxTrx43392, Protocol.ByronSx),
                CancellationToken.None);

            var soundMap = new Dictionary<ConsoleKey, ChimeSound>
            {
                {ConsoleKey.D0, ChimeSound.BigBen},
                {ConsoleKey.D1, ChimeSound.Clarinet},
                {ConsoleKey.D2, ChimeSound.Solo},
                {ConsoleKey.D3, ChimeSound.Tubular2Notes},
                {ConsoleKey.D4, ChimeSound.Tubular3Notes},
                {ConsoleKey.D5, ChimeSound.TubularMix},
                {ConsoleKey.D6, ChimeSound.Saxophone},
                {ConsoleKey.D7, ChimeSound.MorningDew}
            };

            
            while (true)
            {
                

                Console.WriteLine("Ring the doorbell:");

                foreach (var sound in soundMap)
                {
                    Console.WriteLine($"Press [{sound.Key.ToString().Replace("D", "")}] for {sound.Value}");
                }

                Console.WriteLine("Press Q to quit");
                
                var result = Console.ReadKey();

                Console.WriteLine();
                
                if (soundMap.ContainsKey(result.Key))
                {
                    var sound = soundMap[result.Key];
                    Console.WriteLine("Ringing doorbell with sound {0} ", sound);
                    await transceiver.SendAsync(new ByronSxChimeMessage(1, 1, ChimeSound.BigBen), cancellationToken);
                }
                else if ((result.KeyChar == 'Q' || result.KeyChar == 'q'))
                {
                    Console.WriteLine("Quitting");
                    await transceiver.StopAsync(cancellationToken);

                    break;
                }
            }




        }

        static void Main(string[] args)
        {
            try
            {
               CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
               Task.WaitAll(MainAsync(cancellationTokenSource.Token));
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}