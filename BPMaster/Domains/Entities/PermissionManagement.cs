
using Common.Domains.Entities;
using Dapper.Contrib.Extensions;
using Domain.Entities;
using Domain.Enums;
using FirebaseAdmin.Auth;
using System.Buffers.Text;
using System.ComponentModel.DataAnnotations;

namespace BPMaster.Domains.Entities
{
    [Table("permission")]
    public class PermissionManagement : SystemLogEntity<Guid>
    {
        public string Username { get; set; } = string.Empty;
        public Guid UserId {  get; set; }
        public Guid BuildingId { get; set; }
    }
}