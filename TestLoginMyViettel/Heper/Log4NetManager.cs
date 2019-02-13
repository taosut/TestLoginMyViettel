using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using log4net;

namespace TestLoginMyViettel.Heper
{
    /// <summary>
    ///     Lớp hỗ trợ ghi log hệ thống.
    ///     Limk tham khảo: https://stackify.com/log4net-guide-dotnet-logging/
    /// </summary>
    public abstract class Log4NetManager
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        ///     Hàm ghi log ra file.
        /// </summary>
        /// <param name="message">Nội dung log</param>
        /// <param name="level">Cấp độ của log</param>
        /// <param name="filePath">File xảy ra log</param>
        /// <param name="callerMemberName">Phương thức xảy ra log</param>
        /// <param name="line">Vị trí xảy ra log</param>
        public static void LogToFile(object message, LogLevel level = LogLevel.Debug, [CallerFilePath] string filePath = "", [CallerMemberName] string callerMemberName = "",
                                     [CallerLineNumber] int line = 0)
        {
            var fp = Path.GetFileName(filePath);

            switch (level)
            {
                case LogLevel.Error:
                    Log.Error($"[{fp} > {callerMemberName}, line {line}] {Environment.NewLine}{message}");
                    return;
                case LogLevel.Debug:
                    Log.Debug($"[{fp} > {callerMemberName}, line {line}] {Environment.NewLine}{message}");
                    return;
                case LogLevel.Informative:
                    Log.Info($"[{fp} > {callerMemberName}, line {line}] {Environment.NewLine}{message}");
                    return;
                case LogLevel.Warning:
                    Log.Warn($"[{fp} > {callerMemberName}, line {line}] {Environment.NewLine}{message}");
                    return;
            }
        }
    }

    /// <summary>
    ///     The severity of the log message
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        ///     Developer-specific information
        /// </summary>
        Debug = 1,

        /// <summary>
        ///     Verbose information
        /// </summary>
        Verbose = 2,

        /// <summary>
        ///     General information
        /// </summary>
        Informative = 3,

        /// <summary>
        ///     A warning
        /// </summary>
        Warning = 4,

        /// <summary>
        ///     An error
        /// </summary>
        Error = 5,

        /// <summary>
        ///     A success
        /// </summary>
        Success = 6
    }
}