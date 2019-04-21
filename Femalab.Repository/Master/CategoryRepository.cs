using Autofac.Features.Indexed;
using Femalab.Model.Entities;
using Femalab.Repository.Master.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Femalab.Repository.Master
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(IIndex<string, DbContext> context) : base(context)
        {
        }
    }
}
