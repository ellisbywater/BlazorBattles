using System.Security.Claims;
using System.Threading.Tasks;
using BlazorBattles.Web.Server.Data;
using BlazorBattles.Web.Server.Services;
using BlazorBattles.Web.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorBattles.Web.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IUtilityService _utilityService;
        public UserController(ApplicationDbContext applicationDbContext, IUtilityService utilityService)
        {
            _dbContext = applicationDbContext;
            _utilityService = utilityService;
        }

        private int GetUserId() => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        private async Task<User> GetUser() => await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());

        [HttpGet("GetBananas")]
        public async Task<IActionResult> GetBananas()
        {
            var user = await _utilityService.GetUser();
            return Ok(user.Bananas);
        }

        [HttpPut("AddBananas")]
        public async Task<IActionResult> AddBananas([FromBody] int bananas)
        {
            var user = await _utilityService.GetUser();
            user.Bananas += bananas;

            await _dbContext.SaveChangesAsync();
            return Ok(user);
        }
    }
}