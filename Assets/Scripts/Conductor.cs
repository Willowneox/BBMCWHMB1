using UnityEngine;

public class Conductor : MonoBehaviour
{
    // Public Instance Variables
    public AudioSource song;

    public float bpm;
    public float offset = 0;

    public float quarterNote; // Time duration of a single quarter note / beat
    public float songPosition = 0f; // Seconds since song started
    public float songPositionInBeats = 0f; // Beats since song started

    public float songStartTimeDSP; // Time that song started (using DSP time)
    private float leadInBeats = 4f; // Beats before song starts to allow for notes to spawn in

    //This goes before start, this makes sure that quarterNote will be ready before any other start function in other scripts.
    void Awake()
    {
        quarterNote = 60 / bpm;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        double leadInSeconds  = leadInBeats * quarterNote;
        songStartTimeDSP = (float) (AudioSettings.dspTime + leadInSeconds); // Schedule the song to start in 1 second
        song.PlayScheduled(songStartTimeDSP);
    }

    // Update is called once per frame
    void Update()
    {
        if (AudioSettings.dspTime >= songStartTimeDSP)
        {
            songPosition = (float)(AudioSettings.dspTime -  songStartTimeDSP) * song.pitch - offset;
            songPositionInBeats = songPosition / quarterNote;
        }
    }

}
