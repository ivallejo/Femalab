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
    public class SpecialtyService : EntityService<Specialty>, ISpecialtyService
    {
        IUnitOfWork _unitOfWork;
        ISpecialtyRepository _specialtyrepository;

        public SpecialtyService(IUnitOfWork unitOfWork, ISpecialtyRepository specialtyrepository) 
            : base(unitOfWork, specialtyrepository)
        {
            _unitOfWork = unitOfWork;
            _specialtyrepository = specialtyrepository;
        }
    }
}
