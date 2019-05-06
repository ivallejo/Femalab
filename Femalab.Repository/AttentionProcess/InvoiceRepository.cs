using Autofac.Features.Indexed;
using Femalab.Model.Entities;
using Femalab.Repository.AttentionProcess.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Femalab.Repository.AttentionProcess
{
    public class InvoiceRepository : GenericRepository<Invoice>, IInvoiceRepository
    {
        public InvoiceRepository(IIndex<String, DbContext> context)
           : base(context)
        {
        }

        public Invoice GetById(long id)
        {
            return _dbset.Where(x => x.Id == id)
                         .Include(i => i.InvoiceDetails)
                         .Include(c => c.Customer)
                         .Include(p => p.Payments)
                         .FirstOrDefault();
        }
        public Invoice GetByIdAttention(long idAttention)
        {
            return _dbset.Where(x => x.AttentionId == idAttention)
                         .Include(id => id.InvoiceDetails)
                         .Include(id => id.InvoiceDetails.Select(p => p.Product).Select(s => s.Specialty))
                         .Include(c => c.Customer)
                         .Include(p => p.Payments)
                         .FirstOrDefault();
        }

        public void UpdateInvoice(Invoice model)
        {
            _entities.Entry(model.Customer).State = EntityState.Modified;

            foreach (var pay in model.Payments)
            {
                if (pay.Id == 0)
                {
                    pay.State = true;
                    _entities.Entry(pay).State = EntityState.Added;
                }
                else
                {
                    _entities.Entry(pay).State = EntityState.Modified;
                }

            }

            _entities.Entry(model).State = EntityState.Modified;

                     
            
        }

        //public override Invoice Add(Invoice model)
        //{
        //    //if (model.PatientId != 0)
        //    //{
        //    //    _entities.Entry(model.Patient).State = System.Data.Entity.EntityState.Modified;
        //    //    model.Patient = null;
        //    //}
        //    return _dbset.Add(model);
        //}

    }
}
