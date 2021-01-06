using AutoMapper;
using Core.Entities;
using Core.Entities.Email;
using Core.Interfaces;
using Core.Interfaces.Email;
using Core.ViewModels;
using System;
using System.Threading.Tasks;

namespace Core.Managers
{
    public sealed class SystemUserManager
    {
        private readonly ISystemUserRepository _systemUserRepository;
        private readonly IMapper _mapper;
        private readonly IEncrypService _encrypService;
        private readonly IEmailService _emailService;

        public SystemUserManager(ISystemUserRepository systemUserRepository, IMapper mapper, IEncrypService encrypService, IEmailService emailService)
        {
            _systemUserRepository = systemUserRepository;
            _mapper = mapper;
            _encrypService = encrypService;
            _emailService = emailService;
        }

        public async Task<IOperationResult<bool>> Create(SystemUserCreateViewModel systemUserToCreate)
        {
            try
            {
                IOperationResult<bool> isValidSystemUserToCreateResult = ValidateUserToCreate(systemUserToCreate);

                if (!isValidSystemUserToCreateResult.Success)
                {
                    return OperationResult<bool>.Fail(isValidSystemUserToCreateResult.Message);
                }

                systemUserToCreate.Password = _encrypService.EncrypText(systemUserToCreate.Password);

                SystemUser systemUser = _mapper.Map<SystemUser>(systemUserToCreate);

                _systemUserRepository.Create(systemUser);

                await _systemUserRepository.SaveAsync();

                await SendActivationEmail(systemUser);

                return OperationResult<bool>.Ok(true);
            }
            catch
            {
                return OperationResult<bool>.Fail("Ha ocurrido un error creando el usuario");
            }
        }

        private IOperationResult<bool> ValidateUserToCreate(SystemUserCreateViewModel systemUserToCreate)
        {
            foreach (var prop in systemUserToCreate.GetType().GetProperties())
            {
                Object value = prop.GetValue(systemUserToCreate, null);

                if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
                {
                    return OperationResult<bool>.Fail("Todos los campos son requeridos");
                }
            }

            return OperationResult<bool>.Ok(true);
        }

        private async Task SendActivationEmail(SystemUser systemUser)
        {
            SystemUserActivationRequest systemUserActivationRequest = new SystemUserActivationRequest
            {
                Email = systemUser.Email,
                EncriptedUsername = _encrypService.EncrypText(systemUser.Username)
            };

            await _emailService.SendActivationEmail(systemUserActivationRequest);
        }

        public async Task<IOperationResult<bool>> Activate(string email, string encriptedUsername)
        {
            try
            {
                SystemUser systemUser = await _systemUserRepository.FindAsync(user => user.Email == email);

                ValidateUserToActive(systemUser, encriptedUsername);

                systemUser.Active = true;

                await _systemUserRepository.SaveAsync();

                return OperationResult<bool>.Ok(true);
            }
            catch
            {
                return OperationResult<bool>.Fail("Ha ocurrido un error activando el usuario comuniquese con el administrador del sistema para mas informacion");
            }
        }

        private void ValidateUserToActive(SystemUser systemUser, string encriptedUsername)
        {
            if (systemUser == null)
            {
                throw new Exception("Este link es invalido");
            }

            string usernameEncripted = _encrypService.EncrypText(systemUser.Username);

            if (!encriptedUsername.Equals(usernameEncripted))
            {
                throw new Exception("Este link es invalido");
            }
        }
    }
}
