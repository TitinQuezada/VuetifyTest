using Core.Entities;
using Core.Interfaces;

namespace Database.Repositories
{
    public sealed class SystemUserRepository : BaseRepository<SystemUser>, ISystemUserRepository
    {
        public SystemUserRepository(ApplicationContext applicationContext) : base(applicationContext)
        {

        }
    }
}
