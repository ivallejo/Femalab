using Femalab.Model.Entities;
using Femalab.Repository;
using Femalab.Repository.AttentionProcess;
using System.Collections.Generic;

namespace Femalab.Service.AttentionService
{
    public class AttentionService : EntityService<Attention>, IAttentionService
    {
        IUnitOfWork _unitOfWork;
        IAttentionRepository _attentionRepository;

        public AttentionService(IUnitOfWork unitOfWork, IAttentionRepository attentionRepository) 
            : base(unitOfWork, attentionRepository)
        {
            _unitOfWork = unitOfWork;
            _attentionRepository = attentionRepository;
        }
        public IEnumerable<Attention> GetAllPending()
        {
            return _attentionRepository.GetAllPending();
        }

        public void CreateAttention(Attention model)
        {
            _attentionRepository.CreateAttention(model);
            _attentionRepository.Save();
        }
        public void UpdateAttention(Attention model)
        {
            _attentionRepository.UpdateAttention(model);
            _attentionRepository.Save();
        }

        public Attention GetById(long Id)
        {
            return _attentionRepository.GetById(Id);
        }

       
    }
}
