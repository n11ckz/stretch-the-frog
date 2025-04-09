using System.Collections.Generic;

namespace Project
{
    public class ProgressStorage
    {
        private readonly ISaveStrategy _saveStrategy = new SaveInPrefsStrategy();
        private readonly List<IProgressListener> _listeners = new List<IProgressListener>();

        private Progress _progress;

        public void Load()
        {
            _progress = _saveStrategy.Load();

            foreach (IProgressListener listener in _listeners)
                listener.Load(_progress);
        }

        public void Save()
        {
            foreach (IProgressListener listener in _listeners)
                listener.Save(_progress);

            _saveStrategy.Save(_progress);
        }

        public void AddListener(IProgressListener listener) =>
            _listeners.Add(listener);

        public void RemoveListener(IProgressListener listener) =>
            _listeners.Remove(listener);
    }
}
