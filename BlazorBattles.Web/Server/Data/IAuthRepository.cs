using System.Threading.Tasks;
using BlazorBattles.Web.Shared;

namespace BlazorBattles.Web.Server.Data
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<int>> Register(User user, string password);
        Task<ServiceResponse<string>> Login(string email, string password);
        Task<bool> UserExists(string email);
    }
}