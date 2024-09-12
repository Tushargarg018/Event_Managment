using AutoMapper;
using EM.Business.BOs;
using EM.Business.Services;
using EM.Core.Helpers;
using EM.Data;
using EM.Data.Entities;
using EM.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EM.Business.ServiceImpl
{
    public class PerformerService : IPerformerService
    {
        private readonly IPerformerRepository _repository;
        private readonly IMapper _mapper;

        public PerformerService(IPerformerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public PerformerBO AddPerformer(PerformerBO performerBO)
        {
            var performer = _repository.AddPerformer(performerBO.Name, performerBO.Bio, performerBO.Profile, performerBO.OrganizerId);
            var performerResponseBo = new PerformerBO();
            _mapper.Map(performer, performerResponseBo);
            performerResponseBo.CreatedOn = TimeConversionHelper.TruncateSeconds(performerBO.CreatedOn);
            performerResponseBo.ModifiedOn = TimeConversionHelper.TruncateSeconds(performerBO.ModifiedOn);
            return performerResponseBo;
        }
        public List<PerformerBO> GetPerformers(int organizerId)
        {
            var performerList = _repository.GetPerformersUsingOrganizer(organizerId);
            var performerBoList = new List<PerformerBO>();
            foreach (var performer in performerList)
            {
                var performerBo = new PerformerBO();
                _mapper.Map(performer, performerBo);
                performerBo.CreatedOn = TimeConversionHelper.TruncateSeconds(performerBo.CreatedOn);
                performerBo.ModifiedOn = TimeConversionHelper.TruncateSeconds(performerBo.ModifiedOn);
                performerBoList.Add(performerBo);
            }
            return performerBoList;
        }
    }
}
