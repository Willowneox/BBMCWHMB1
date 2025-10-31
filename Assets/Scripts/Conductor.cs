using UnityEngine;

public class Conductor : MonoBehaviour
{
    // Public Instance Variables
    public AudioSource song;

    public float bpm = 120;
    public float offset = 0;

    public float quarterNote; // Time duration of a single quarter note / beat
    public float songPosition; // Seconds since song started
    public float songPositionInBeats; // Beats since song started
    public float songStartTimeDSP; // Time that song started (using DSP time)
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        quarterNote = 60 / bpm;
        song.Play();
        songStartTimeDSP = (float)AudioSettings.dspTime;
    }

    // Update is called once per frame
    void Update()
    {
        songPosition = (float)(AudioSettings.dspTime -  songStartTimeDSP) * song.pitch - offset;
        songPositionInBeats = songPosition / quarterNote;
    }
}
