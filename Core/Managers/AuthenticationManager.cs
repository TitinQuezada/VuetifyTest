using Core.Entities;
using Core.Interfaces;
using Core.ViewModels;
using System.Threading.Tasks;

namespace Core.Managers
{
    public sealed class AuthenticationManager
    {
        private readonly ISystemUserRepository _systemUserRepository;
        private readonly IUserService _userService;
        private readonly IEncrypService _encrypService;

        public AuthenticationManager(ISystemUserRepository systemUserRepository, IUserService userService, IEncrypService encrypService)
        {
            _systemUserRepository = systemUserRepository;
            _userService = userService;
            _encrypService = encrypService;
        }

        public async Task<IOperationResult<AuthenticationViewModel>> Login(AuthenticationRequest authenticationRequest)
        {
            try
            {
                string encrypPassword = _encrypService.EncrypText(authenticationRequest.Password);
                SystemUser user = await _systemUserRepository.FindAsync(user => user.Username == authenticationRequest.Username && user.Password == encrypPassword);

                if (user == null)
                {
                    return OperationResult<AuthenticationViewModel>.Fail("Usuario o contraseña incorrecto");
                }

                AuthenticationViewModel authenticationResponse = _userService.GetToken(user);

                return OperationResult<AuthenticationViewModel>.Ok(authenticationResponse);
            }
            catch
            {
                return OperationResult<AuthenticationViewModel>.Fail("Ha ocurrido un error en la autenticación del usuario");
            }
        }
    }
}
