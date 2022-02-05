// Copyright (c) Utapoi Ltd <contact@utapoi.com>

using System.Text.RegularExpressions;
using Utapoi.Subtitles.Models;
using Utapoi.Subtitles.SSA.Models;

namespace Utapoi.Subtitles.SSA;

public static class SSAConvert
{
    private static readonly Regex karaoke_tag_regex = new(@"\{\\k([0-9].*)\}");

    private static readonly Regex karaoke_part_regex = new(@"\{\\k([0-9]*)\}(\p{L}*\s?)");

    public static SSAFile Deserialize(string file)
    {
        var ssaFile = new SSAFile();
        using var stream = File.OpenRead(file);
        using var reader = new StreamReader(stream);

        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();

            if (string.IsNullOrEmpty(line))
                continue;

            if (!line.Equals("[Events]"))
                continue;
            else
                break;
        }

        reader.ReadLine();

        var index = 0;

        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();

            if (string.IsNullOrEmpty(line))
                continue;

            ssaFile.Lines.Add(ParseLine(line, index));
            index++;
        }

        return ssaFile;
    }

    private static Line ParseLine(string content, int index)
    {
        var parts = content.Split(",");

        if (parts.Length < 10)
            throw new InvalidOperationException($"Invalid line format. {content}");

        var lineType = GetLineType(parts[9]);
        var line = new Line
        {
            Start = TimeSpan.Parse(parts[1]),
            End = TimeSpan.Parse(parts[2]),
            Index = 0,
            Type = lineType
        };

        if (lineType == LineType.Karaoke)
        {
            line.Karaoke = ParseKaraoke(parts[9]);
            line.Text = string.Join("", line.Karaoke.Select(x => x.Content));
        }
        else
        {
            line.Text = parts[9];
        }

        return line;
    }

    private static LineType GetLineType(string line)
    {
        return karaoke_tag_regex.IsMatch(line)
            ? LineType.Karaoke
            : LineType.Text;
    }

    private static IList<Karaoke> ParseKaraoke(string content)
    {
        return karaoke_part_regex
            .Matches(content)
            .Select(match => new Karaoke
            {
                Content = match.Groups[2].Value,
                Duration = int.Parse(match.Groups[1].Value)
            })
            .ToList();
    }
}
