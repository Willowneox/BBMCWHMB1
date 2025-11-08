using System;
using System.Collections.Generic;
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
    private readonly Queue<Tuple<float, bool>> noteQueue = new();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Todo: Replace with ChartCreator method "FileToBeatChart" when it's ready
        List<float> noteTimeList = new() { 1f, 2f, 3f, 4f, 5f, 6f, 7f, 8f, 9f, 10f };
        // Todo: Call "BeatsToTimes" when it's ready
        foreach (float noteTime in noteTimeList)
        {
            noteQueue.Enqueue(Tuple.Create(noteTime, ((int)noteTime) % 2 == 0));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (noteQueue.Count != 0)
        {
            // We use peek here because we may not want to take it out of the queue yet.
            Tuple<float, bool> nextNote = noteQueue.Peek();
            // Check if the note time is close enough for the house to spawn.
            if (conductor.songPositionInBeats > nextNote.Item1 - 2f)
            {
                // Now we take it out of the queue.
                nextNote = noteQueue.Dequeue();
                // Instantiate the house and set its variables.
                House house = Instantiate(housePrefab);
                float houseX;
                if (nextNote.Item2)
                {
                    houseX = -5.5f;
                }
                else
                {
                    houseX = 5.5f;
                }
                house.spriteRenderer.flipX = nextNote.Item2;
                house.transform.position = new Vector3(houseX, this.transform.position.y, 0f);
                house.noteTime = nextNote.Item1;
                house.conductor = conductor;
                house.distanceScale = imageScroller.speed;
            }
        }
    }
}
