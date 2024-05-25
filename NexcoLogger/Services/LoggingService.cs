/*
```csharp
NexcoLogger - A Robust Logging Library for WinUI 3

 NexcoLogger is a robust logging library for WinUI 3 applications, providing detailed logging functionality with 
 features such as log filtering, exporting, and structured logging.

 Features:
 - Thread-Safe Logging: Ensures all interactions with the UI are thread-safe.
 - Log Filtering: Filter log entries by type or source.
 - Log Exporting: Export log entries to a CSV file.
 - Configuration Options: Configure log retention policies and maximum log entry limits.
 - Structured Logging: Enhanced log messages with structured data fields.

 Usage Instructions:

 1. Initialize and Configure the Logging Service:

 var serviceCollection = new ServiceCollection();
 serviceCollection.AddNexLog();
 var serviceProvider = serviceCollection.BuildServiceProvider();

 var loggingService = serviceProvider.GetRequiredService<ILoggingService>();
 var loggingOptions = new LoggingOptions
 {
     MaxLogEntries = 1000,
     LogRetentionPeriod = TimeSpan.FromDays(7)
 };
 loggingService.ConfigureLogging(loggingOptions);

 2. Log Messages:

 loggingService.Log("Application started", LogType.Info, "Main");
 loggingService.Log("An unexpected error occurred", LogType.Error, "Main", "StackTrace details");

 3. Start and End Operations:

 loggingService.StartOperation("databaseLoad");
 // Perform the database load operation
 loggingService.EndOperation("databaseLoad", "Database loaded successfully", LogType.Success, "DatabaseService");

 4. Export Logs:

 loggingService.ExportLogsToCsv("C:\\Logs\\log.csv");

 5. Filter Logs:

 var errorLogs = logsViewModel.FilterLogEntriesByType(LogType.Error);
 var mainLogs = logsViewModel.FilterLogEntriesBySource("Main");

 Classes and Methods:

 LogEntry: Represents a log entry with detailed information.
 LogType: Represents the type of a log entry.
 ILoggingService: Provides methods for logging messages with varying levels of severity.
 LoggingService: Provides methods for logging messages and tracking operation times.
 LogsViewModel: ViewModel for managing log entries.
 LogTypeToColorConverter: Converts LogType values to corresponding colors.

 Contributions:
 Feel free to contribute to the project by submitting issues or pull requests.

 License:
 This project is licensed under the MIT License.
*/

using Microsoft.UI.Dispatching;
using NexcoLogger.Models;
using NexcoLogger.ViewModels;
using System.Collections.Concurrent;

namespace NexcoLogger.Services
{
    /// <summary>
    /// Provides methods for logging messages and tracking operation times.
    /// </summary>
    public class LoggingService : ILoggingService
    {
        private readonly LogsViewModel _logsViewModel;
        private readonly ConcurrentDictionary<string, DateTime> _operationStartTimes = new();
        private LoggingOptions _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggingService"/> class.
        /// </summary>
        /// <param name="logsViewModel">The ViewModel responsible for managing log entries.</param>
        public LoggingService(LogsViewModel logsViewModel)
        {
            _logsViewModel = logsViewModel;
            _options = new LoggingOptions();
        }

        /// <summary>
        /// Logs a message with detailed context including the source, execution time, and optional call stack.
        /// </summary>
        /// <param name="message">The message describing the log event.</param>
        /// <param name="type">The type of the log entry (e.g., Error, Warning).</param>
        /// <param name="source">The component or module from which the log originates.</param>
        /// <param name="callStack">The stack trace of the call leading to the log entry, if applicable.</param>
        /// <param name="elapsedTime">The duration of the operation leading to the log entry, if measured.</param>
        /// <remarks>
        /// This method is thread-safe and can be called from any part of the application.
        /// </remarks>
        public void Log(string message, LogType type, string source, string callStack = "", TimeSpan? elapsedTime = null)
        {
            DispatcherQueue.GetForCurrentThread().TryEnqueue(() =>
            {
                _logsViewModel.AddLogEntry(message, type, source, callStack, elapsedTime);
                EnforceLogRetentionPolicy();
            });
        }

        /// <summary>
        /// Starts timing an operation with a unique identifier.
        /// </summary>
        /// <param name="operationId">A unique identifier for the operation.</param>
        public void StartOperation(string operationId)
        {
            _operationStartTimes[operationId] = DateTime.Now;
        }

        /// <summary>
        /// Ends timing an operation and logs the result with the elapsed time.
        /// </summary>
        /// <param name="operationId">The unique identifier of the operation to end.</param>
        /// <param name="message">The message describing the operation's outcome.</param>
        /// <param name="type">The type of log entry.</param>
        /// <param name="source">The source of the log entry.</param>
        public void EndOperation(string operationId, string message, LogType type, string source)
        {
            if (_operationStartTimes.TryGetValue(operationId, out DateTime startTime))
            {
                TimeSpan elapsedTime = DateTime.Now - startTime;
                Log(message, type, source, elapsedTime: elapsedTime);
                _operationStartTimes.TryRemove(operationId, out _);
            }
            else
            {
                Log("Failed to find start time for operation.", LogType.Error, source);
            }
        }

        /// <summary>
        /// Exports the current log entries to a specified file in CSV format.
        /// </summary>
        /// <param name="filePath">The path of the file to which the log entries will be exported.</param>
        public void ExportLogsToCsv(string filePath)
        {
            var lines = new List<string>
            {
                "Timestamp,Message,Type,Source,CallStack,ElapsedTime"
            };
            lines.AddRange(_logsViewModel.Entries.Select(entry =>
                $"{entry.Timestamp},{entry.Message},{entry.Type},{entry.Source},{entry.CallStack},{entry.ElapsedTime}"));
            File.WriteAllLines(filePath, lines);
        }

        /// <summary>
        /// Configures the logging service with specified options.
        /// </summary>
        /// <param name="options">The logging options to configure.</param>
        public void ConfigureLogging(LoggingOptions options)
        {
            _options = options;
        }

        /// <summary>
        /// Enforces the log retention policy by removing old log entries if the limit is exceeded.
        /// </summary>
        private void EnforceLogRetentionPolicy()
        {
            if (_logsViewModel.Entries.Count > _options.MaxLogEntries)
            {
                while (_logsViewModel.Entries.Count > _options.MaxLogEntries)
                {
                    _logsViewModel.Entries.RemoveAt(0);
                }
            }

            var threshold = DateTime.Now - _options.LogRetentionPeriod;
            var oldEntries = _logsViewModel.Entries.Where(e => e.Timestamp < threshold).ToList();
            foreach (var entry in oldEntries)
            {
                _logsViewModel.Entries.Remove(entry);
            }
        }
    }
}
