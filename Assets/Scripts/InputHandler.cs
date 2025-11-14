using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class InputHandler : MonoBehaviour
{
    public Conductor conductor;
    public Judgements judge;

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
            HandleHit();
        }
            
    }

    void HandleHit()
    {
        Note[] allNotes = FindObjectsOfType<Note>();

        List<Note> candidates = new List<Note>();
        for(int i = 0; i < allNotes.Length; i++)
        {
            Note n = allNotes[i];
            if(n != null && n.isActiveAndEnabled && !n.wasHit && !n.wasMissed)
            {
                candidates.Add(n);
            }
        }

        if (candidates.Count == 0)
        {
            Debug.Log("Miss (No notes available)");
            return;
        }

        float currentBeat = conductor.songPositionInBeats;
        Note closestNote = candidates[0];
        float bestDist = Mathf.Abs(candidates[0].hitBeat - currentBeat);

        for(int i = 1; i < candidates.Count; i++)
        {
            float dist = Mathf.Abs(candidates[i].hitBeat - currentBeat);
            if(dist < bestDist)
            {
                bestDist = dist;
                closestNote = candidates[i];
            }
        }

        float offsetBeats = currentBeat - closestNote.hitBeat;
        double offsetMs = offsetBeats * conductor.quarterNote * 1000.0;

        Judgement result = Judgements.GetRating(offsetMs);

        Debug.Log($"{result} ({offsetMs:F1} ms");

        if (result != Judgement.Miss)
        {
            closestNote.Hit();
        }

        
    }
    
}
