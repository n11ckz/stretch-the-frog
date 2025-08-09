using DG.Tweening;

namespace Project
{
    public static class TweenExtensions
    {
        public static T WriteInField<T>(this T tween, ref T field) where T : Tween
        {
            field = tween;
            return tween;
        }
    }
}
