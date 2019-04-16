using Femalab.Model.Entities;

namespace Femalab.Service.MasterService
{
    public interface IPatientService : IEntityService<Patient>
    {
        Patient GetById(long Id);
    }
}
