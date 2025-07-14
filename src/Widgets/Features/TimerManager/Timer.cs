using System;

namespace Widgets.Features.TimerManager;

public struct Timer(TimeSpan Time)
{
    public readonly string Hours => Time.Hours.ToString("D2");
    public readonly string Minutes => Time.Minutes.ToString("D2");
    public readonly string Seconds => Time.Seconds.ToString("D2");
    public readonly string Formatted => $"{Hours}:{Minutes}:{Seconds}";
}