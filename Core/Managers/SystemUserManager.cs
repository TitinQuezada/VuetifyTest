using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.ViewModels;
using System.Threading.Tasks;

namespace Core.Managers
{
    public sealed class SystemUserManager
    {
        private readonly ISystemUserRepository _systemUserRepository;
        private readonly IMapper _mapper;
        private readonly IEncrypService _encrypService;

        public SystemUserManager(ISystemUserRepository systemUserRepository, IMapper mapper, IEncrypService encrypService)
        {
            _systemUserRepository = systemUserRepository;
            _mapper = mapper;
            _encrypService = encrypService;
        }

        public async Task<IOperationResult<bool>> Create(SystemUserCreateViewModel systemUserToCreate)
        {
            try
            {
                systemUserToCreate.Password = _encrypService.EncrypText(systemUserToCreate.Password);

                SystemUser systemUser = _mapper.Map<SystemUser>(systemUserToCreate);
                _systemUserRepository.Create(systemUser);

                await _systemUserRepository.SaveAsync();

                return OperationResult<bool>.Ok(true);
            }
            catch
            {
                return OperationResult<bool>.Fail("Ha ocurrido un error creando el usuario");
            }
        }
    }
}
