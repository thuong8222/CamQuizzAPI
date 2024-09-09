using CalmQuizzModels;
using Microsoft.AspNetCore.Mvc;

namespace CalmQuizzAdminAPI.Controllers
{
    
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BaseAdminAPIController : ControllerBase
    {
        [HttpGet]
        public JsonResponse Success(object? data = null, string? message = null)
        {

            return new JsonResponse() { status = "success", data = data, message = message };
        }
        [HttpGet]
        public JsonResponse Error(string? message = null)
        {

            return new JsonResponse() { status = "error", message = message };
        }
    }
}
