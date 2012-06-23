using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace Examples.AddressBook.Api
{
    public class ValidationActionFilterAttribute : System.Web.Http.Filters.ActionFilterAttribute
    {
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext context)
        {
            if(!context.ModelState.IsValid)
            {
                context.Response =
                    context.Request.CreateResponse(
                        HttpStatusCode.BadRequest,
                        from k in context.ModelState.Keys
                        let v = context.ModelState[k]
                        where v.Errors.Any()
                        select new
                                   {
                                       name=k,
                                       errors = v.Errors
                                   }
                        );
            }
        }
    }
}