﻿using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Domains.Entities;
using Dapper.Contrib.Extensions;
using System.ComponentModel.DataAnnotations;

namespace BPMaster.Domains.Dtos
{
    public class InformationDto
    {
        public Guid Id { get; set; }   
        public string Username { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string Role { get; set; } = string.Empty;
        public string? FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string avata {  get; set; } = string.Empty;
    }
}
