using System;

namespace BaseProject.Intrastructure
{
    public interface IDateTimeProvider
    {
        DateTime Now { get; }
    }
}