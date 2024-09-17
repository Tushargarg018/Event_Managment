using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EM.Core.DTOs.Response
{
    public class PagedResponseDTO<T>
    {
        public T Data { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }

        public PaginationMetadata Pagination { get; set; }
        public List<string> Errors { get; set; } = [];

        public PagedResponseDTO() { }

        public PagedResponseDTO(T data, string status, string message, PaginationMetadata pagination, List<string> errors = null)
        {
            Data = data;
            Status = status;
            Message = message;
            Pagination = pagination;
            Errors = errors ?? [];
        }
        public PagedResponseDTO<T> WithErrors(List<string> errors)
        {
            Errors = errors;
            return this;
        }
        public PagedResponseDTO<T> WithMessage(string message)
        {
            Message = message;
            return this;
        }
        public PagedResponseDTO<T> WithPagination(PaginationMetadata pagination)
        {
            Pagination = pagination;
            return this;
        }

    }
}
