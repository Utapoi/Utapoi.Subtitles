// Copyright (c) Utapoi Ltd <contact@utapoi.com>

using Utapoi.Subtitles.Models;

namespace Utapoi.Subtitles.SSA.Models;

public class SSAFile
{
    public IList<Line> Lines { get; set; } = new List<Line>();
}
