using Femalab.Model.Entities;
using Femalab.Repository;
using Femalab.Repository.Master;

namespace Femalab.Service.MasterService
{
    public class DoctorService : EntityService<Doctor>, IDoctorService
    {
        IUnitOfWork _unitOfWork;
        IDoctorRepository _doctorRepository;
        public DoctorService(IUnitOfWork unitOfWork, IDoctorRepository doctorRepository) : base(unitOfWork, doctorRepository)
        {
            _unitOfWork = unitOfWork;
            _doctorRepository = doctorRepository;
        }
    }
}
