using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Level3;

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
    /// The audio component connected to this game object.
    /// </summary>
    public AudioSource doorSound;
    /// <summary>
    /// The conductor used to get the song position.
    /// </summary>
    public Conductor conductor;
    /// <summary>
    /// The X position of left houses.
    /// </summary>
    public const float houseLeftXPos = -5.5f;
    /// <summary>
    /// The X position of right houses.
    /// </summary>
    public const float houseRightXPos = 4.75f;
    /// <summary>
    /// A queue of house times and whether they go on the left or right side of the level.
    /// </summary>
    private readonly Queue<(float, Direction)> noteQueue = new();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get the path of the level 3 chart file
        string fullPath = Path.Combine(Application.dataPath, "Charts", "Level_3.txt");

        List<(float, Direction)> beatList = DeliveryFoodGameChart.FileToBeatChart(fullPath);

        foreach ((float beatTime, Direction direction) in beatList)
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
            (float nextBeatTime, _) = noteQueue.Peek();
            // Check if the note time is close enough for the house to spawn.
            if (conductor.songPositionInBeats > nextBeatTime - 2f)
            {
                Direction nextDirection;
                // Now we take it out of the queue.
                (nextBeatTime, nextDirection) = noteQueue.Dequeue();
                // Instantiate the house and set its variables.
                House house = Instantiate(housePrefab);
                float houseX;
                if (nextDirection == Direction.Left)
                {
                    houseX = houseLeftXPos;
                    house.spriteRenderer.flipX = true;
                    // Make the door sound play on the left.
                    doorSound.panStereo = -1f;
                }
                else
                {
                    houseX = houseRightXPos;
                    // Make the door sound play on the right.
                    doorSound.panStereo = 1f;
                }
                house.transform.position = new Vector3(houseX, this.transform.position.y, 0f);

                // Slightly randomize the house sizes.
                float scale = UnityEngine.Random.Range(0.5f, 0.75f);
                house.transform.localScale = new Vector3(scale, scale, scale);

                house.noteTime = nextBeatTime;
                house.conductor = conductor;
                house.distanceScale = imageScroller.speed;
                // Play the door opening sound.
                doorSound.Play();
            }
        }
    }
}
