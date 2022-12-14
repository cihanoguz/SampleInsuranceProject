using Model.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace API.Code
{
    public class ValidateModel : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var response = new BaseResponse<bool>();

                response.ValidationErrors = context.ModelState.Select(x => new ValidationError
                {
                    Key = x.Key,
                    Value = x.Value.Errors.FirstOrDefault().ErrorMessage
                }).ToList();
                response.SetMessage("Please Check For Errored Fields");

                foreach (var item in response.ValidationErrors)
                {
                    item.Key = item.Key.ToLower();
                }

                context.Result = new OkObjectResult(response);
            }
        }
    }
}
