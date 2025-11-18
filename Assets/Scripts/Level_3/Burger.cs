using Level3;
using UnityEngine;
using UnityEngine.Rendering;

public class Burger : MonoBehaviour
{

    /// <summary>
    /// The note time (in beats) corresponding to this burger.
    /// This is a chart time, not the user's hit time.
    /// </summary>
    public float noteTime;
    /// <summary>
    /// The direction of the burger.
    /// </summary>
    public Direction direction;
    /// <summary>
    /// The conductor to get the song position from.
    /// </summary>
    public Conductor conductor;
    /// <summary>
    /// The scaling factor that the difference between the song position and note time is multiplied by.
    /// This should be the same value as "speed" in the level's ImageScroller.
    /// </summary>
    public float distanceScale;

    /// <summary>
    /// The starting X position of the burger.
    /// </summary>
    private float startingX;

    /// <summary>
    /// The X position where the burger should despawn.
    /// </summary>
    private float despawnX;

    void Start()
    {
        startingX = this.transform.position.x;
        if (direction == Direction.Left) {
            despawnX = HouseSpawner.houseLeftXPos * 0.75f;        
        } else
        {
            despawnX = HouseSpawner.houseRightXPos * 0.75f;
        }
        // The burger is ready to be visible, so we change the scale from 0.
        this.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
    }

    // Update is called once per frame
    void Update()
    {
        /// The difference (in beats) between the note time of this house and the actual song position.
        /// Note that this is negative after the note is supposed to be hit.
        float difference = noteTime - conductor.songPositionInBeats;

        // X adds the difference to the starting position to move smoothly from start to finish.
        float x = this.transform.position.x;
        if (direction == Direction.Left)
        {
            x = startingX + difference * distanceScale * 0.75f;
            if (x < despawnX)
            {
                Destroy(this.gameObject);
                return;
            }
        } else
        {
            x = startingX - (difference * distanceScale * 0.75f);
            if (x > despawnX)
            {
                Destroy(this.gameObject);
                return;
            }
        }
        // Y has the same position logic as the house.
        float y = this.transform.position.y;
        // Move the houses down to the music.
        y = difference * distanceScale;
        this.transform.position = new Vector3(x, y, this.transform.position.z);
    }
}
