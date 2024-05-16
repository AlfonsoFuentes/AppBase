using System.Diagnostics.CodeAnalysis;

namespace Application.Interfaces.Services.Storage
{
    [ExcludeFromCodeCoverage]
    public class ChangedEventArgs
    {
        public string Key { get; set; } = string.Empty;
        public object OldValue { get; set; } = string.Empty;
        public object NewValue { get; set; } = string.Empty;
    }
}