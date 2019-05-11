using Femalab.Model.Entities;
using System.Collections.Generic;

namespace Femalab.Service.AttentionService
{
    public interface IAttentionService : IEntityService<Attention>
    {
        Attention GetById(long Id);
        IEnumerable<Attention> GetAllPending();
        void CreateAttention(Attention model);
        void UpdateAttention(Attention model);
    }
}
