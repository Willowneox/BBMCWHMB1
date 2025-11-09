using System;
using System.Collections.Generic;
using System.IO;

public static class DeliveryFoodGameChart
{
    // List<(float, string)> beatChart;  List of the target beats as beat numbers and the side, either "L" for left or "R" for right
    // List<(float, string)> timeChart;  List of the target beats as times in sec and the side, either "L" for left or "R" for right

    /// <summary>
    /// Returns a dictionary of the beat numbers of target beats and their release time for hold notes
    /// The path must lead to a text file that has a beat number on each line
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    static List<(float, string)> FileToBeatChart(string path)
    {
        StreamReader streamReader = new StreamReader(path);
        List<(float, string)> beatChart = new List<(float, string)>();
        // Turn each line containing a beat number into a float and add it to the chart
        string line = streamReader.ReadLine();
        while (line != null)
        {
            line = line.Trim();
            string[] lineList = line.Split(' ');
            if (lineList.Length == 2)
            {
                float.TryParse(lineList[0], out float beat);
                beatChart.Add((beat, lineList[1]));
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
	static List<(float, string)> BeatsToTimes(List<(float, string)> beatChart, Conductor conductor)
    {
        List<(float, string)> timeChart = List<(float, string)>();
        foreach ((float,string) beat in beatChart)
        {
            timeChart.Add((beat.Item1 * conductor.quarterNote, beat.Item2));
        }
        return timeChart;
    }

    /// <summary>
    /// Gets the time to the nearest note in the time chart based on the song position
    /// The direction should be either "L" for left or "R" for right
    /// </summary>
    /// <param name="timeChart"></param>
    /// <param name="conductor"></param>
    /// <param name="direction"></param>
    /// <returns></returns>
    static double GetHitTime(List<(float, string)> timeChart, Conductor conductor, string direction)
    {
        List<float> hitTimes = new List<float>();
        // Find the distance to every note
        foreach (float beat in timeChart)
        {
            if (beat.Item2 == direction)
                hitTimes.Add(Math.Abs(conductor.songPosition - beat.Item1));
        }
        // Find the distance to the closest note
        double hitTime = hitTimes.Min();
        // Convert seconds to milliseconds.
        hitTime *= 1000.0;
        return hitTime;
    }
}
