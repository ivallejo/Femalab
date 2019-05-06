using Femalab.Model.Entities;
using Femalab.Repository;
using Femalab.Repository.AttentionProcess.Interfaces;
using Femalab.Service.AttentionService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Femalab.Service.AttentionService
{
    public class InvoiceService : EntityService<Invoice>, IInvoiceService
    {
        IUnitOfWork _unitOfWork;
        IInvoiceRepository _invoiceRepository;

        public InvoiceService(IUnitOfWork unitOfWork, IInvoiceRepository invoiceRepository)
            : base(unitOfWork, invoiceRepository)
        {
            _unitOfWork = unitOfWork;
            _invoiceRepository = invoiceRepository;
        }

        public Invoice GetById(long Id)
        {
            return _invoiceRepository.GetById(Id);
        }
        public Invoice GetByIdAttention(long IdAttention)
        {
            return _invoiceRepository.GetByIdAttention(IdAttention);
        }

        public  void UpdateInvoice(Invoice entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            _invoiceRepository.UpdateInvoice(entity);
            _unitOfWork.commit();
        }

    }
}
