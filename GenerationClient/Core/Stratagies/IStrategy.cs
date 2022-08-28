using System.Threading.Tasks;

namespace GenerationClient.Core.Stratagies
{
    public interface IStrategy
    {
        int OperationId { get;}
        Task RunAsync();
    }
}
