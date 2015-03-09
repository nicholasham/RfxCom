using System.Collections.Generic;
using RfxCom.Messages;

namespace RfxCom.UnitTests.Messages
{
    public class TestField : Field<TestField>
    {
        public static TestField Field1 = new TestField(0x03, "Field1");
        public static TestField Field2 = new TestField(0x20, "Field2");
        public static TestField Field3 = new TestField(0x40, "Field2");

        private TestField(byte value, string description) : base(value, description)
        {
        }
        
    }
}