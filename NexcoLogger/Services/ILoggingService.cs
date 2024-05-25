using NexcoLogger.Models;

namespace NexcoLogger.Services
{
    /// <summary>
    /// Provides methods for logging messages with varying levels of severity.
    /// </summary>
    public interface ILoggingService
    {
        /// <summary>
        /// Logs a message with additional details such as source and timing.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="type">The severity level of the log entry (e.g., Info, Warning, Error).</param>
        /// <param name="source">The source of the log entry, typically the class or module.</param>
        /// <param name="callStack">Optional. The call stack information if relevant.</param>
        /// <param name="elapsedTime">Optional. The time elapsed for the logged operation, if applicable.</param>
        void Log(string message, LogType type, string source, string callStack = "", TimeSpan? elapsedTime = null);

        /// <summary>
        /// Starts timing an operation with a unique identifier.
        /// </summary>
        /// <param name="operationId">A unique identifier for the operation.</param>
        void StartOperation(string operationId);

        /// <summary>
        /// Ends timing an operation and logs the result with the elapsed time.
        /// </summary>
        /// <param name="operationId">The unique identifier of the operation to end.</param>
        /// <param name="message">The message describing the operation's outcome.</param>
        /// <param name="type">The type of log entry.</param>
        /// <param name="source">The source of the log entry.</param>
        void EndOperation(string operationId, string message, LogType type, string source);

        /// <summary>
        /// Exports the current log entries to a specified file in CSV format.
        /// </summary>
        /// <param name="filePath">The path of the file to which the log entries will be exported.</param>
        void ExportLogsToCsv(string filePath);

        /// <summary>
        /// Configures the logging service with specified options.
        /// </summary>
        /// <param name="options">The logging options to configure.</param>
        void ConfigureLogging(LoggingOptions options);
    }

    /// <summary>
    /// Options for configuring the logging service.
    /// </summary>
    public class LoggingOptions
    {
        /// <summary>
        /// Gets or sets the maximum number of log entries to retain.
        /// </summary>
        public int MaxLogEntries { get; set; } = 1000;

        /// <summary>
        /// Gets or sets the log retention period.
        /// </summary>
        public TimeSpan LogRetentionPeriod { get; set; } = TimeSpan.FromDays(7);
    }
}