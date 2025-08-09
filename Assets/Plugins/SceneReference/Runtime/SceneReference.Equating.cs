using System;

namespace n11ckz.SceneReference
{
    public partial class SceneReference : IEquatable<SceneReference>
    {
        public bool Equals(SceneReference other)
        {
            if (other == null)
                return false;

            return BuildIndex == other.BuildIndex && Name == other.Name;
        }

        public override bool Equals(object obj) =>
            Equals(obj as SceneReference);

        public override int GetHashCode() =>
            HashCode.Combine(BuildIndex, Name);
    }
}
