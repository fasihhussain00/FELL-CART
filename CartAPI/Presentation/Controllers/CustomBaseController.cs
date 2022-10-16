using CartAPI.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CartAPI.Presentation.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomBaseController : ControllerBase
    {
        private Guid RequestID { get => Guid.Parse(Request.Headers["RequestId"]); }
        protected ResponseBuilder ResponseBuilder { get => new(RequestID); }
        protected ResponseWriter ResponseWriter { get => new(HttpContext); }
    }
}
