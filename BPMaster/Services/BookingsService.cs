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
        private readonly RoomRepository _RoomRepository = new(connection);
        private readonly BuildingRepository _BuildingRepository = new(connection);
        private readonly SendEmailRepository _SendEmailRepository = new(connection);

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

            var room = await _RoomRepository.GetByIDRoom(booking.roomid);

            if (room == null)
            {
                throw new NonAuthenticateException("Room not found");
            }

            var bd = await _BuildingRepository.GetByIDBuilding(room.Building_Id);

            if (bd == null)
            {
                throw new NonAuthenticateException("Building not found");
            }
            // gửi gmail
            string subject = "xác nhận lịch hẹn xem ";
            string body = $"Chào mừng {booking.customername}<br/><br/>" +
                          $"Chúng tôi xin thông báo lịch hẹn xem của bạn đã được hệ thống ghi nhận.<br/>" +
                          $"<strong>Số điện thoại của bạn là :</strong> {booking.phone}<br/>" +
                          $"<strong>Ngày, giờ hẹn:</strong> {booking.Date}<br/><br/>" +
                          $"<strong>Địa chỉ:</strong> {bd.address}, {bd.city}, {bd.district}<br/><br/>" +
                          $"<strong>Phòng:</strong> {room.room_name}<br/><br/>";

            try
            {
                await _SendEmailRepository.SendEmailAsync(dto.email, subject, body);
            }
            catch (Exception ex)
            {
                throw new Exception("Tạo khách hàng thất bại vui lòng kiểm tra lại gmail.");
            }
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



