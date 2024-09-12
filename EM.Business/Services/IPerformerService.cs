using EM.Business.BOs;
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
        public PerformerBO AddPerformer(PerformerBO performerBO);
        public List<PerformerBO> GetPerformers(int organizerId);
    }
}
