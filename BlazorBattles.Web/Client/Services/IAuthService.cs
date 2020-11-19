using System.Threading.Tasks;
using BlazorBattles.Web.Shared;

namespace BlazorBattles.Web.Client.Services
{
    public interface IAuthService
    {
        Task<ServiceResponse<int>> Register(UserRegistration request);
        Task<ServiceResponse<string>> Login(UserLogin request);
    }
}