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
    public class UbigeoService : EntityService<Ubigeo>, IUbigeoService
    {
        IUnitOfWork _unitOfWork;
        IUbigeoRepository _ubigeorepository;

        public UbigeoService(IUnitOfWork unitOfWork, IUbigeoRepository ubigeorepository)
            : base(unitOfWork, ubigeorepository)
        {
            _unitOfWork = unitOfWork;
            _ubigeorepository = ubigeorepository;
        }

        public IEnumerable<Ubigeo> GetAll_Departments()
        {
            return _ubigeorepository.GetAll_Departments();
        }
        public IEnumerable<Ubigeo> GetAll_Province(string codeDepartment)
        {
            return _ubigeorepository.GetAll_Province(codeDepartment);
        }
        public IEnumerable<Ubigeo> GetAll_District(string codeDepartmentProvince)
        {
            return _ubigeorepository.GetAll_District(codeDepartmentProvince);
        }
    }
}
