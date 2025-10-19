using Cabio.Dashboard.Application.Dtos.Users;
using System.Threading.Tasks;

namespace Cabio.Dashboard.Application.Services
{ 
    public interface IUserService
    {
        Task<string> LoginAsync(UserLoginDto dto);
        Task<bool> SignupAsync(UserSignupDto dto);
    }
}
