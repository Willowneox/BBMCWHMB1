using UnityEngine;

public class TestConductor : MonoBehaviour
{
    public AudioSource metronome;

    public Conductor conductor;
    private int nextBeat = 0;
    private bool trigger = true;

    // Update is called once per frame
    void Update()
    {
        if (conductor.songPositionInBeats >= nextBeat) // If it is time for the "nextBeat", do the visual
        {
            Visual();
            nextBeat += 1;     // Increment nextBeat so it does the visual again
        }
    }

    void Visual()
    {
        //Plays the metronome sound, comment out if you no longer need it for testing.
        metronome.Play();

        GetComponent<SpriteRenderer>().color = trigger ? Color.blue : Color.gold;
        trigger = !trigger;
    }
}
