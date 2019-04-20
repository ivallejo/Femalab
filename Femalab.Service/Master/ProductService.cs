using Femalab.Model.Entities;
using Femalab.Repository;
using Femalab.Repository.Master;

namespace Femalab.Service.MasterService
{
    public class ProductService : EntityService<Product>, IProductService
    {
        IUnitOfWork _unitOfWork;
        IProductRepository _productRepository;

        public ProductService(IUnitOfWork unitOfWork, IProductRepository productRepository) : base(unitOfWork, productRepository)
        {
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
        }
        public Product GetById(long Id)
        {
            return _productRepository.GetById(Id);
        }
    }
}
