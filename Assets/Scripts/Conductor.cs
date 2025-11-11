using UnityEngine;

public class Conductor : MonoBehaviour
{
    // Public Instance Variables
    public AudioSource song;

    public float bpm;
    public float offset = 0;

    public float quarterNote; // Time duration of a single quarter note / beat
    public float songPosition; // Seconds since song started
    public float songPositionInBeats; // Beats since song started
    public float songStartTimeDSP; // Time that song started (using DSP time)

    private double scheduledStartTime;

    //This goes before start, this makes sure that quarterNote will be ready before any other start function in other scripts.
    void Awake()
    {
        quarterNote = 60 / bpm;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scheduledStartTime = AudioSettings.dspTime + 1.0f; // Schedule the song to start in 1 second
        song.PlayScheduled(scheduledStartTime);

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
