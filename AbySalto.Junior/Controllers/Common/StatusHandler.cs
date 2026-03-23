using AbySalto.Junior.Services.Common;
using Microsoft.AspNetCore.Mvc;

namespace AbySalto.Junior.Controllers.Common
{
    public static class StatusHandler
    {
        public static IActionResult HandleResult<T>(this Controller controller, Result<T> result)
        {
            if (result.IsSuccess)
                return controller.Ok(result.Value);
            return controller.BadRequest(result.ErrorItems);
        }
    }
}