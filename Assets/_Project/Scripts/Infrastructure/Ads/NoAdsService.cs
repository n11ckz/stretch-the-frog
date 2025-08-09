using Cysharp.Threading.Tasks;
using System.Threading;

namespace Project
{
    public class NoAdsService : IAdsService
    {
        private bool _hasDisplayedOnce;

        public UniTask TryShowInterstitialAdAsync(CancellationToken cancellationToken)
        {
            if (_hasDisplayedOnce == false)
            {
                Logger.Log("Ads should be displayed here :p");
                _hasDisplayedOnce = true;
            }

            return UniTask.CompletedTask;
        }
    }
}
