

using Femalab.Model.Entities;
using System.Collections.Generic;

namespace Femalab.Repository.AttentionProcess
{
    public interface IAttentionRepository :IGenericRepository<Attention>
    {
        Attention GetById(long id);
        IEnumerable<Attention> GetAllPending();
        void CreateAttention(Attention model);
        void UpdateAttention(Attention model);
    }
}
