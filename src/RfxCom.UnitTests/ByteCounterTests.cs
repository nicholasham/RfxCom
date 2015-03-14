using Should;
using Xunit;

namespace RfxCom.UnitTests
{
    public class ByteCounterTests
    {
        [Fact]
        public void Constructor_ShouldInitializeCounterWithAStartValue()
        {
            var generator = new ByteCounter(0);
            generator.Next().ShouldEqual((byte) 1);
        }

        [Fact]
        public void Reset_ReturnsZero()
        {
            var generator = new ByteCounter(5);
            generator.Reset().ShouldEqual((byte)0);
        }

        [Fact]
        public void Next_IncrementsBy1()
        {
            var generator = new ByteCounter(0);
            generator.Next().ShouldEqual((byte)1);
            generator.Next().ShouldEqual((byte)2);
            generator.Next().ShouldEqual((byte)3);
        }

        [Fact]
        public void Next_Returns1WhenMaxOf255IsExceeded()
        {
            var generator = new ByteCounter(byte.MaxValue);
            generator.Next().ShouldEqual((byte)1);
        } 
    }
}