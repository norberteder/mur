using System;
using System.Text.RegularExpressions;

namespace Mur
{
    internal class ScheduleParser
    {
        public static Func<DateTime, DateTime> GetAddInterval(string interval)
        {
            var rx = new Regex(@"(?<count>\d+)\s*(?<unit>minute|hour|day|week|month|year)s?", RegexOptions.IgnoreCase);
            var match = rx.Match(interval);

            if (match.Success)
            {
                var count = Convert.ToInt32(match.Result("${count}"));
                var unit = match.Result("${unit}").ToLowerInvariant();

                switch (unit)
                {
                    case "minute":
                        return dateTime => dateTime.AddMinutes(count);

                    case "hour":
                        return dateTime => dateTime.AddHours(count);

                    case "day":
                        return dateTime => dateTime.AddDays(count);

                    case "week":
                        return dateTime => dateTime.AddDays(7 * count);

                    case "month":
                        return dateTime => dateTime.AddMonths(count);

                    case "year":
                        return dateTime => dateTime.AddYears(count);
                }
            }
            throw new ArgumentException("interval");
        }

        public static DateTime GetNextRun(DateTime startDate, string interval, DateTime lastRun)
        {
            var addInterval = GetAddInterval(interval);
            var nextStart = startDate;

            while (nextStart <= lastRun)
            {
                nextStart = addInterval(nextStart);
            }
            return nextStart;
        }
    }
}
