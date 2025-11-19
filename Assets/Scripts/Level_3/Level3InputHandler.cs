using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System;

public class Level3InputHandler : MonoBehaviour
{
    
    public Conductor conductor;

    /// <summary>
    /// List of note times in seconds and their directions. Left = true and right = false.
    /// </summary>
    private List<(float, bool)> beatList;
    
    void Start()
    {
        // Get the path of the level 3 chart file
        string fullPath = Path.Combine(Application.dataPath, "Charts", "Level_3.txt");

        beatList = DeliveryFoodGameChart.FileToBeatChart(fullPath).Select((tuple, index) => (tuple.Item1 * conductor.quarterNote, tuple.Item2)).ToList();
    }

    void Update()
    {
        //Checks key hit and records the song position.
        if (Input.GetKeyDown(KeyCode.Z))
        {
            float hitTime = conductor.songPosition;
            Debug.Log("Hit at beat:" + conductor.songPositionInBeats);
            CheckHit(hitTime, true);
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            float hitTime = conductor.songPosition;
            Debug.Log("Hit at beat:" + conductor.songPositionInBeats);
            CheckHit(hitTime, false);
        }

    }
    
    void CheckHit(float hitTime, bool hitLeft)
    {
        float hitOffset = GetHitOffset(hitTime, hitLeft);
        Judgement judgement = Judgements.GetRating(hitOffset);
        Debug.Log(judgement);
    }
    
    /// <summary>
    /// Gets the time difference in milliseconds between the note hit and the nearest note in the chart.
    /// <paramref name="hitDirection"/> Whether or not the player hit the left button.
    /// <returns></returns>
    float GetHitOffset(float hitTime, bool hitDirection)
    {
        float closestNote = float.MaxValue;
        // Find the distance to every note and save the closest note from the list.
        foreach ((float beat, bool beatDirection) in beatList)
        {
            if (beatDirection == hitDirection && Math.Abs(hitTime - beat) < Math.Abs(hitTime - closestNote))
                closestNote = beat;
        }
        // Find the distance to the closest note
        float hitOffset = conductor.songPosition - closestNote;
        // Convert seconds to milliseconds.
        hitOffset *= 1000.0f;
        return hitOffset;
    }
}
