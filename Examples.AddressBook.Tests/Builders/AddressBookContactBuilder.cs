using System.Collections.Generic;
using Examples.AddressBook.Data;
using Moq;
using Poci.Contacts.Data;
using Poci.Security.Data;

namespace Examples.AddressBook.Tests.Builders
{
    public class AddressBookContactBuilder
    {
        public const string ContactName = "userName";
        public const string ContactEmail = "user@example.com";

        readonly List<IEmail> _emails = new List<IEmail>();
        string _name;

        public AddressBookContactBuilder WithName(string value)
        {
            _name = value;
            return this;
        }

        public AddressBookContactBuilder WithEmail(IEmail value)
        {
            _emails.Add(value);
            return this;
        }

        public IAddressBookContact Build(IUser owner)
        {
            var mockObject = Mock.Of<IAddressBookContact>();
            mockObject.Owner = owner;
            mockObject.Name = _name;
            mockObject.Emails = _emails;

            return mockObject;
        }
    }
}