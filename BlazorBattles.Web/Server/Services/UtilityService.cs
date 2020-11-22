using System.Security.Claims;
using System.Threading.Tasks;
using BlazorBattles.Web.Server.Data;
using BlazorBattles.Web.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace BlazorBattles.Web.Server.Services
{
    public class UtilityService : IUtilityService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IHttpContextAccessor _contextAccessor;
        public UtilityService(ApplicationDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _contextAccessor = httpContextAccessor;
        }
        public async Task<User> GetUser()
        {
            var userId = int.Parse(_contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
            return user;
        }
    }
}