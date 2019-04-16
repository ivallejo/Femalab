using Femalab.Model.Entities;
using Femalab.Repository;
using Femalab.Repository.Master;

namespace Femalab.Service.MasterService
{
    public class PatientService : EntityService<Patient>, IPatientService
    {
        IUnitOfWork _unitOfWork;
        IPatientRepository _patientRepository;

        public PatientService(IUnitOfWork unitOfWork, IPatientRepository patientRepository)
            : base(unitOfWork, patientRepository)
        {
            _unitOfWork = unitOfWork;
            _patientRepository = patientRepository;
        }
        public Patient GetById(long Id)
        {
            return _patientRepository.GetById(Id);
        }
    }
}
