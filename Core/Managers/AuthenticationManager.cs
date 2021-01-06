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
                IOperationResult<string> validationLoginModelResult = ValidateLoginModel(authenticationRequest);

                if (!validationLoginModelResult.Success)
                {
                    return OperationResult<AuthenticationViewModel>.Fail(validationLoginModelResult.Message);
                }

                string encrypPassword = _encrypService.EncrypText(authenticationRequest.Password);
                SystemUser user = await _systemUserRepository.FindAsync(user => user.Username == authenticationRequest.Username && user.Password == encrypPassword);

                IOperationResult<string> validationUserResult = ValidateULogedUser(user);

                if (!validationUserResult.Success)
                {
                    return OperationResult<AuthenticationViewModel>.Fail(validationUserResult.Message);
                }

                AuthenticationViewModel authenticationResponse = _userService.GetToken(user);

                return OperationResult<AuthenticationViewModel>.Ok(authenticationResponse);
            }
            catch
            {
                return OperationResult<AuthenticationViewModel>.Fail("Ha ocurrido un error en la autenticación del usuario");
            }
        }

        private IOperationResult<string> ValidateLoginModel(AuthenticationRequest authenticationRequest)
        {
            if (authenticationRequest == null)
            {
                return OperationResult<string>.Fail("La autenticacion no puede estar nula");
            }

            if (string.IsNullOrWhiteSpace(authenticationRequest.Username))
            {
                return OperationResult<string>.Fail("El nombre de usuario es requerido");
            }

            if (string.IsNullOrWhiteSpace(authenticationRequest.Password))
            {
                return OperationResult<string>.Fail("La contraseña del usuario es requerida");
            }

            return OperationResult<string>.Ok();
        }

        private IOperationResult<string> ValidateULogedUser(SystemUser user)
        {
            if (user == null)
            {
                return OperationResult<string>.Fail("Usuario o contraseña incorrecto");
            }

            if (!user.Active)
            {
                return OperationResult<string>.Fail("Este usuario no se encuentra activo");
            }

            return OperationResult<string>.Ok();
        }
    }
}
