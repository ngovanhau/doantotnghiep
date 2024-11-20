using AutoMapper;
using BPMaster.Domains.Dtos;
using BPMaster.Domains.Entities;
using Common.Mappers.AutoMapper;

public class BookingProfile : BaseProfile
{
    protected override void CreateMaps()
    {
        CreateMap<BookingsDto, Bookings>()
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<Bookings, BookingsDto>()
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
    }
}

