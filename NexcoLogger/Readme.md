# NexLog

NexLog is a robust logging library for WinUI 3 applications, providing detailed logging functionality with features such as log filtering, exporting, and structured logging.

## Features

- **Thread-Safe Logging**: Ensures all interactions with the UI are thread-safe.
- **Log Filtering**: Filter log entries by type or source.
- **Log Exporting**: Export log entries to a CSV file.
- **Configuration Options**: Configure log retention policies and maximum log entry limits.
- **Structured Logging**: Enhanced log messages with structured data fields.

## Installation

1. **Add NexLog to your project:**

   Add the NexLog project as a reference to your main application project.

2. **Install Required NuGet Packages:**

   ```powershell
   Install-Package Microsoft.WindowsAppSDK -Version 1.2.0
   Install-Package Microsoft.Extensions.DependencyInjection -Version 6.0.0

## Classes and Methods

**LogEntry**
Represents a log entry with detailed information.

**LogType**
Represents the type of a log entry.

**ILoggingService**
Provides methods for logging messages with varying levels of severity.

**LoggingService**
Provides methods for logging messages and tracking operation times.

**LogsViewModel**
ViewModel for managing log entries.

**LogTypeToColorConverter**
Converts LogType values to corresponding colors.

**Contributions**
Feel free to contribute to the project by submitting issues or pull requests.

**License**
This project is licensed under the MIT License.