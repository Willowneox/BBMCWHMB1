using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public static class AssemblyGameChart
{
    // List<(float, float)> beatChart;  List of the target beats and release beats as beat numbers 
    // List<(float, float)> timeChart;  List of the target beats and release beats as times in sec
    // Release beat value is set to 0 if the note is not a hold note

    /// <summary>
    /// Returns a dictionary of the beat numbers of target beats and their release time for hold notes
    /// The path must lead to a text file that has a beat number on each line
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    static List<(float, float)> FileToBeatChart(string path)
    {
        StreamReader streamReader = new StreamReader(path);
        List<(float, float)> beatChart = new List<(float, float)>();
        // Turn each line containing a beat number into a float and add it to the chart
        string line = streamReader.ReadLine();
        while (line != null)
        {
            line = line.Trim();
            string[] lineList = line.Split(' ');
            if (lineList.Length == 1)
                    {
                float.TryParse(lineList[0], out float beat);
                beatChart.Add((beat, 0));
            }
            else if (lineList.Length == 2)
                    {
                float.TryParse(lineList[0], out float beatStart);
                float.TryParse(lineList[1], out float beatEnd);
                beatChart.Add((beatStart, beatEnd));
            }
            line = streamReader.ReadLine();
        }
        return beatChart;
    }

    /// <summary>
    /// Returns a list of the time of target beats based on the beat numbers
    /// </summary>
    /// <param name="beatChart"></param>
    /// <param name="conductor"></param>
    /// <returns></returns>
	static List<(float, float)> BeatsToTimes(List<(float, float)> beatChart, Conductor conductor)
    {
        List<(float, float)> timeChart = new List<(float, float)>();
        foreach ((float, float) beat in  beatChart)
        {
            timeChart.Add((beat.Item1 * conductor.quarterNote, beat.Item2 * conductor.quarterNote));
        }
        return timeChart;
    }

    /// <summary>
    /// Gets the time to the nearest note in the time chart based on the song position
    /// </summary>
    /// <param name="timeChart"></param>
    /// <param name="conductor"></param>
    /// <returns></returns>
    static double GetHitTime(List<(float, float)> timeChart, Conductor conductor)
    {
        List<float> hitTimes = new List<float>();
        // Find the distance to every note
        foreach ((float, float) beatTime in timeChart)
        {
            hitTimes.Add(Math.Abs(conductor.songPosition - beatTime.Item1));
        }
        // Find the distance to the closest note
        double hitTime = hitTimes.Min();
        // Convert seconds to milliseconds.
        hitTime *= 1000.0;
        return hitTime;
    }

    /// <summary>
    /// Gets the time to the release time of the most recent hold note
    /// </summary>
    /// <param name="timeChart"></param>
    /// <param name="conductor"></param>
    /// <returns></returns>
    static double GetReleaseTime(List<(float, float)> timeChart, Conductor conductor)
    {
        List<float> pastHoldNoteStarts = new List<float>();
        // Find all past hold notes
        foreach ((float, float) note in timeChart)
        {
            if (note.Item2 > 0 && conductor.songPosition - note.Item1 > 0)
                    pastHoldNoteStarts.Add(note.Item1);
        }
        // Find the most recent hold note
        float mostRecentHoldNote = pastHoldNoteStarts.Max();
        // Find the distance to the release time of the note
        float releaseTime = -1;
        foreach((float, float) note in timeChart)
                {
            if (note.Item1 == mostRecentHoldNote)
            {
                releaseTime = Math.Abs(conductor.songPosition - note.Item2);
            }
        }
        // Convert to milliseconds
        return releaseTime * 1000.0;
    }
}
