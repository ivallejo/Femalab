using Femalab.Model.Entities;
using Femalab.Repository;
using Femalab.Repository.Master;
using System.Collections.Generic;

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
        public IEnumerable<Product> GetAll_Attentention()
        {
            return _productRepository.GetAll_Attentention();
        }
        public Product GetById(long Id)
        {
            return _productRepository.GetById(Id);
        }
    }
}
