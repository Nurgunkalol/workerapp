using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WorkersApp.Infrastructure
{
    public class ValidateModelAttribute: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            if (!actionContext.ModelState.IsValid)
            {
                actionContext.Result = new BadRequestObjectResult(new
                {
                    Message = "ModelState is invalid",
                    ModelState = actionContext.ModelState
                });
            }
        }

        public override void OnActionExecuted(ActionExecutedContext actionContext)
        {
            if (!actionContext.ModelState.IsValid)
            {
                actionContext.Result = new BadRequestObjectResult(new
                {
                    Message = "ModelState is invalid",
                    ModelState = actionContext.ModelState
                });
            }
        }
    }
}
