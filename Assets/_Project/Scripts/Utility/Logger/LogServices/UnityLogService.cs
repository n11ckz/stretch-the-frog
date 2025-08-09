using System;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Project
{
    public class UnityLogService : ILogService
    {
        public void Log<T>(T message, LogMode logMode = LogMode.Standard,
            [CallerFilePath] string filePath = null, [CallerMemberName] string memberName = null)
        {
            if (Debug.isDebugBuild == false)
                return;
            
            string file = Path.GetFileNameWithoutExtension(filePath);

            Action<string> log = logMode switch
            {
                LogMode.Warning => Debug.LogWarning,
                LogMode.Error => Debug.LogError,
                _ => Debug.Log
            };

            log.Invoke($"{file}.{memberName}(): {message}");
        }
    }
}
