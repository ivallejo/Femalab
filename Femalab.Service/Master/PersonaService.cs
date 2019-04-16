using Femalab.Model.Entities;
using Femalab.Model.Persistence;
using Femalab.Repository.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Femalab.Service.MasterService
{
    public class PersonaService : IPersonaService
    {
        IPersonaRepository _personaRepository;

        public PersonaService(IPersonaRepository personaRepository)
        {
            _personaRepository = personaRepository;
        }

        public PERSONA GetById(string Id)
        {
            return _personaRepository.GetById(Id);
        }
    }
}
