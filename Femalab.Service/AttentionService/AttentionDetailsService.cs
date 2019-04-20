using Femalab.Model.Entities;
using Femalab.Repository;
using Femalab.Repository.AttentionProcess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Femalab.Service.AttentionService
{
    public class AttentionDetailsService : EntityService<AttentionDetails>, IAttentionDetailsService
    {
        IUnitOfWork _unitOfWork;
        IAttentionDetailsRepository _attentionDetailsRepository;

        public AttentionDetailsService(IUnitOfWork unitOfWork, IAttentionDetailsRepository attentionDetailsRepository) 
            : base(unitOfWork, attentionDetailsRepository)
        {
            _unitOfWork = unitOfWork;
            _attentionDetailsRepository = attentionDetailsRepository;
        }
    }
}
