using System;

namespace Project
{
    public interface ICompleteCondition
    {
        public event Action<bool> Completed;
    }

    public interface IWinCondition
    {
        public event Action<bool> Won;
    }
}
