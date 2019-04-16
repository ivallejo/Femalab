using Femalab.Model.Persistence;

namespace Femalab.Service.MasterService
{
    public interface IPersonaService
    {
        PERSONA GetById(string Id);
    }
}
