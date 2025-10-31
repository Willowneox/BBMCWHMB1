using UnityEngine;

public class TestConductor : MonoBehaviour
{
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
        GetComponent<SpriteRenderer>().color = trigger ? Color.blue : Color.gold;
        trigger = !trigger;
    }
}
