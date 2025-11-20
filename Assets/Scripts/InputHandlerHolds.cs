using UnityEngine;
using System.Collections.Generic;

public class InputHandlerHolds : MonoBehaviour
{
    public Conductor conductor;
    public NoteSpawner spawner;

    //This should be the same list in NoteSpawner
    public List<HoldNoteData> notes;

    private bool isHeld = false;
    private HoldNoteData activeNote = null;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.X))
        {
            HandlePress();
        }

        if(Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.Z) || Input.GetKeyUp(KeyCode.X))
        {
            HandleRelease();
        }
    }

    private void HandlePress()
    {
        if (isHeld) return;

        float currentBeat = conductor.songPositionInBeats;

        HoldNoteData closest = null;
        float bestDist = float.MaxValue;
        foreach(var note in notes)
        {
            if (currentBeat > note.releaseBeat + 0.6f) continue;


            float dist = Mathf.Abs(note.pressBeat - currentBeat);

            if(dist < bestDist)
            {
                bestDist = dist;
                closest = note;
            }
        }

        if(closest == null) return;

        float offsetBeats = currentBeat - closest.pressBeat;
        double offsetMs = offsetBeats * conductor.quarterNote * 1000.0;

        Judgement result = Judgements.GetRating(offsetMs);
        Debug.Log($"Press: {result} ({offsetMs:F1} ms)");

        if(result != Judgement.Miss)
        {
            isHeld = true;
            activeNote = closest;
        }
    }

    private void HandleRelease()
    {
        if (!isHeld || activeNote == null) return;

        float currentBeat = conductor.songPositionInBeats;

        float offsetBeats = currentBeat - activeNote.releaseBeat;
        double offsetMs = offsetBeats * conductor.quarterNote * 1000.0;

        Judgement result = Judgements.GetRating(offsetMs);
        Debug.Log($"Release: {result} ({offsetMs:F1} ms)");

        activeNote.noteObject.ApplyJudgement(result);

        isHeld = false;
        activeNote = null;
    }

}
