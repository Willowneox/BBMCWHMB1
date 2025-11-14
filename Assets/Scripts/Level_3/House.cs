using UnityEngine;

public class House : MonoBehaviour
{
    /// <summary>
    /// The sprite renderer for this house.
    /// </summary>
    public SpriteRenderer spriteRenderer;
    /// <summary>
    /// The note time (in beats) corresponding to this house.
    /// </summary>
    public float noteTime;
    /// <summary>
    /// The conductor to get the song position from.
    /// </summary>
    public Conductor conductor;
    /// <summary>
    /// The scaling factor that the difference between the song position and note time is multiplied by.
    /// This should be the same value as "speed" in the level's ImageScroller.
    /// </summary>
    public float distanceScale;

    // Update is called once per frame
    void Update()
    {
        /// The difference (in beats) between the note time of this house and the actual song position.
        float difference = noteTime - conductor.songPositionInBeats;
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
