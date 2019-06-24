
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
            return _entities.Set<Attention>().Where(x=> x.State == true)
                .Include(x => x.Patient)
                .Include(x => x.Doctor)
                .Include(x => x.AttentionType)
                .Include(x => x.AttentionCategory)
                .Include(x => x.AttentionDetails)
                .Include(x => x.Invoice)
                .AsEnumerable();
        }

        public IEnumerable<Attention> GetAllPending()
        {
            return _entities.Set<Attention>().Where(x => x.State == true)
                .Include(x => x.Patient)
                .Include(x => x.Doctor)
                .Include(x => x.AttentionType)
                .Include(x => x.AttentionCategory)
                .Include(x => x.AttentionDetails)
                .Include(x => x.Invoice.Select(p => p.Payments))
                .Include(x => x.AttentionDetails.Select(p => p.Product))
                //.Include(ad => ad.AttentionDetails.Select(p => p.Product).Select(s => s.Specialty))
                .AsEnumerable();

        }

        public void CreateAttention(Attention model)
        {
            if (model.PatientId != 0)
            {
                _entities.Entry(model.Patient).State = System.Data.Entity.EntityState.Modified;
                model.Patient = null;
            }
            _dbset.Add(model);            
        }

        public void UpdateAttention(Attention model)
        {
            _entities.Entry(model.Patient).State = System.Data.Entity.EntityState.Modified;
            _entities.Entry(model).State = System.Data.Entity.EntityState.Modified;            
        }
            
        public Attention GetById(long id)
        {
            return _dbset.Where(x => x.Id == id).Where(x => x.State == true)
                         .Include(p => p.Patient)
                         .Include(d => d.Doctor)
                         .Include(at => at.AttentionType)
                         .Include(ac => ac.AttentionCategory)
                         .Include(ad => ad.AttentionDetails.Select(p => p.Product).Select(s => s.Specialty))
                         .Include(ad => ad.AttentionDetails.Select(p => p.Product).Select(s => s.Category))
                         .FirstOrDefault();
        }


    }
}
