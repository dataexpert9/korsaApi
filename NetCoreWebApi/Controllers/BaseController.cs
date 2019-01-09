using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LIMO.Controllers
{
    public class BaseController : Controller
    {
        public virtual void OnActionExecuting(ActionExecutedContext context)
        {
            string cultureShort = context.HttpContext.Request.Headers.FirstOrDefault().Value;
        }
    }
}