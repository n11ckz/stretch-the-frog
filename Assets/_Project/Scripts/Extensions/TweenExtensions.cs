using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;

namespace Project
{
    public static class TweenExtensions
    {
        public static async UniTask ContinueWith(this Tween tween, Func<UniTask> additionalTask)
        {
            await tween.ToUniTask();
            await additionalTask();
        }
    }
}
