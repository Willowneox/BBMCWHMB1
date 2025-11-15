using System;
using System.Text.RegularExpressions;
using UnityEngine;

public class Motorcycle_Cat : MonoBehaviour
{
    /// <summary>
    /// The motorcycle bobs up and down to the tempo.
    /// </summary>
    public Conductor conductor;

    /// <summary>
    /// The audio source component for this game object.
    /// </summary>
    public AudioSource motorcycleSound;
    
    void Update()
    {
        // Start motorcycle sound when the song starts, but lower the volume to half by the 8th beat.
        float volume = (16f - conductor.songPositionInBeats) / 16f;
        volume = Math.Clamp(volume, 0.5f, 1f);
        motorcycleSound.volume = volume;
        // Make the cat bob up and down to the music
        double y = -Math.Sin(Math.PI * 2.0 * conductor.songPositionInBeats) / 20.0;
        this.transform.position = new Vector3(this.transform.position.x, (float) y, this.transform.position.z);
    }
}
