using System;

namespace Funzone.Domain.SharedKernel
{
    public static class SystemClock
    {
        private static DateTime? _dateTime;

        public static DateTime Now => _dateTime ?? DateTime.UtcNow;

        public static void Set(DateTime dateTime) => _dateTime = dateTime;

        public static void Reset() => _dateTime = null;
    }
}
