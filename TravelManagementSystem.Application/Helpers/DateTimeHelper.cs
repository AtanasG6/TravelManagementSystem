namespace TravelManagementSystem.Application.Helpers
{
    public static class DateTimeHelper
    {
        /// <summary>
        /// Конвертира Unix timestamp (секунди) към локално DateTime.
        /// </summary>
        public static DateTime FromUnixTimeSeconds(long seconds)
            => DateTimeOffset
                   .FromUnixTimeSeconds(seconds)
                   .UtcDateTime
                   .ToLocalTime();
    }
}
