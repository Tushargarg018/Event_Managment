using EM.Core.DTOs.Response.Success;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Business.Services
{
    public interface ICurrencyService
    {
        public Task<List<CurrencyDTO>> GetCurrency();
    }
}
