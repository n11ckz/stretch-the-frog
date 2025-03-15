using UnityEngine;

namespace Project
{
    public class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
#if UNITY_EDITOR
            Debug.Log(message);
#endif
        }
    }
}
