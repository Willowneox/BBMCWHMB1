using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public static class DeliveryFoodGameChart
{
    // List<(float, bool)> beatChart;  List of the target beats as beat numbers and the side.
    // In the text file, side "L" is converted to true and side "R" is converted to false.

    /// <summary>
    /// Returns a dictionary of the beat numbers of target beats and their release time for hold notes
    /// The path must lead to a text file that has a beat number on each line
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static List<(float, bool)> FileToBeatChart(string path)
    {
        StreamReader streamReader = new(path);
        List<(float, bool)> beatChart = new();
        // Turn each line containing a beat number into a float and add it to the chart
        string line = streamReader.ReadLine();
        while (line != null)
        {
            line = line.Trim();
            string[] lineList = line.Split(' ');
            if (lineList.Length == 2)
            {
                float.TryParse(lineList[0], out float beat);
                bool direction = false;
                if (lineList[1] == "L")
                {
                    direction = true;
                }
                beatChart.Add((beat, direction));
            }
            line = streamReader.ReadLine();
        }
        return beatChart;
    }

    /// <summary>
    /// Gets the time to the nearest note in the time chart based on the song position
    /// The direction should be either "L" for left or "R" for right
    /// </summary>
    /// <param name="timeChart"></param>
    /// <param name="conductor"></param>
    /// <param name="direction"></param>
    /// <returns></returns>
    // public static double GetHitTime(List<(float, string)> timeChart, Conductor conductor, string direction)
    // {
    //     List<float> hitTimes = new List<float>();
    //     // Find the distance to every note
    //     foreach ((float beat, string beatDirection) in timeChart)
    //     {
    //         if (beatDirection == direction)
    //             hitTimes.Add(Math.Abs(conductor.songPosition - beat));
    //     }
    //     // Find the distance to the closest note
    //     double hitTime = hitTimes.Min();
    //     // Convert seconds to milliseconds.
    //     hitTime *= 1000.0;
    //     return hitTime;
    // }
}
