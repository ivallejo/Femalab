using Femalab.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Femalab.Model.Persistence
{
    public class PersonaConext : DbContext
    {
        public PersonaConext() : base("Name=PersonaContext")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }

        //public virtual DbSet<PERSONA> PERSONA { get; set; }

    }
}
