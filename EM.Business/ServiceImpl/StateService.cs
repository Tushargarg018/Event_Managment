using AutoMapper;
using EM.Business.BOs;
using EM.Core.DTOS.Response.Success;
using EM.Data;
using EM.Data.Entities;
using EM.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Business.Services
{
    public class StateService : IStateService
    { 
        private readonly IStateRepository _stateRepository;
        private readonly IMapper _mapper;
        public StateService(IStateRepository _stateRepository , IMapper _mapper)
        {
            this._stateRepository = _stateRepository;
            this._mapper = _mapper;
        }

        public async Task<IEnumerable<StateBo>> GetStates(int CountryId)
        {
            var state = await _stateRepository.GetStateList(CountryId);
            //var stateBOs = _mapper.Map<List<StateBO>>(states);
            var stateBOs = new List<StateBo>();
            _mapper.Map(state , stateBOs);
            return stateBOs;

        }

    }
}



