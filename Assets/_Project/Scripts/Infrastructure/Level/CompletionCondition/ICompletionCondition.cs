using System;

namespace Project
{
    public interface ICompletionCondition
    {
        public event Action<bool> Checked;
    }
}
