using Autofac.Features.Indexed;
using Femalab.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Femalab.Repository.AttentionProcess
{
    public class AttentionTypeRepository : GenericRepository<AttentionType>, IAttentionTypeRepository
    {
        public AttentionTypeRepository(IIndex<String, DbContext> context) : base(context)
        {
        }
    }
}
