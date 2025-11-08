using UnityEngine;

/// <summary>
///  A class used to scroll an image down and wrap at a certain value.
/// </summary>
public class ImageScroller : MonoBehaviour
{
    /// <summary>
    /// The camera that this script moves.
    /// </summary>
    public Transform imageTransform;
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
        float y = imageTransform.position.y;
        // Move the camera up.
        y -= Time.deltaTime * speed;
        // Wrap the camera's y value around to 0 when it reaches the limit.
        y %= wrapAtY;
        imageTransform.position = new Vector3(imageTransform.position.x, y, imageTransform.position.z);
    }
}
