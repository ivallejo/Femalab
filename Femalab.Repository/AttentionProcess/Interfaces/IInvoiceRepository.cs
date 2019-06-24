using Femalab.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Femalab.Repository.AttentionProcess.Interfaces
{
    public interface IInvoiceRepository : IGenericRepository<Invoice>
    {
        Invoice GetById(long id);
        Invoice GetByIdAttention(long idAttention);
        void UpdateInvoice(Invoice model);
        int GetNumberSerie(string Series);
    }   
}
