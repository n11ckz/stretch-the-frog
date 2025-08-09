using Cysharp.Threading.Tasks;
using System.Threading;

namespace Project
{
    public interface IScreenCurtain
    {
        public UniTask ShowAsync(CancellationToken cancellationToken);
        public UniTask HideAsync(CancellationToken cancellationToken);
    }
}
