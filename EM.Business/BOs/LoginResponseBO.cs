using EM.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Business.BOs
{
    public class LoginResponseBO
    {
        public string Token { get; set; }
        public OrganizerBo? Organizer { get; set; }
    }
}
