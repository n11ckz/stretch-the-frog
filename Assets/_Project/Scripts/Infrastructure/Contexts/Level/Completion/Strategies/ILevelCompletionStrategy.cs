using Cysharp.Threading.Tasks;
using System.Threading;

namespace Project
{
    public interface ILevelCompletionStrategy
    {
        public UniTask<bool> WaitForCompletionAsync(CancellationToken cancellationToken);
    }
}
