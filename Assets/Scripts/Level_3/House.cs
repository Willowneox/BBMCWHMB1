using Level3;
using Unity.Collections;
using UnityEngine;

public class House : MonoBehaviour
{
    /// <summary>
    /// The sprite renderer for this house.
    /// </summary>
    public SpriteRenderer spriteRenderer;
    /// <summary>
    /// The audio component connected to this game object.
    /// </summary>
    public AudioSource doorSound;

    /// <summary>
    /// The note time (in beats) corresponding to this house.
    /// </summary>
    public float noteTime;
    /// <summary>
    /// The side that this house was spawned on.
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

    private bool hasPlayedDoorSound = false;

    void Start()
    {
        if (direction == Direction.Left)
        {
            // Make the door sound play on the left at a regular pitch.
            doorSound.panStereo = -1f;
            doorSound.pitch = 2f;
        }
        else
        {
            // Make the door sound play on the right at a high pitch.
            doorSound.panStereo = 1f;
            doorSound.pitch = 3f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        /// The difference (in beats) between the note time of this house and the actual song position.
        float difference = noteTime - conductor.songPositionInBeats;
        if (!hasPlayedDoorSound && difference < 1f)
        {
            // If there is 1 beat before this note is supposed to be hit, play the door sound.
            doorSound.Play();
            hasPlayedDoorSound = true;
        }
        // If 2 beats have passed since this note was supposed to be hit, we can despawn the house.
        if (difference < -2f)
        {
            Destroy(this.gameObject);
        } else
        {
            float y = this.transform.position.y;
            // Move the houses down to the music.
            y = difference * distanceScale;
            this.transform.position = new Vector3(this.transform.position.x, y, this.transform.position.z);
        }
    }
}
