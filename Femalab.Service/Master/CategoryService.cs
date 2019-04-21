using Femalab.Model.Entities;
using Femalab.Repository;
using Femalab.Repository.Master.Interfaces;
using Femalab.Service.Master.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Femalab.Service.Master
{
    public class CategoryService : EntityService<Category>, ICategoryService
    {
        IUnitOfWork _unitOfWork;
        ICategoryRepository _categoryRepository;

        public CategoryService(IUnitOfWork unitOfWork, ICategoryRepository categoryRepository) 
            : base(unitOfWork, categoryRepository)
        {
            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository;
        }
    }
}
