using System;
using System.Collections.Generic;
using Unity.VisualScripting;

public class ChartCreator
{	
	private List<float> beatChart = new List<float>();
	private Conductor conductor;
	
	List<float> beats_to_times(List<float> beatChart)
    {
        List<float> timeChart = new List<float>();
        foreach (float beat in beatChart)
        {
            timeChart.Add(beat*conductor.quarterNote);
        }
    return timeChart;
    }

    double GetHitTime(List<float> timeChart)
    {   List<float> hitTimes = new List<float>();
        // Find the distance to every note
        foreach (float beatTime in timeChart) {
            hitTimes.Add(Math.Abs(conductor.songPosition - beatTime))
        }
        // Find the distance to the closest note
        double hitTime = Minimum(hitTimes)
        // Convert seconds to milliseconds.
        hitTime *= 1000.0;
        return hitTime;
    }
}
