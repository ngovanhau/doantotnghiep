using Application.Settings;
using Common.Application.CustomAttributes;
using Common.Services;
using System.Data;
using Common.Application.Settings;
using Repositories;
using BPMaster.Domains.Entities;
using Common.Application.Exceptions;
using BPMaster.Domains.Dtos;
using FirebaseAdmin.Auth.Multitenancy;

namespace BPMaster.Services
{
    [ScopedService]
    public class RoomService(IServiceProvider services,
        ApplicationSetting setting,
        IDbConnection connection) : BaseService(services)
    {
        private readonly RoomRepository _RoomRepository = new(connection);
        private readonly CustomerRepository _CustomerRepository = new(connection);
        public async Task<List<RoomDto>> GetAllRoom()
        {
            var rooms = await _RoomRepository.GetAllRoom();
            var result = new List<RoomDto>();

            foreach (var room  in rooms)
            {
                var Services = await _RoomRepository.GetServicesByRoom(room.Id);
                var image = await _RoomRepository.GetImagesByRoom(room.Id);
                var dto = _mapper.Map<RoomDto>(room);

                dto.imageUrls = image;
                dto.roomservice = Services;

                if(room.CustomerId != Guid.Empty)
                {
                    var customer = await _CustomerRepository.GetByIDCustomer(room.CustomerId);
                    if (customer != null) {
                        dto.nameCustomer = customer.customer_name;
                    }
                    else
                    {
                        dto.nameCustomer = "chưa có";
                    }
                }

                result.Add(dto);
            }

            return result;
        }

        public async Task<RoomDto> GetByIDRoom(Guid RoomId)
        {
            var Room = await _RoomRepository.GetByIDRoom(RoomId);

            if (Room == null)
            {
                throw new NonAuthenticateException();
            }

            var image = await _RoomRepository.GetImagesByRoom(RoomId);

            var service = await _RoomRepository.GetServicesByRoom(RoomId);

            var dto = _mapper.Map<RoomDto>(Room);

            if (Room.CustomerId != Guid.Empty)
            {
                var customer = await _CustomerRepository.GetByIDCustomer(Room.CustomerId);
                if (customer != null)
                {
                    dto.nameCustomer= customer.customer_name;
                }
                else
                {
                    dto.nameCustomer = "chưa có";
                }
            }

            dto.roomservice = service;
            dto.imageUrls = image;  

            return dto;
        }
        public async Task<(List<RoomDto> Rooms, int ActiveRoomCount)> GetAllRoomByBuildingID(Guid id)
        {
            var result = new List<RoomDto>();
            var rooms = await _RoomRepository.GetAllRoomByBuildingID(id);

            if (rooms == null || rooms.Count == 0)
            {
                throw new NonAuthenticateException("Room Not found!");
            }

            // Đếm số phòng có status = 1
            int activeRoomCount = rooms.Count(room => room.status == 1);

            foreach (var room in rooms)
            {
                var service = await _RoomRepository.GetServicesByRoom(room.Id);
                var image = await _RoomRepository.GetImagesByRoom(room.Id);

                var dto = _mapper.Map<RoomDto>(room);
                dto.roomservice = service;
                dto.imageUrls = image;
                result.Add(dto);
            }

            return (result, activeRoomCount);
        }

        public async Task<List<RoomDto>> GetRoomByStatus(int status)
        {
            var result = new List<RoomDto>();
            var rooms = await _RoomRepository.GetRoomByStatus(status);

            if (rooms == null || rooms.Count == 0)
            {
                throw new NonAuthenticateException("Room Not found!");
            }
            foreach (var room in rooms)
            {
                var sevice = await _RoomRepository.GetServicesByRoom(room.Id);

                var image = await _RoomRepository.GetImagesByRoom(room.Id);

                var dto = _mapper.Map<RoomDto>(room);

                dto.roomservice = sevice;

                dto.imageUrls = image;

                result.Add(dto);
            }
            return result;
        }
        public async Task<Room> CreateRoomAsync(RoomDto dto)
        {
            var Room = _mapper.Map<Room>(dto);
            Room.Id = Guid.NewGuid();

            await _RoomRepository.CreateAsync(Room);

            if (dto.roomservice != null && dto.roomservice.Count > 0)
            {
                var serviceIds = dto.roomservice.Select(s => s.ServiceId).ToList();
                await _RoomRepository.AddServicesToRoom(Room.Id, serviceIds);
            }
            if (dto.imageUrls != null && dto.imageUrls.Count > 0)
            {
                await _RoomRepository.AddImagesToRoom(Room.Id, dto.imageUrls);
            }
            return Room;
        }

        public async Task<Room> UpdateRoomAsync(Guid id, RoomDto dto)
        {
            var existingRoom = await _RoomRepository.GetByIDRoom(id);

            if (existingRoom == null)
            {
                throw new Exception("Building not found");
            }

            var Room = _mapper.Map(dto, existingRoom);

            if (dto.CustomerId != Guid.Empty)
            {
                var customer = await _CustomerRepository.GetByIDCustomer(dto.CustomerId);
                if (customer != null)
                {
                    dto.nameCustomer = customer.customer_name;
                }
                else
                {
                    dto.nameCustomer = "chưa có";
                }
            }

            await _RoomRepository.UpdateAsync(Room);

            await _RoomRepository.RemoveImagesFromRoom(id);
            
            await _RoomRepository.UpdateAsync(existingRoom);

            await _RoomRepository.RemoveServicesFromRoom(id);



            if (dto.roomservice != null && dto.roomservice.Count > 0)
            {
                var serviceIds = dto.roomservice.Select(s => s.ServiceId).ToList();  
                await _RoomRepository.AddServicesToRoom(id, serviceIds);
            }

            if (dto.imageUrls != null && dto.imageUrls.Count > 0)
            {
                await _RoomRepository.AddImagesToRoom(id, dto.imageUrls);
            }
            return Room;
        }
        public async Task DeleteRoomAsync(Guid id)
        {
            var Room = await _RoomRepository.GetByIDRoom(id);
            if (Room == null)
            {
                throw new Exception("Room not found !");
            }
            await _RoomRepository.RemoveServicesFromRoom(id);
            await _RoomRepository.DeleteAsync(Room);
        }
    }
}


