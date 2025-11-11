using UnityEngine;

public class Metronome : MonoBehaviour
{
    public Conductor conductor;
    public AudioSource tickSound;
    private int lastBeat = -1;

    // Update is called once per frame
    void Update()
    {
        int currentBeat = Mathf.FloorToInt(conductor.songPositionInBeats);

        if (currentBeat != lastBeat && currentBeat >= 0)
        {
            tickSound.Play();
            lastBeat = currentBeat;
        }
    }
}
