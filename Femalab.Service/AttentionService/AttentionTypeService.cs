using Femalab.Model.Entities;
using Femalab.Repository;
using Femalab.Repository.AttentionProcess;

namespace Femalab.Service.AttentionService
{
    public class AttentionTypeService : EntityService<AttentionType>, IAttentionTypeService
    {
        IUnitOfWork _unitOfWork;
        IAttentionTypeRepository _attentionTypeRepository;

        public AttentionTypeService(IUnitOfWork unitOfWork, IAttentionTypeRepository attentionTypeRepository) : base(unitOfWork, attentionTypeRepository)
        {
            _unitOfWork = unitOfWork;
            _attentionTypeRepository = attentionTypeRepository;
        }
    }
}
