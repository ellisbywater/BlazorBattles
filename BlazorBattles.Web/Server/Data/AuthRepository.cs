using System.Threading.Tasks;
using BlazorBattles.Web.Shared;
using Microsoft.EntityFrameworkCore;

namespace BlazorBattles.Web.Server.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public AuthRepository(ApplicationDbContext applicationDbContext)
        {
            _dbContext = applicationDbContext;
        }
        public async Task<ServiceResponse<int>> Register(User user, string password)
        {
            if (await UserExists(user.Email))
            {
                return new ServiceResponse<int>
                {
                    Success = false,
                    Message = "User already exists."
                };
            }
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            return new ServiceResponse<int>
            {
                Success = true,
                Message = "New User Registration Successful"
            };
        }

        public async Task<ServiceResponse<string>> Login(string email, string password)
        {
            var response = new ServiceResponse<string>();
            User user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email.ToLower().Equals(email.ToLower()));
            if (user == null)
            {
                response.Success = false;
                response.Message = "User not found.";
            }

            else if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                response.Success = false;
                response.Message = "Wrong password.";
            }
            else
            {
                response.Data = user.Id.ToString();
                response.Success = true;
                response.Message = $"User: {user.Username} successfully logged in.";
            }
            return response;
        }

        public async Task<bool> UserExists(string email)
        {
            if (await _dbContext.Users.AnyAsync(x => x.Email.ToLower() == email.ToLower()))
            {
                return true;
            }

            return false;
        }
        /// <summary>
        /// Hash Password for Security.
        /// 'out' paramater modifier makes the alias for parameters passwordHash and passwordSalt of type byte[]
        /// More information @ https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/out-parameter-modifier
        /// </summary>
        /// <param name="password">string input given by user</param>
        /// <param name="passwordHash">alias, value = hmac.key</param>
        /// <param name="passwordSalt">alias, value = hmac.ComputeHash of password</param>
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }

           
        }
    }
}