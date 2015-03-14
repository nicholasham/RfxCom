using System.Threading;

namespace RfxCom
{
    public class ByteCounter
    {
        public ByteCounter(int startValue)
        {
            _counter = startValue;
        }

        private long _counter;

        public byte Reset()
        {
            Interlocked.Exchange(ref _counter, 0);
            return (byte)Interlocked.Read(ref _counter);
        }

        public byte Next()
        {
            Interlocked.CompareExchange(ref _counter, byte.MinValue, byte.MaxValue);
            return (byte) Interlocked.Increment(ref _counter);
        }
    }
}