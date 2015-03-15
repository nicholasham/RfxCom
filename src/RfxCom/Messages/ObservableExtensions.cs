using System;
using System.Reactive.Linq;
using RfxCom.Events;

namespace RfxCom.Messages
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
}