using System;

namespace Accounts.Api.Utils
{
    public class DateTimeProxy : IDateTimeProxy
    {
        public DateTime UtcNow() => DateTime.UtcNow;
    }
}