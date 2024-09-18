using AutoMapper;
using EM.Business.BOs;
using EM.Business.Services;
using EM.Core.DTOs.Request;
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
        public async Task<PerformerBO> AddPerformer(PerformerDTO performerDto, int organizer_id, string imageName)
        {
            var newPerformer = new Performer{
                Name = performerDto.Name,
                Bio = performerDto.Bio,
                Profile = imageName,
                OrganizerId = organizer_id,
                CreatedOn = DateTime.UtcNow,
                ModifiedOn = DateTime.UtcNow
            };
            var performer = await _repository.AddPerformer(newPerformer);
            var performerResponseBo = new PerformerBO();
            _mapper.Map(performer, performerResponseBo);
            performerResponseBo.CreatedOn = TimeConversionHelper.TruncateSeconds(performerResponseBo.CreatedOn);
            performerResponseBo.ModifiedOn = TimeConversionHelper.TruncateSeconds(performerResponseBo.ModifiedOn);
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

        public async Task<PerformerBO> UpdatePerformer(PerformerUpdateDTO performerDto, int id)
        {
            //var performer = _repository.GetPerformerById(id).Result;
            //performer.Profile = performerDto.ProfilePath;
            //performer.Bio = performerDto.Bio;
            //performer.ModifiedOn = DateTime.UtcNow;
            var updatedPerformer = _repository.UpdatePerformer(performerDto.Bio, performerDto.ProfilePath, id).Result;
            var performerBo = new PerformerBO();
            _mapper.Map(updatedPerformer, performerBo);
            performerBo.CreatedOn = TimeConversionHelper.TruncateSeconds(performerBo.CreatedOn);
            performerBo.ModifiedOn = TimeConversionHelper.TruncateSeconds(performerBo.ModifiedOn);
            return performerBo;
        }
    }
}
