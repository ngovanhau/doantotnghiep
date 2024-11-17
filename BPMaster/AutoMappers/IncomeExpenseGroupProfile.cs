using AutoMapper;
using BPMaster.Domains.Dtos;
using BPMaster.Domains.Entities;
using Common.Mappers.AutoMapper;

public class IncomeExpenseGroupProfile : BaseProfile
{
    protected override void CreateMaps()
    {
        CreateMap<IncomeExpenseGroupDto, IncomeExpenseGroup>()
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<IncomeExpenseGroup, IncomeExpenseGroupDto>()
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
    }
}

