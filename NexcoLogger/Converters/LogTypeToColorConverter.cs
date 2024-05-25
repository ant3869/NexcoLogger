using Microsoft.UI;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;
using NexcoLogger.Models;

namespace NexcoLogger.Converters
{
    /// <summary>
    /// Converts <see cref="LogType"/> values to corresponding colors.
    /// </summary>
    public class LogTypeToColorConverter : IValueConverter
    {
        /// <summary>
        /// Converts a <see cref="LogType"/> value to a <see cref="SolidColorBrush"/>.
        /// </summary>
        /// <param name="value">The <see cref="LogType"/> value to convert.</param>
        /// <param name="targetType">The target type of the conversion.</param>
        /// <param name="parameter">An optional parameter for the conversion.</param>
        /// <param name="language">The language of the conversion.</param>
        /// <returns>A <see cref="SolidColorBrush"/> corresponding to the <see cref="LogType"/> value.</returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return value switch
            {
                LogType.Info => new SolidColorBrush(Colors.Silver),
                LogType.Warning => new SolidColorBrush(Colors.Yellow),
                LogType.Error => new SolidColorBrush(Colors.Red),
                LogType.Exception => new SolidColorBrush(Colors.DarkRed),
                LogType.Success => new SolidColorBrush(Colors.Green),
                _ => new SolidColorBrush(Colors.Gray),
            };
        }

        /// <summary>
        /// Converts a <see cref="SolidColorBrush"/> back to a <see cref="LogType"/> value.
        /// </summary>
        /// <param name="value">The <see cref="SolidColorBrush"/> value to convert back.</param>
        /// <param name="targetType">The target type of the conversion.</param>
        /// <param name="parameter">An optional parameter for the conversion.</param>
        /// <param name="language">The language of the conversion.</param>
        /// <returns>A <see cref="LogType"/> value corresponding to the <see cref="SolidColorBrush"/> value.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
