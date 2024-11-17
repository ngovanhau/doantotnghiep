using AutoMapper;
using BPMaster.Domains.Dtos;
using BPMaster.Domains.Entities;
using Common.Mappers.AutoMapper;

public class TransactionGroupProfile : BaseProfile
{
    protected override void CreateMaps()
    {
        CreateMap<TransactionGroupDto, TransactionGroup>()
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<TransactionGroup, TransactionGroupDto>()
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
    }
}

