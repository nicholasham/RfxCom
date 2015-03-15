﻿using System;
using System.Collections.Generic;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using RfxCom.Events;
using RfxCom.Messages;
using RfxComSandpit;

namespace RfxCom.Windows.ChimeSample
{
    public static class ObservableExtensions
    {
        public static IObservable<MessageReceived<T>> MessagesOf<T>(this IObservable<Event> observable) where T : Message
        {
            return
                observable.Where(@event => @event is MessageReceived<T>)
                    .Cast<MessageReceived<T>>();
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            using (var communicationInterface = new UsbInterface("COM3"))
            {
                var logger = new ConsoleLogger();
                var transceiver = new Transceiver(communicationInterface, logger);


                transceiver.Receive(TimeSpan.FromMilliseconds(10), ThreadPoolScheduler.Instance).TimeInterval()
                .Subscribe(x =>
                {
                    var result = x.Value;
                    if (!(result is ErrorEvent))
                        logger.Info(result.ToString());
                    else
                        logger.Error(result.ToString());
                });


                transceiver.Initialize().Wait();
                transceiver.SetMode(Protocol.ByronSx).Wait();
                
                Console.ReadLine();
                
            }
        }

        private static void Handle(IEnumerable<ChimeMessage> chimeMessages)
        {
        }
    }
}