using CartAPIEntityFramwork.Context;
using Microsoft.AspNetCore.Mvc;

namespace CartAPIEntityFramwork.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomBaseController : ControllerBase
    {
        protected readonly CartDbContext _context;

        public CustomBaseController(CartDbContext context)
        {
            _context = context;
        }   
    }
}
