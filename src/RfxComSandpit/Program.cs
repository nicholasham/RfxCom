using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Media;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading.Tasks;
using RfxCom;
using RfxCom.Events;
using RfxCom.Messages;
using RfxCom.Messages.Handlers;
using RfxCom.Windows;

namespace RfxComSandpit
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            
            var communicationInterface = new UsbInterface("COM3");

            var consoleLogger = new ConsoleLogger();
            var transmitter = new Transceiver(communicationInterface, consoleLogger, new ReceiveHandlerFactory());


            transmitter.Receive()
                .ObserveOn(TaskPoolScheduler.Default)
                .Subscribe(@event =>
                {
                    
                    if (!(@event is ErrorEvent))
                        consoleLogger.Info(@event.ToString());
                    else
                        consoleLogger.Error(@event.ToString());
                });

            transmitter.Initialize().Wait();
            transmitter.SetMode(Protocol.ByronSx).Wait();

            
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

                var result = Console.ReadKey();

                
                if (soundMap.ContainsKey(result.Key))
                {
                    var sound = soundMap[result.Key];

                    consoleLogger.Info("Playing {0} ", sound);

                    var soundFileName = sound.Description.Replace(" ", string.Empty);
                    var soundFilePath = string.Format(@"C:\dev\GitHub\Sounds\{0}.wav", soundFileName);
                    
                    SoundPlayer player = new SoundPlayer(soundFilePath);
                    
                    player.Play();

                    transmitter.SendChime(ChimeSubType.ByronSx, sound).Wait();
                    
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