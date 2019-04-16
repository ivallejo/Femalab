
using Autofac.Features.Indexed;
using Femalab.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Femalab.Repository.AttentionProcess
{
    public class AttentionRepository : GenericRepository<Attention>, IAttentionRepository
    {
        public AttentionRepository(IIndex<String, DbContext> context) 
            : base(context)
        {
        }
        public override IEnumerable<Attention> GetAll()
        {
            return _entities.Set<Attention>()
                .Include(x => x.Patient)
                .Include(x => x.Doctor)
                .Include(x => x.AttentionType)
                .Include(x => x.AttentionCategory)
                .Include(x => x.AttentionDetails)
                .AsEnumerable();
        }

        public void CreateAttention(Attention model)
        {
            _entities.Entry(model.Patient).State = System.Data.Entity.EntityState.Added;
            _dbset.Add(model);
        }

        public void UpdateAttention(Attention model)
        {
            _entities.Entry(model.Patient).State = System.Data.Entity.EntityState.Modified;
            _entities.Entry(model).State = System.Data.Entity.EntityState.Modified;            
        }

        public Attention GetById(long id)
        {
            return _dbset.Where(x => x.Id == id)
                         .Include(p => p.Patient)
                         .Include(d => d.Doctor)
                         .Include(at => at.AttentionType)
                         .Include(ac => ac.AttentionCategory)
                         .Include(ad => ad.AttentionDetails)
                         .FirstOrDefault();
        }
    }
}
