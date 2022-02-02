// Copyright (c) Utapoi Ltd <contact@utapoi.com>

namespace Utapoi.Subtitles.Models.ASS;

public class ASSFile
{
    public IList<Line> Lines { get; set; } = Array.Empty<Line>();
}
