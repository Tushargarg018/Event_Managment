﻿using EM.Core.DTOs.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Core.DTOs.Response.Success
{
    public class LoginResponseDTO
    {
        public string token { get; set; }
        public OrganizerDto? organizer { get; set; }

    }
}
