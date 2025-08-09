using Cysharp.Threading.Tasks;
using System;
using System.Threading;

namespace Project
{
    public class LevelCycle
    {
        public event Action<bool> Completed;
        public event Action Disposed;

        private readonly PauseService _pauseService;
        // private readonly SaveLoadSystem _saveLoadSystem

        public LevelCycle(PauseService pauseService)
        {
            _pauseService = pauseService;
        }

        public async UniTaskVoid Start(ILevelCompletionStrategy completionStrategy, CancellationToken cancellationToken)
        {
            bool isSuccessfully = await completionStrategy.WaitForCompletionAsync(cancellationToken);
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken: cancellationToken);

            Complete(isSuccessfully);
        }

        public void Dispose()
        {
            _pauseService.Resume();
            Disposed?.Invoke();
        }

        private void Complete(bool isSuccessfully)
        {
            _pauseService.Pause();
            Completed?.Invoke(isSuccessfully);
            Logger.Log($"Result - {isSuccessfully}");
        }
    }
}
