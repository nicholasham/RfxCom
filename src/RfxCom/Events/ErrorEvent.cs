using System;

namespace RfxCom.Events
{
    public class ErrorEvent : Event
    {
        public ErrorEvent(Exception exception)
        {
            Exception = exception;
        }

        public Exception Exception { get; private set; }

        public override string ToString()
        {
            return Exception.ToString();
        }
    }
}