using Autofac.Features.Indexed;
using Femalab.Model.Entities;
using Femalab.Repository.AttentionProcess.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
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

        public Invoice GetNumber(string series)
        {
            return _dbset.Where(x => x.Series == series && x.State == true)
                         .LastOrDefault();
        }

        public Invoice GetById(long id)
        {
            return _dbset.Where(x => x.Id == id).Where(x => x.State == true)
                         .Include(i => i.InvoiceDetails)
                         .Include(c => c.Customer)
                         .Include(p => p.Payments)
                         .FirstOrDefault();
        }

        public override IEnumerable<Invoice> GetAll()
        {
            return _entities.Set<Invoice>().Where(x => x.State == true)
                .Include(x => x.Customer)
                .Include(x => x.Payments)
                .AsEnumerable();
        }

        public Invoice GetByIdAttention(long idAttention)
        {
            return _dbset.Where(x => x.AttentionId == idAttention && x.State == true)
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

        public int GetNumberSerie(string Series)
        {
            int sunatNumber = _entities.Database.SqlQuery<int>("Select Top 1 Cast(SunatNumber as int) from [dbo].[Invoice] where State = 1 and Series = '" + Series + "' Order By 1 desc").FirstOrDefault();
            
            if (sunatNumber != 0)
            {
                sunatNumber = sunatNumber + 1;
            }
            else
            {
                sunatNumber = 1;
            }
            return sunatNumber;
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
