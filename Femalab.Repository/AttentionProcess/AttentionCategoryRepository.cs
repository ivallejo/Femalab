
using Femalab.Model.Entities;
using Femalab.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Femalab.Repository.AttentionProcess
{
    public class AttentionCategoryRepository : GenericRepository<AttentionCategory>, IAttentionCategoryRepository
    {
        public AttentionCategoryRepository(DbContext context) : base(context)
        {
        }


    }
}
