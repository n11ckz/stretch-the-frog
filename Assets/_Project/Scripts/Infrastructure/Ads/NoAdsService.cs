using Cysharp.Threading.Tasks;
using System.Threading;

namespace Project
{
    public class NoAdsService : IAdsService
    {
        public UniTask TryShowInterstitialAdAsync(CancellationToken cancellationToken)
        {
            Logger.Log("Ads should be displayed here :p");
            return UniTask.CompletedTask;
        }
    }
}
