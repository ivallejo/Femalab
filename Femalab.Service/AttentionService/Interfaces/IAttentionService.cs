using Femalab.Model.Entities;

namespace Femalab.Service.AttentionService
{
    public interface IAttentionService : IEntityService<Attention>
    {
        Attention GetById(long Id);

        void CreateAttention(Attention model);
        void UpdateAttention(Attention model);
    }
}
