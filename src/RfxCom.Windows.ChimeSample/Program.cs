using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Reflection;
using RfxCom.Events;
using RfxCom.Messages;
using RfxComSandpit;

namespace RfxCom.Windows.ChimeSample
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            using (var communicationInterface = new UsbInterface("COM3"))
            {
                var logger = new ConsoleLogger();
                var transceiver = new Transceiver(communicationInterface, logger);

                var observable = transceiver.Receive();

                observable.Subscribe(@event =>
                {
                    if (!(@event is ErrorEvent))
                        logger.Info(@event.ToString());
                    else
                        logger.Error(@event.ToString());
                });

                observable.MessagesOf<ChimeMessage>().Subscribe(Handle);

                observable.Connect();

                transceiver.Initialize().Wait();
                transceiver.SetMode(Protocol.ByronSx).Wait();

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

                    foreach (var sound in soundMap)
                    {
                        Console.WriteLine("Press [{0}] for {1}", sound.Key.ToString().Replace("D", ""), sound.Value.Description);
                    }

                    Console.WriteLine("Press Q to quit");

                    
                    var result = Console.ReadKey();


                    if (soundMap.ContainsKey(result.Key))
                    {
                        var sound = soundMap[result.Key];

                        logger.Info("Playing {0} ", sound);

                        transceiver.SendChime(ChimeSubType.ByronSx, sound).Wait();
                    }
                    else if ((result.KeyChar == 'Q' || result.KeyChar == 'q'))
                    {
                        Console.WriteLine("Quit");
                        break;
                    }
                }
                
            }
        }

        private static void Handle(MessageReceived<ChimeMessage> messageReceived)
        {
            using (var stream = GetSoundStream(messageReceived.Message.Sound))
            {
                var player = new SoundPlayer(stream);
                player.Play();
            }
        }

        private static Stream GetSoundStream(ChimeSound chimeSound)
        {
            var thisAssembly = Assembly.GetExecutingAssembly();
            var resourceName = thisAssembly
                .GetManifestResourceNames()
                .FirstOrDefault(x => x.Contains(chimeSound.Description.Replace(" ", string.Empty)));

            return thisAssembly.GetManifestResourceStream(resourceName);
        }
    }
}