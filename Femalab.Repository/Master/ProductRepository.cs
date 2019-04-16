using Autofac.Features.Indexed;
using Femalab.Model.Entities;
using Femalab.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Femalab.Repository.Master
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(IIndex<String, DbContext> context)
            : base(context)
        {

        }
        public override IEnumerable<Product> GetAll()
        {
            return _entities.Set<Product>()
                .Include(x => x.Category)
                .Include(x => x.Specialty)
                .AsEnumerable();
        }

        public Product GetById(long id)
        {
            return _dbset.Where(x => x.Id == id).FirstOrDefault();
        }
    }
}
