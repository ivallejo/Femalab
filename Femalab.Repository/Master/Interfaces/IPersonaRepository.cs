using Femalab.Model.Persistence;

namespace Femalab.Repository.Master
{
    public interface IPersonaRepository
    {
        PERSONA GetById(string id);
    }
}
