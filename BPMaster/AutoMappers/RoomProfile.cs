using AutoMapper;
using BPMaster.Domains.Dtos;
using BPMaster.Domains.Entities;
using Common.Mappers.AutoMapper;

public class RoomProfile : BaseProfile
{
    protected override void CreateMaps()
    {
        CreateMap<RoomDto, Room>()
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<Room, RoomDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
    }
}
