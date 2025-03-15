using System;

namespace Project
{
    public class BetweenScenesMediator
    {
        public event Action<bool> LevelCompleted;

        public void NotifyAboutLevelCompletion(bool isSuccessfully) =>
            LevelCompleted?.Invoke(isSuccessfully);
    }
}
