using System.Threading.Tasks;
using BlazorBattles.Web.Shared;

namespace BlazorBattles.Web.Server.Services
{
    public interface IUtilityService
    {
        Task<User> GetUser();
        
    }
}