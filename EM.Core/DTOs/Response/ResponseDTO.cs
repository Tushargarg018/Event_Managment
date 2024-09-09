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
        public string Status { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; } = [];
		public ResponseDTO()
		{
		}

		public ResponseDTO(T data, string status, string message, List<string> errors = null)
        {
            Data = data;
            Status = status;
            Message = message;
            Errors = errors ?? [];
		}
        public ResponseDTO<T> WithErrors(List<string> errors)
		{
			Errors = errors;
			return this;
		}
        public ResponseDTO<T> WithMessage(string message) {
			Message = message;
			return this;
		}

	}
}
