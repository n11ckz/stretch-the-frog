using Cysharp.Threading.Tasks;
using System.Threading;

namespace Project
{
    public interface IAdsService
    {
        public UniTask TryShowInterstitialAdAsync(CancellationToken cancellationToken);
    }
}
