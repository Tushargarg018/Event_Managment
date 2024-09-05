using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Core.DTOs.Response
{
    public class ResponseDTO<T>
    {
        public T Data { get; set; }
        public string Status {  get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }

        public ResponseDTO()
        {
            Errors = new List<string>();
        }
        public ResponseDTO(T data, string status, string message, List<string> errors)
        {
            Data = data;
            Status = status;
            Message = message;
            //Errors = errors ?? new List<string>();
            if (Errors == null)
            {
                Errors = new List<string>();
            }
            else Errors = errors;
        }
    }
}
