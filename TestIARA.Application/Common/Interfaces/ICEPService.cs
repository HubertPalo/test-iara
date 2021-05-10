using System.Threading.Tasks;
using TestIARA.Application.Common.Models;

namespace TestIARA.Application.Common.Interfaces
{
    public interface ICEPService
    {
        public CEPDto SearchCEP(string CEP);
        
    }
}
