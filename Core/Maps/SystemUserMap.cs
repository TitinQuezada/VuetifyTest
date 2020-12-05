using AutoMapper;
using Core.Entities;
using Core.ViewModels;

namespace Core.Maps
{
    public sealed class SystemUserMap : Profile
    {
        public SystemUserMap()
        {
            CreateMap<SystemUserCreateViewModel, SystemUser>();
        }
    }
}
