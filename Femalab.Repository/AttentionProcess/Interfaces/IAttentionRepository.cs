

using Femalab.Model.Entities;

namespace Femalab.Repository.AttentionProcess
{
    public interface IAttentionRepository :IGenericRepository<Attention>
    {
        Attention GetById(long id);

        void CreateAttention(Attention model);

        void UpdateAttention(Attention model);
    }
}
