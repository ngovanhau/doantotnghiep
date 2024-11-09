using Application.Settings;
using BPMaster.Domains.Dtos;
using BPMaster.Services;
using Common.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain.Dtos;
using Services;

namespace BPMaster.Controllers.v1
{
    public class RoomController(IServiceProvider service) : BaseV1Controller<RoomService, ApplicationSetting>(service)
    {
        /// <summary>
        /// this is api get all Room
        /// </summary>
        [HttpGet("Roomall")]
        public async Task<IActionResult> GetallRoom()
        {
            var Room = await _service.GetAllRoom();
            return Success(Room);
        }
        /// <summary>
        /// this is api get all room by userid
        /// </summary>
        [HttpGet("getallroombymanagementid")]
        public async Task<IActionResult> GetRoomsByUserId(Guid id)
        {
            return Success(await _service.GetRoomsByUserId(id));
        }
        /// <summary>
        /// this is api get room by userid
        /// </summary>
        [HttpGet("getallroombyuseridmb")]
        public async Task<IActionResult> GetRoomByUserIdMB(Guid id)
        {
            return Success(await _service.GetRoomByUserIdMB(id));
        }
        /// <summary>
        /// this is api get by id Room
        /// </summary>
        [HttpGet("getRoombyid")]
        public async Task<IActionResult> GetRoomById(Guid id)
        {
            return Success(await _service.GetByIDRoom(id));
        }
        /// <summary>
        /// this is api create a new Room
        /// </summary>
        [HttpPost("create")]
        public async Task<IActionResult> CreateRoom([FromBody] RoomDto dto)
        {
            return CreatedSuccess(await _service.CreateRoomAsync(dto));
        }
        /// <summary>
        /// this is api update Room
        /// </summary>
        [HttpPut("update")]
        public async Task<IActionResult> UpdateRoom(Guid id, [FromBody] RoomDto dto)
        {
            return Success(await _service.UpdateRoomAsync(id, dto));
        }
        /// <summary>
        /// this is api delete Room
        /// </summary>
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteRoom(Guid id)
        {
            await _service.DeleteRoomAsync(id);
            return Success("delete Success");
        }
        /// <summary>
        /// this is api GetallRoomByBuildingID
        /// </summary>
        [HttpGet("GetallRoomByBuildingID")]
        public async Task<IActionResult> GetallRoomByBuildingID(Guid id)
        {
            var (rooms, activeRoomCount) = await _service.GetAllRoomByBuildingID(id);

            return Success(new { Rooms = rooms, ActiveRoomCount = activeRoomCount });
        }
        [HttpGet("GetRoomByStatus")]
        public async Task<IActionResult> GetRoomByStatus(int status)
        {
            return Success(await _service.GetRoomByStatus(status)); 
        }
    }
}

