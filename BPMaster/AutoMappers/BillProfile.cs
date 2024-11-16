using AutoMapper;
using BPMaster.Domains.Dtos;
using BPMaster.Domains.Entities;
using Common.Mappers.AutoMapper;

public class BillProfile : BaseProfile
{
    protected override void CreateMaps()
    {
        CreateMap<BillDto, Bill>()
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<Bill, BillDto>()
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
    }
}

