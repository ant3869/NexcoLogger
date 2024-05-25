using NexcoLogger.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace NexcoLogger.ViewModels
{
    /// <summary>
    /// ViewModel for managing log entries.
    /// </summary>
    public class LogsViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<LogEntry> Entries { get; } = new ObservableCollection<LogEntry>();

        /// <summary>
        /// Adds a log entry to the collection.
        /// </summary>
        /// <param name="message">The log message.</param>
        /// <param name="type">The type of the log entry.</param>
        /// <param name="source">The source of the log entry.</param>
        /// <param name="callStack">The call stack information, if any.</param>
        /// <param name="elapsedTime">The elapsed time, if any.</param>
        public void AddLogEntry(string message, LogType type, string source, string callStack = "", TimeSpan? elapsedTime = null)
        {
            Entries.Add(new LogEntry(message, type, source, callStack, elapsedTime));
            OnPropertyChanged(nameof(Entries));
        }

        /// <summary>
        /// Filters log entries by type.
        /// </summary>
        /// <param name="type">The type of log entries to filter.</param>
        /// <returns>A filtered list of log entries.</returns>
        public ObservableCollection<LogEntry> FilterLogEntriesByType(LogType type)
        {
            var filteredEntries = new ObservableCollection<LogEntry>(Entries.Where(e => e.Type == type));
            return filteredEntries;
        }

        /// <summary>
        /// Filters log entries by source.
        /// </summary>
        /// <param name="source">The source of log entries to filter.</param>
        /// <returns>A filtered list of log entries.</returns>
        public ObservableCollection<LogEntry> FilterLogEntriesBySource(string source)
        {
            var filteredEntries = new ObservableCollection<LogEntry>(Entries.Where(e => e.Source == source));
            return filteredEntries;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
