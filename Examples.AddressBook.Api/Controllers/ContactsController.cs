using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Poci.Contacts.Data;

namespace Examples.AddressBook.Api.Controllers
{
    public class ContactsController : ApiController
    {
        readonly IAddressBookContactsService _contactsService;

        public ContactsController(IAddressBookContactsService contactsService)
        {
            _contactsService = contactsService;
        }

        // GET api/contacts
        public async Task<IEnumerable<IContact>> Get(string text, string continuationToken)
        {
            return await _contactsService.Search(null, text, continuationToken);
        }

        // GET api/contacts/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/contacts
        public void Post(string value)
        {
        }

        // PUT api/contacts/5
        public void Put(int id, string value)
        {
        }

        // DELETE api/contacts/5
        public void Delete(int id)
        {
        }
    }
}
