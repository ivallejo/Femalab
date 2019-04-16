using Autofac.Features.Indexed;
using Femalab.Model.Persistence;
using System;
using System.Data.Entity;
using System.Linq;

namespace Femalab.Repository.Master
{
    public class PersonaRepository : IPersonaRepository
    {
        protected DbContext _entities;
        protected readonly IDbSet<PERSONA> _dbset;
        public PersonaRepository(IIndex<String, DbContext> context)
        {
            _entities = context["databaseB"];
            _dbset = context["databaseB"].Set<PERSONA>();
        }

        public PERSONA GetById(string id)
        {
            return _dbset.Where(x => x.DNI == id).FirstOrDefault();
        }
    }
}
