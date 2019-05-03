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
            return _dbset.Where(x => x.AttentionId == id)
                         .Include(p => p.InvoiceDetails)
                         .Include(d => d.Customer)
                         .FirstOrDefault();
        }
    }
}
