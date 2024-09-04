using EM.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Data.Repositories
{
    public interface IOrganizerRepository
    {
        public Task<IEnumerable<Organizer>> GetOrganizers();
        public Task<Organizer> GetOrganizerByEmailAndPassword(string email, string password);
        public Task<Organizer> GetOrganizerByEmail(string email);
    }
}
