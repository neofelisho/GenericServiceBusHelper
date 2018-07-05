namespace GenericServiceBusHelper
{
    public class ServiceBusTestObject
    {
        public string Foo { get; set; }
        public decimal Bar { get; set; }
        public override string ToString()
        {
            return Foo + Bar;
        }
    }
}