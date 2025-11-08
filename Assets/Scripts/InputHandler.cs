using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class InputHandler : MonoBehaviour
{
    
    public Conductor conductor;

    //Hard coded notes
    public List<double> noteBeats = new List<double> { 1.0, 3.0, 5.0, 7.0, 9.0, 11.0 };

    //List for notes converted into times in seconds
    public List<double> noteTimes = new List<double> {};
    private int nextNoteIndex = 0;

    
    void Start()
    {
        //This is where the hard coded note beats are converted into seconds
        noteTimes = noteBeats.Select(noteBeats => noteBeats * conductor.quarterNote).ToList();
    }
    
    void Update()
    {
        //Checks key hit and records the song position.
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.X))
        {
            double hitTime = conductor.songPosition;
            CheckHit(hitTime);
        }
            
    }
    
    //Compares the hit to the next hard coded note, then outputs the offset in ms and grabs the judgement from the judgement script.
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
