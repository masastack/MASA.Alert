// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Web.Admin.Components.Modules.Alarm;

public class ChartHelper
{
    public static List<string> Colors = new List<string> { "#4318FF", "#FF5252", "#FFB547", "#05CD99", "#00B42A", "#FF7D00", "#37A7FF" };

    public static object? GetLineStyle(int i)
    {
        return new
        {
            Color = i < Colors.Count ? Colors[i] : null,
            Width = 3
        };
    }

    public static object? GetAreaStyle(int i)
    {
        return new
        {
            Color = new
            {
                ColorStops = GetColorStops(i),
                Global = false,
                Type = "linear",
                X = 0,
                X2 = 0,
                Y = 0,
                Y2 = 1
            }
        };
    }

    public static object? GetColorStops(int i)
    {
        var index = i % Colors.Count;
        return index switch
        {
            0 => new[] { new { Offset = 0, Color = "#ECE8FF" }, new { Offset = 1, Color = "rgba(236, 232, 255, 0)" } },
            1 => new[] { new { Offset = 0, Color = "#FDCDC5" }, new { Offset = 1, Color = "rgba(255, 236, 232, 0)" } },
            2 => new[] { new { Offset = 0, Color = "#FFB547" }, new { Offset = 1, Color = "#FFF8ED" } },
            3 => new[] { new { Offset = 0, Color = "#05CD99" }, new { Offset = 1, Color = "#E6FAF5" } },
            4 => new[] { new { Offset = 0, Color = "#00B42A" }, new { Offset = 1, Color = "#7A8499" } },
            5 => new[] { new { Offset = 0, Color = "#FF7D00" }, new { Offset = 1, Color = "#FFF7E8" } },
            6 => new[] { new { Offset = 0, Color = "#37A7FF" }, new { Offset = 1, Color = "#EBF6FF" } },
            _ => null
        };
    }
}
