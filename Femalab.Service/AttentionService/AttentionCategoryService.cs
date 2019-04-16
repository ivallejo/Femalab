using Femalab.Model.Entities;
using Femalab.Repository;
using Femalab.Repository.AttentionProcess;

namespace Femalab.Service.AttentionService
{
    public class AttentionCategoryService : EntityService<AttentionCategory>, IAttentionCategoryService
    {
        IUnitOfWork _unitOfWork;
        IAttentionCategoryRepository _attentionCategoryRepository;

        public AttentionCategoryService(IUnitOfWork unitOfWork, IAttentionCategoryRepository attentionCategoryRepository) : base(unitOfWork, attentionCategoryRepository)
        {
            _unitOfWork = unitOfWork;
            _attentionCategoryRepository = attentionCategoryRepository;
        }
    }
}
