using Core.Entities;
using Core.Interfaces;
using Core.ViewModels;
using System.Threading.Tasks;

namespace Core.Managers
{
    public sealed class DashBoardManager
    {
        private readonly ISystemUserRepository _systemUserRepository;

        public DashBoardManager(ISystemUserRepository systemUserRepository)
        {
            _systemUserRepository = systemUserRepository;
        }

        public async Task<IOperationResult<DashBoardViewModel>> GetDashBoarData()
        {
            try
            {
                int registeredUsersQuantity = await _systemUserRepository.CountAsync();
                int activeUsersQuantity = await _systemUserRepository.CountAsync(user => user.Active == true);

                var dashBoardResult = new DashBoardViewModel
                {
                    RegisteredUsersQuantity = registeredUsersQuantity,
                    ActiveUsersQuantity = activeUsersQuantity
                };

                return OperationResult<DashBoardViewModel>.Ok(dashBoardResult);
            }
            catch
            {
                return OperationResult<DashBoardViewModel>.Fail("Ha ocurrido un error al cargar los datos del tablero");
            }
        }
    }
}
