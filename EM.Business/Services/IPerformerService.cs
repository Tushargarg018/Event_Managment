﻿using EM.Business.BOs;
using EM.Core.DTOs.Request;
using EM.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Business.Services
{
    public interface IPerformerService
    {
        public Task<PerformerBO> AddPerformer(PerformerDTO performerDto);
        public Task<List<PerformerBO>> GetPerformers();
        public Task<PerformerBO> UpdatePerformer(PerformerUpdateDTO performerDto, int id, string imagePath);
        public Task<PerformerBO> GetPerformerById(int performerId);
        public Task UpdatePerformerImage(string imagePath, int performerId);
    }
}
