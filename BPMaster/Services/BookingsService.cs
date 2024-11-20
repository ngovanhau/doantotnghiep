using Application.Settings;
using Common.Application.CustomAttributes;
using Common.Services;
using System.Data;
using Common.Application.Settings;
using Repositories;
using BPMaster.Domains.Entities;
using Common.Application.Exceptions;
using BPMaster.Domains.Dtos;

namespace BPMaster.Services
{
    [ScopedService]
    public class BookingsService(IServiceProvider services,
        ApplicationSetting setting,
        IDbConnection connection) : BaseService(services)
    {
        private readonly BookingRepository _Repository = new(connection);

        public async Task<List<BookingsDto>> GetAll()
        {
            var deposits = await _Repository.GetAll();
            var dto = _mapper.Map<List<BookingsDto>>(deposits);
            return dto;
        }

        public async Task<BookingsDto> GetById(Guid Id)
        {
            var booking = await _Repository.GetByID(Id);

            if (booking == null)
            {
                throw new NonAuthenticateException("not found");
            }

            var dto = _mapper.Map<BookingsDto>(booking);
            return dto;
        }
        public async Task<List<BookingsDto>> GetByBuildingId(Guid Id)
        {
            var booking = await _Repository.GetBKByBuildingId(Id);

            if (booking == null)
            {
                throw new NonAuthenticateException("not found");
            }

            var dto = _mapper.Map<List<BookingsDto>>(booking);

            return dto;
        }

        public async Task<Bookings> UpdateAsync(Guid id, BookingsDto dto)
        {
            var existing = await _Repository.GetByID(id);

            if (existing == null)
            {
                throw new Exception("not found !");
            }

            var booking = _mapper.Map(dto, existing);

            await _Repository.UpdateBookings(booking);

            return booking;
        }
        public async Task<Bookings> CreateAsync(BookingsDto dto)
        {
            var booking = _mapper.Map<Bookings>(dto);

            booking.Id = Guid.NewGuid();

            await _Repository.CreateAsync(booking);

            return booking;
        }
        public async Task DeleteAsync(Guid id)
        {
            var booking = await _Repository.GetByID(id);
            if (booking == null)
            {
                throw new Exception("not found !");
            }

            await _Repository.Delete(booking);
        }
    }
}



