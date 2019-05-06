using Femalab.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Femalab.Service.AttentionService.Interfaces
{
    public interface IPaymentService : IEntityService<Payment>
    {
        void UpdatePayment(Payment entity);
    }
}
