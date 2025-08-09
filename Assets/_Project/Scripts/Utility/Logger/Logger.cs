using System;
using System.Runtime.CompilerServices;

namespace Project
{
    public static class Logger
    {
        public static bool HasServiceInstance => _logService != null;

        private static ILogService _logService;

        public static void Initialize(ILogService logService) =>
            _logService = logService;

        public static void Log<T>(T message, LogMode logMode = LogMode.Standard,
            [CallerFilePath] string filePath = null, [CallerMemberName] string memberName = null)
        {
            if (HasServiceInstance == false)
                throw new NullReferenceException($"Instance of <{nameof(ILogService)}> is null");

            _logService.Log(message, logMode, filePath, memberName);
        }
    }
}
