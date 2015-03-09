using System;
using Xunit;

namespace RfxCom.UnitTests.Messages
{
    public class FieldTests
    {
        [Fact]
        public void List_ShouldListAllStaticFieldsOfTypeFieldThatAreDefinedInTheClass()
        {
            var expected = new[] {TestField.Field1, TestField.Field2, TestField.Field3};
            var actual = TestField.List();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void IsEnabled_ShouldReturnTrueIfAFieldsValueIsAnEnabledBitInAByte()
        {
            var value = Convert.ToByte(TestField.Field2 + TestField.Field3);

            Assert.True(TestField.Field2.IsEnabled(value));
            Assert.True(TestField.Field3.IsEnabled(value));
        }

        [Fact]
        public void IsEnabled_ShouldReturnFalseIfAFieldsValueIsNotAnEnabledBitInAByte()
        {
            var value = Convert.ToByte(TestField.Field2 + TestField.Field3);
            Assert.False(TestField.Field1.IsEnabled(value));
        }

        [Fact]
        public void ListEnabled_ListsAllFieldsThatAreEnabledBitsInAByte()
        {
            var value = Convert.ToByte(TestField.Field2 + TestField.Field3);
            var expected = new[] { TestField.Field2, TestField.Field3 };
            var actual = TestField.ListEnabled(value);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TryParse_ShouldFindTheFirstFieldThatMatchesTheProvidedValueAndReturnTrue()
        {
            var expected = TestField.Field2;

            TestField actual;
            var result = TestField.TryParse(expected.Value, out actual);

            Assert.Same(expected, actual);
            Assert.True(result);
        }

        [Fact]
        public void TryParse_ShouldReturnFalseWhenItCannotFindAFieldWithMatchingValue()
        {

            TestField actual;
            var result = TestField.TryParse(0x99, out actual);

            Assert.Null(actual);
            Assert.False(result);
        }

        [Fact]
        public void Parse_ShouldReturnFirstFieldThatMatchesTheGivenValue()
        {
            var expected = TestField.Field2;

            TestField actual= TestField.Parse(expected.Value, TestField.Field1);

            Assert.Same(expected, actual);
        }

        [Fact]
        public void Parse_ShouldReturnDefaultFieldWhenUnableToMatchAFieldOnValue()
        {
            var expected = TestField.Field1;

            TestField actual = TestField.Parse(0x99, TestField.Field1);

            Assert.Same(expected, actual);
        }
    }
}