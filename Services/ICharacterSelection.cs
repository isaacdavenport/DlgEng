

using System.Threading.Tasks;

namespace DialogEngine.Services
{
    public interface ICharacterSelection
    {
         Task StartSelectionService();
         void Stop();
    }
}
