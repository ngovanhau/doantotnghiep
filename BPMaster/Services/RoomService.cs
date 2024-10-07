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
    public class RoomService(IServiceProvider services,
        ApplicationSetting setting,
        IDbConnection connection) : BaseService(services)
    {
        private readonly RoomRepository _RoomRepository = new(connection);

        public async Task<List<Room>> GetAllRoom()
        {
            return await _RoomRepository.GetAllRoom();
        }

        public async Task<Room> GetByIDRoom(Guid RoomId)
        {
            var Room = await _RoomRepository.GetByIDRoom(RoomId);

            if (Room == null)
            {
                throw new NonAuthenticateException();
            }
            return Room;
        }

        public async Task<Room> CreateRoomAsync(RoomDto dto)
        {
            var Room = _mapper.Map<Room>(dto);

            Room.Id = Guid.NewGuid();

            await _RoomRepository.CreateAsync(Room);

            return Room;
        }
        public async Task<Room> UpdateRoomAsync(Guid id, RoomDto dto)
        {
            var existingRoom = await _RoomRepository.GetByIDRoom(id);

            if (existingRoom == null)
            {
                throw new Exception("Room not found !");
            }
            var Room = _mapper.Map(dto, existingRoom);

            await _RoomRepository.UpdateAsync(Room);

            return Room;
        }
        public async Task DeleteRoomAsync(Guid id)
        {
            var Room = await _RoomRepository.GetByIDRoom(id);
            if (Room == null)
            {
                throw new Exception("Room not found !");
            }
            await _RoomRepository.DeleteAsync(Room);
        }

    }
}


