using AutoMapper;
using EM.Business.Services;
using EM.Core.DTOs.Response.Success;
using EM.Data.Entities;
using EM.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Business.ServiceImpl
{
    public class CurrencyService : ICurrencyService
    {
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IMapper _mapper;
        public CurrencyService(ICurrencyRepository currencyRepository, IMapper mapper)
        {
            _currencyRepository = currencyRepository;
            _mapper = mapper;
        }
        public async Task<List<CurrencyDTO>> GetCurrency()
        {
            List<Currency> currencies = await _currencyRepository.GetCurrencies();
            List<CurrencyDTO> currencyList = _mapper.Map<List<CurrencyDTO>>(currencies);
            return currencyList;
        }
    }
}
