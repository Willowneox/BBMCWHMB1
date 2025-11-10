using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework.Constraints;
using UnityEngine;

public class HouseSpawner : MonoBehaviour
{
    /// <summary>
    /// The house prefab to instantiate.
    /// </summary>
    public House housePrefab;
    /// <summary>
    /// The image scroller to get the speed variable from.
    /// </summary>
    public ImageScroller imageScroller;
    /// <summary>
    /// The conductor used to get the song position.
    /// </summary>
    public Conductor conductor;
    /// <summary>
    /// A queue of house times and whether they go on the left or right side of the level.
    /// </summary>
    private readonly Queue<(float, bool)> noteQueue = new();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get the path of the level 3 chart file
        string fullPath = Path.Combine(Application.dataPath, "Charts", "Level_3.txt");

        List<(float, bool)> beatList = DeliveryFoodGameChart.FileToBeatChart(fullPath);

        foreach ((float beatTime, bool direction) in beatList)
        {
            noteQueue.Enqueue((beatTime, direction));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (noteQueue.Count != 0)
        {
            // We use peek here because we may not want to take it out of the queue yet.
            (float nextBeatTime, bool nextDirection) = noteQueue.Peek();
            // Check if the note time is close enough for the house to spawn.
            if (conductor.songPositionInBeats > nextBeatTime - 2f)
            {
                // Now we take it out of the queue.
                (nextBeatTime, nextDirection) = noteQueue.Dequeue();
                // Instantiate the house and set its variables.
                House house = Instantiate(housePrefab);
                float houseX;
                if (nextDirection)
                {
                    houseX = -5.5f;
                }
                else
                {
                    houseX = 5.5f;
                }
                house.spriteRenderer.flipX = nextDirection;
                house.transform.position = new Vector3(houseX, this.transform.position.y, 0f);
                house.noteTime = nextBeatTime;
                house.conductor = conductor;
                house.distanceScale = imageScroller.speed;
            }
        }
    }
}
