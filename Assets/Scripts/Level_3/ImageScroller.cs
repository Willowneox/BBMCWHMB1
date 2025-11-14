using UnityEngine;

/// <summary>
///  A class used to scroll an image down and wrap at a certain value.
/// </summary>
public class ImageScroller : MonoBehaviour
{
    /// <summary>
    /// The conductor to get the song position from.
    /// </summary>
    public Conductor conductor;
    /// <summary>
    /// The speed that the camera should move at.
    /// </summary>
    public float speed;
    /// <summary>
    /// The y value at which the camera's position should be reset to y = 0.
    /// </summary>
    public float wrapAtY;

    // Update is called once per frame
    void Update()
    {
        float y = this.transform.position.y;
        // Move the street down to the music.
        y = -conductor.songPositionInBeats * speed;
        // Wrap the camera's y value around to 0 when it reaches the limit.
        y %= wrapAtY;
        this.transform.position = new Vector3(this.transform.position.x, y, this.transform.position.z);
    }
}
