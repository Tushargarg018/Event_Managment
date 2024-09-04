using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Core.DTOs.Response
{
    public class ResponseDTO
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<DataDTO> data { get; set; }   //GENERIC IMPLEMENTATION PENDING
        public List<ErrorDTO> error { get; set;}
    }
}
