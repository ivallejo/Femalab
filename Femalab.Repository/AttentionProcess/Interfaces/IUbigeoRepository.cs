using Femalab.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Femalab.Repository.Master.Interfaces
{
    public interface IUbigeoRepository : IGenericRepository<Ubigeo>
    {
        IEnumerable<Ubigeo> GetAll_Departments();
        IEnumerable<Ubigeo> GetAll_Province(string codeDepartment);
        IEnumerable<Ubigeo> GetAll_District(string codeDepartmentProvince);
    }
}
