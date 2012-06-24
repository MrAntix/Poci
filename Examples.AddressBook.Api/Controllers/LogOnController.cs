using System.Net;
using System.Net.Http;
using System.Web.Http;
using Examples.AddressBook.Api.Models;
using Poci.Common.Validation;
using Poci.Security;

namespace Examples.AddressBook.Api.Controllers
{
    public class LogOnController : ApiController
    {
        readonly ISecurityService _securityService;

        public LogOnController(ISecurityService securityService)
        {
            _securityService = securityService;
        }

        // [RequireHttps]
        public HttpResponseMessage Post(
            [FromBody] UserLogOn user)
        {
            try
            {
                var session = _securityService.LogOn(user);
                if (session == null)
                {
                    throw new HttpResponseException(
                        new HttpResponseMessage(HttpStatusCode.BadRequest)
                        );
                }

                return Request.CreateResponse(HttpStatusCode.Accepted, session.Identifier);
            }
            catch (ValidationResultsException vex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, vex.Results);
            }
        }
    }
}