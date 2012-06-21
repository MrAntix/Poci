using Moq;
using Poci.Contacts.Data;

namespace Examples.AddressBook.Tests.Builders
{
    public class EmailBuilder
    {
        public IEmail Build(
            string address, string type = "")
        {
            var mockObject = Mock.Of<IEmail>();
            mockObject.Address = address;
            mockObject.Type = type;

            return mockObject;
        }
    }
}