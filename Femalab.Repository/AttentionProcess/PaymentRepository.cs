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
    public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(IIndex<String, DbContext> context)
          : base(context)
        {
        }

    }
}
