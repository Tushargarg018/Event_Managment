﻿using AutoMapper;
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
        /// <summary>
        /// To add new performer
        /// </summary>
        /// <param name="performerDto"></param>
        /// <param name="organizer_id"></param>
        /// <param name="imageName"></param>
        /// <returns></returns>
        public async Task<PerformerBO> AddPerformer(PerformerDTO performerDto)
        {
            Performer performer = new Performer{
                Name = performerDto.Name,
                Bio = performerDto.Bio,
                CreatedOn = DateTime.UtcNow,
                ModifiedOn = DateTime.UtcNow
            };
            var newperformer = await _repository.AddPerformer(performer);
            var performerResponseBo = new PerformerBO();
            _mapper.Map(newperformer, performerResponseBo);
            return performerResponseBo;
        }
        /// <summary>
        /// To get all performers for a organizer
        /// </summary>
        /// <param name="organizerId"></param>
        /// <returns></returns>
        public async Task<List<PerformerBO>> GetPerformers()
        {
            var performerList = await _repository.GetPerformers();
            var performerBoList = new List<PerformerBO>();
            foreach (var performer in performerList)
            {
                var performerBo = new PerformerBO();
                _mapper.Map(performer, performerBo);
                performerBoList.Add(performerBo);
            }
            return performerBoList;
        }

        public async Task<PerformerBO> UpdatePerformer(PerformerUpdateDTO performerDto, int id, string imagePath)
        {
            var updatedPerformer = await _repository.UpdatePerformer(performerDto.Bio, performerDto.Name, imagePath, id);
            var performerBo = new PerformerBO();
            _mapper.Map(updatedPerformer, performerBo);
            return performerBo;
        }

        public async Task<PerformerBO> GetPerformerById(int performerId)
        {
            var performer = await _repository.GetPerformerById(performerId);
            var performerBo = new PerformerBO();
            _mapper.Map(performer, performerBo);
            return performerBo;
        }

        public async Task UpdatePerformerImage(string imagePath, int performerId)
        {
            await _repository.UpdatePerformerImage(imagePath, performerId);
        }
    }
}
