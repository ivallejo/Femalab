using Femalab.Model.Entities;
using Femalab.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Femalab.Repository.Master
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        IEnumerable<Product> GetAll_Attentention();

        Product GetById(long id);
    }
}
