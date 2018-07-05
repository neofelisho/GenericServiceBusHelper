using FluentAssertions;
using GenericServiceBusHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenericServiceBusHelperTest
{
    [TestClass]
    public class ServiceBusHelperTest
    {
        [DataTestMethod]
        [DataRow("123")]
        [DataRow("test broker message for string type")]
        [DataRow("if/nthere/sare/tsome/xescape/rcodes")]
        [DataRow("if*)(there!#@!are)(&)(many#$(&!!symbols")]
        public void Test_BrokerMessage_For_StringType(string expected)
        {
            var message = expected.ToBrokeredMessage();
            var actual = message.GetPayload();
            actual.Should().Be(expected);
        }

        [DataTestMethod]
        [DataRow(123.456)]
        [DataRow(1234567890123)]
        [DataRow(123)]
        [DataRow(3.5f)]
        [DataRow(EnumForServiceBusHelperTest.Foobar)]
        [DataRow('c')]
        [DataRow((byte)3)]
        [DataRow(true)]
        public void Test_BrokerMessage_For_ValueType<T>(T expected)
        {
            var message = expected.ToBrokeredMessage();
            var actual = message.GetPayload();
            actual.Should().Be(expected);
        }

        [DataTestMethod]
        [DataRow("123.456")]
        [DataRow("123")]
        public void Test_BrokerMessage_For_DecimalType(string value)
        {
            var expected = decimal.Parse(value);
            var message = expected.ToBrokeredMessage();
            var actual = message.GetPayload();
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Test_BrokerMessage_For_ReferenceType()
        {
            var expected = new ServiceBusTestObject
            {
                Foo = "123",
                Bar = 2932019m
            };

            var message = expected.ToBrokeredMessage();
            var actual = message.GetPayload();
            actual.Should().BeEquivalentTo(expected);
        }

    }

    public enum EnumForServiceBusHelperTest
    {
        Foobar
    }
}
