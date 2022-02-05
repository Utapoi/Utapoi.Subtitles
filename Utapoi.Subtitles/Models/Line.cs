// Copyright (c) Utapoi Ltd <contact@utapoi.com>

namespace Utapoi.Subtitles.Models;

public class Line
{
    public int Index { get; set; }

    public TimeSpan Start { get; set; }

    public TimeSpan End { get; set; }

    public TimeSpan Duration => End - Start;

    public LineType Type { get; set; } = LineType.None;

    public string Text { get; set; } = string.Empty;

    public IList<Karaoke>? Karaoke { get; set; }
}
