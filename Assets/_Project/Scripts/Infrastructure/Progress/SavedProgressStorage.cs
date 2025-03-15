using System.Collections.Generic;

namespace Project
{
    public class SavedProgressStorage
    {
        private readonly List<IProgressListener> _listeners = new List<IProgressListener>();
        
        public SavedProgress Progress { get; private set; }

        public void Load()
        {
            Progress = new SavedProgress() { UnlockedSceneBuildIndexes = new HashSet<int>() };

            foreach (IProgressListener listener in _listeners)
                listener.Load(Progress);
        }

        public void Save()
        {
            foreach (IProgressListener listener in _listeners)
                listener.Save(Progress);
        }

        public void AddListener(IProgressListener listener) =>
            _listeners.Add(listener);

        public void RemoveListener(IProgressListener listener) =>
            _listeners.Remove(listener);
    }
}
