namespace OnlineStore.AppServices.Common.DateTimeProviders
{
    /// <summary>
    /// Провайдер времени.
    /// </summary>
    public sealed class DateTimeProvider : IDateTimeProvider
    {
        /// <inheritdoc/>
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
