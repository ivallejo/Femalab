using Femalab.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Femalab.Service.AttentionService.Interfaces
{
    public interface IInvoiceService : IEntityService<Invoice>
    {
        Invoice GetById(long Id);
        Invoice GetByIdAttention(long IdAttention);
        void UpdateInvoice(Invoice entity);
        int GetNumberSerie(string Series);
    }
}
