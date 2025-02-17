﻿using EM.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Data.Repositories
{
    public interface IPerformerRepository
    {
        public Task<Performer> AddPerformer(Performer performer);
        //public IEnumerable<Performer> GetPerformersUsingOrganizer(int organizerId);
        public Task<IEnumerable<Performer>> GetPerformers();
        public Task<bool> PerformerExistsAsync(int performerId);
        public Task<Performer> UpdatePerformer(string bio, string name, string profile_pic, int performer_id);
        public Task<Performer> GetPerformerById(int id);
        public Task<string> GetPerformerProfilePath(int id);
        public Task UpdatePerformerImage(string fileName, int id);
        public Task<bool> PerformerNameExistAsync(string performerName);
    }
}
