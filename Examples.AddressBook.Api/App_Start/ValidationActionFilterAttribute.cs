using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Examples.AddressBook.Api.App_Start
{
    public class ValidationActionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Response =
                    context.Request.CreateResponse(
                        HttpStatusCode.BadRequest,
                        from k in context.ModelState.Keys
                        let v = context.ModelState[k]
                        where v.Errors.Any()
                        select new
                                   {
                                       name = k,
                                       errors = v.Errors
                                   }
                        );
            }
        }
    }
}