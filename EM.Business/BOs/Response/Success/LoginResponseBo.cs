using EM.Business.BOs.Objects;
using EM.Core.DTOs.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Core.DTOs.Response.Success
{
    public class LoginResponseBo
    {
        public string status { get; set; }
        public string token { get; set; }
        public OrganizerBo? organizer { get; set; }

    }
}
