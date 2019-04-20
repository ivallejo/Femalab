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
    public class AttentionDetailsRepository : GenericRepository<AttentionDetails>, IAttentionDetailsRepository
    {
        public AttentionDetailsRepository(IIndex<string, DbContext> context) : base(context)
        {
        }
    }
}
