namespace NexcoLogger.Models
{
    /// <summary>
    /// Represents a log entry with detailed information.
    /// </summary>
    public class LogEntry
    {
        public DateTime Timestamp { get; } = DateTime.Now;
        public string Message { get; set; }
        public LogType Type { get; set; }
        public string CallStack { get; set; }
        public TimeSpan? ElapsedTime { get; set; }
        public string Source { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LogEntry"/> class.
        /// </summary>
        /// <param name="message">The log message.</param>
        /// <param name="type">The type of log entry.</param>
        /// <param name="source">The source of the log entry.</param>
        /// <param name="callStack">The call stack information, if any.</param>
        /// <param name="elapsedTime">The elapsed time, if any.</param>
        public LogEntry(string message, LogType type, string source, string callStack = "", TimeSpan? elapsedTime = null)
        {
            Message = message;
            Type = type;
            Source = source;
            CallStack = callStack;
            ElapsedTime = elapsedTime;
        }
    }

    /// <summary>
    /// Represents the type of a log entry.
    /// </summary>
    public enum LogType
    {
        Info,
        Warning,
        Error,
        Exception,
        Success
    }
}
