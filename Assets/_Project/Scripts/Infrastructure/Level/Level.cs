using System;
using UnityEngine;

namespace Project
{
    public class Level : IDisposable
    {
        private readonly ICompletionCondition _completionCondition;
        private readonly BetweenScenesMediator _betweenScenesMediator;
        private readonly SavedProgressStorage _progressStorage;

        private bool _isDisposed;

        public Level(ICompletionCondition completionCondition, BetweenScenesMediator betweenScenesMediator, SavedProgressStorage progressStorage)
        {
            _completionCondition = completionCondition;
            _betweenScenesMediator = betweenScenesMediator;
            _progressStorage = progressStorage;
        }

        public void WaitForCompletion() =>
            _completionCondition.Checked += Complete;

        public void Dispose()
        {
            if (_isDisposed == true)
                return;

            _completionCondition.Checked -= Complete;
            _isDisposed = true;
        }

        private void Complete(bool isSuccessfully)
        {
            _betweenScenesMediator.NotifyAboutLevelCompletion(isSuccessfully);

            if (isSuccessfully == true)
                _progressStorage.Save();

            Dispose();
        }
    }
}
