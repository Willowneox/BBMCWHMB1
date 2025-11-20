using UnityEngine;
using System.Collections.Generic;

public class InputHandlerHolds : MonoBehaviour
{
    public Conductor conductor;

    //Hard coded notes here
    [System.Serializable]
    public class HoldNoteData
    {
        public float pressBeat;
        public float releaseBeat;
    }

    public List<HoldNoteData> notes = new List<HoldNoteData>();

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

        notes.Remove(activeNote);

        isHeld = false;
        activeNote = null;
    }

}
