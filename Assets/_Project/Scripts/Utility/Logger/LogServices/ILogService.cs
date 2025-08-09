using System.Runtime.CompilerServices;

namespace Project
{
    public interface ILogService
    {
        public void Log<T>(T message, LogMode logMode = LogMode.Standard,
            [CallerFilePath] string filePath = null, [CallerMemberName] string memberName = null);
    }
}
