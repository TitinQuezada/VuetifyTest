using Core.Entities;
using Core.ViewModels;

namespace Core.Interfaces
{
    public interface IUserService
    {
        AuthenticationViewModel GetToken(SystemUser user);
    }
}
