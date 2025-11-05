using UnityEngine;

/// <summary>
///  A class used to scroll a camera up and wrap at a certain value.
/// </summary>
public class CameraScroller : MonoBehaviour
{
    /// <summary>
    /// The camera that this script moves.
    /// </summary>
    public Transform cameraTransform;
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
        float cameraY = cameraTransform.position.y;
        // Move the camera up.
        cameraY += Time.deltaTime * speed;
        // Wrap the camera's y value around to 0 when it reaches the limit.
        cameraY %= wrapAtY;
        cameraTransform.position = new Vector3(cameraTransform.position.x, cameraY, cameraTransform.position.z);
    }
}
