using System;

namespace Accounts.Api.Utils
{
    public interface IDateTimeProxy
    {
        DateTime UtcNow();
    }
}