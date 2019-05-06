using Autofac.Features.Indexed;
using Femalab.Model.Entities;
using Femalab.Repository.Master.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Femalab.Repository.Master
{
    public class UbigeoRepository : GenericRepository<Ubigeo>, IUbigeoRepository
    {
        public UbigeoRepository(IIndex<string, DbContext> context) : base(context)
        {
        }

        public IEnumerable<Ubigeo> GetAll_Departments()
        {
            return _entities.Set<Ubigeo>()
                .Where(x=> x.Code.EndsWith("0101"))
                .AsNoTracking()
                .AsEnumerable();
        }
        public IEnumerable<Ubigeo> GetAll_Province(string codeDepartment)
        {
            return _entities.Set<Ubigeo>()
                .Where(x => x.Code.StartsWith(codeDepartment) && x.Code.EndsWith("01"))
                .AsNoTracking()
                .AsEnumerable();
        }
        public IEnumerable<Ubigeo> GetAll_District(string codeDepartmentProvince)
        {
            return _entities.Set<Ubigeo>()
                .Where(x => x.Code.StartsWith(codeDepartmentProvince))
                .AsNoTracking()
                .AsEnumerable();
        }
    }
}
