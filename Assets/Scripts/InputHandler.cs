using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class InputHandler : MonoBehaviour
{
    public Conductor conductor;

    public List<double> noteBeats = new List<double> { 2.0, 4.0, 6.0, 8.0, 10.0 };
    public List<double> noteTimes = new List<double> {};
    private int nextNoteIndex = 0;

    void Start()
    {
        noteTimes = noteBeats.Select(noteBeats => noteBeats * conductor.quarterNote).ToList();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.X))
        {
            double hitTime = conductor.songPosition;
            CheckHit(hitTime);
        }
            
    }

    void CheckHit(double hitTime)
    {
        if (nextNoteIndex >= noteTimes.Count) return;

        double noteTime = noteTimes[nextNoteIndex];
        double offsetMs = (hitTime - noteTime) * 1000;
        Debug.Log($"{offsetMs:F0} ms");
        Judgement judgement = Judgements.GetRating(offsetMs);
        nextNoteIndex++;
    }
}
