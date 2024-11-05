using AutoMapper;
using BPMaster.Domains.Dtos;
using BPMaster.Domains.Entities;
using Common.Mappers.AutoMapper;

public class PermisionProfile : BaseProfile
{
    protected override void CreateMaps()
    {
        CreateMap<PermissionManagementDto, PermissionManagement>()
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<PermissionManagement, PermissionManagementDto>()
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
    }
}
