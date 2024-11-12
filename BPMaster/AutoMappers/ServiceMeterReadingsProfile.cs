using AutoMapper;
using BPMaster.Domains.Dtos;
using BPMaster.Domains.Entities;
using Common.Mappers.AutoMapper;

public class ServiceMeterReadingsProfile : BaseProfile
{
    protected override void CreateMaps()
    {
        CreateMap<ServiceMeterReadingsDto, ServiceMeterReadings>()
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<ServiceMeterReadings, ServiceMeterReadingsDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
    }
}
