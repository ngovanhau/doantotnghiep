using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPMaster.Domains.Dtos
{
    public class PermissionManagementDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public Guid BuildingId { get; set; }
    }
}
