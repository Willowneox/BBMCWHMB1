using System;
using UnityEngine;

public class Motorcycle_Cat : MonoBehaviour
{
    /// <summary>
    /// The motorcycle bobs up and down to the tempo.
    /// </summary>
    public Conductor conductor;
    
    void Update()
    {
        double y = Math.Sin(Math.PI * 2.0 * conductor.songPositionInBeats) / 20.0;
        this.transform.position = new Vector3(this.transform.position.x, (float) y, this.transform.position.z);
    }
}
