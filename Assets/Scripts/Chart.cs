using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;

public class ChartCreator
{	
	private List<float> beatChart; // List of the target beats as beat numbers
    private List<float> timeChart; // List of the target beats as times in sec
	private Conductor conductor;
	
    /// <summary>
    /// Returns a list of the time of target beats based on the beat numbers
    /// </summary>
    /// <param name="beatChart"></param>
    /// <returns></returns>
	List<float> BeatsToTimes(List<float> beatChart)
    {
        List<float> timeChart = new List<float>();
        foreach (float beat in beatChart)
        {
            timeChart.Add(beat*conductor.quarterNote);
        }
    return timeChart;
    }
    /// <summary>
    /// Returns a list of the beat numbers of target beats
    /// The path must lead to a text file that has a beat number on each line
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    List<float> FileToBeatChart(string path)
    {
        StreamReader streamReader = new StreamReader(path);
        List<float> beatChart = new List<float>();
        // Turn each line containing a beat number into a float and add it to the chart
        string line = streamReader.ReadLine();
        while (line != null)
        {
            line = line.Trim();
            float.TryParse(line, out float beat);
            beatChart.Add(beat);
            line = streamReader.ReadLine();
        }
        return beatChart;
    }
    /// <summary>
    /// Gets the time to the nearest note in the time chart based on the song position
    /// </summary>
    /// <param name="timeChart"></param>
    /// <returns></returns>
    double GetHitTime(List<float> timeChart)
    {   List<float> hitTimes = new List<float>();
        // Find the distance to every note
        foreach (float beatTime in timeChart) {
            hitTimes.Add(Math.Abs(conductor.songPosition - beatTime));
        }
        // Find the distance to the closest note
        double hitTime = hitTimes.Min();
        // Convert seconds to milliseconds.
        hitTime *= 1000.0;
        return hitTime;
    }
}
