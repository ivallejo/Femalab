using Femalab.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Femalab.Service.Master.Interfaces
{
    public interface IUbigeoService : IEntityService<Ubigeo>
    {
        IEnumerable<Ubigeo> GetAll_Departments();
        IEnumerable<Ubigeo> GetAll_Province(string codeDepartment);
        IEnumerable<Ubigeo> GetAll_District(string codeDepartmentProvince);
    }
}
