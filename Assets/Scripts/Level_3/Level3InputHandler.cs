using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System;
using Level3;
using UnityEngine.UI;

public class Level3InputHandler : MonoBehaviour
{
    
    public Conductor conductor;

    /// <summary>
    /// The burger to spawn each time the user hits a note.
    /// </summary>
    public Burger burgerPrefab;

    /// <summary>
    /// The image scroller to get the speed of house movement.
    /// </summary>
    public ImageScroller imageScroller;

    /// <summary>
    /// The UI text to display the most recent hit data.
    /// </summary>
    public Text judgementText;

    /// <summary>
    /// List of note times in seconds and their directions. Left = true and right = false.
    /// </summary>
    private List<(float, Direction)> beatList;

    /// <summary>
    /// Dictionary that stores a tally of each judgement
    /// </summary>
    public Dictionary<Judgement, int> ScoreTally;

    // private StreamWriter chartMaker;
    
    void Start()
    {
        // Get the path of the level 3 chart file
        string fullPath = Path.Combine(Application.dataPath, "Charts", "Level_3.txt");
        beatList = DeliveryFoodGameChart.FileToBeatChart(fullPath).Select((tuple, index) => (tuple.Item1 * conductor.quarterNote, tuple.Item2)).ToList();

        // fullPath = Path.Combine(Application.dataPath, "Charts", "Level_3_Custom.txt");
        // chartMaker = new StreamWriter(fullPath);
    }

    void Update()
    {
        //Checks key hit and records the song position.
        if (Input.GetKeyDown(KeyCode.Z))
        {
            float hitTime = conductor.songPosition;
            Debug.Log("Hit at beat: " + conductor.songPositionInBeats + ". Approximately " + (int) Math.Round(conductor.songPositionInBeats));
            // chartMaker.WriteLine((int) Math.Round(hitTime / conductor.quarterNote) + " L");
            CheckHit(hitTime, Direction.Left);
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            float hitTime = conductor.songPosition;
            Debug.Log("Hit at beat:" + conductor.songPositionInBeats);
            // chartMaker.WriteLine((int) Math.Round(hitTime / conductor.quarterNote) + " R");
            CheckHit(hitTime, Direction.Right);
        }

    }
    
    void CheckHit(float hitTime, Direction hitDirection)
    {
        (float hitOffset, float closestNote) = GetHitOffset(hitTime, hitDirection);
        Judgement judgement = Judgements.GetRating(hitOffset);
        if (judgement != Judgement.Miss)
        {
            SpawnBurger(closestNote, hitDirection);
        }
        Debug.Log(judgement);
        ScoreTally[judgement] += 1;

        // Display the judgement onscreen
        judgementText.text = judgement + "!\n" + (int) Math.Abs(hitOffset) + " ms";
        if (hitOffset < 0)
        {
            judgementText.text += " early.";
        } else if (hitOffset > 0)
        {
            judgementText.text += " late.";
        }
    }
    
    /// <summary>
    /// Gets a tuple of two values:
    /// 1. The time difference in milliseconds between the note hit and the nearest note in the chart
    /// 2. The nearest note time in beats
    /// <paramref name="hitDirection"/> Whether or not the player hit the left button.
    /// <returns></returns>
    (float, float) GetHitOffset(float hitTime, Direction hitDirection)
    {
        float closestNote = float.MaxValue;
        // Find the distance to every note and save the closest note from the list.
        foreach ((float beat, Direction beatDirection) in beatList)
        {
            if (beatDirection == hitDirection && Math.Abs(hitTime - beat) < Math.Abs(hitTime - closestNote))
                closestNote = beat;
        }
        // Find the distance to the closest note
        float hitOffset = conductor.songPosition - closestNote;
        // Convert seconds to milliseconds.
        hitOffset *= 1000.0f;
        return (hitOffset, closestNote / conductor.quarterNote);
    }

    void SpawnBurger(float closestNote, Direction hitDirection)
    {
        // Instantiate burger
        Burger burger = Instantiate(burgerPrefab);
        float burgerX;
        if (hitDirection == Direction.Left)
        {
            burgerX = -1f;
            burger.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
        } else
        {
            burgerX = 1f;
            burger.transform.rotation = Quaternion.Euler(0f, 0f, -90f);
        }
        burger.transform.position = new Vector3(burgerX, 0f, 0f);
        // Scale should start off at 0 so we don't see the burger glitching as it spawns.
        burger.transform.localScale = new Vector3(0f, 0f, 0f);
        
        burger.noteTime = closestNote;
        burger.direction = hitDirection;
        burger.conductor = conductor;
        burger.distanceScale = imageScroller.speed;
    }

    void OnDestroy() {
        // chartMaker.Close();
    }
}
