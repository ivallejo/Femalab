using Femalab.Model.Entities;
using System.Collections.Generic;

namespace Femalab.Service.MasterService
{
    public interface IProductService : IEntityService<Product>
    {
        IEnumerable<Product> GetAll_Attentention();
        Product GetById(long Id);   
    }
}
