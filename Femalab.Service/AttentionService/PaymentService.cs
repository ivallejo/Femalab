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
    public class PaymentService : EntityService<Payment>, IPaymentService
    {
        IUnitOfWork _unitOfWork;
        IPaymentRepository _paymentRepository;

        public PaymentService(IUnitOfWork unitOfWork, IPaymentRepository paymentRepository)
            : base(unitOfWork, paymentRepository)
        {
            _unitOfWork = unitOfWork;
            _paymentRepository = paymentRepository;
        }

        public void UpdatePayment(Payment entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            _paymentRepository.Edit(entity);
        }
    }
}
