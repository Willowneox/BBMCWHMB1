using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class NoteSpawner : MonoBehaviour
{
    public Conductor conductor;

    //Assign notes in the inspector in input handler
    public InputHandlerHolds inputHandler;
    
    private int nextNoteIndex = 0;

    public GameObject notePrefab;
    public Transform spawnPoint;
    public Transform hitPoint;

    // Update is called once per frame
    void Update()
    {
        //Prevents spawning notes before the song starts
        if (conductor.songPositionInBeats < 0) return;

        var notes = inputHandler.notes;

        //Basically spawns a note at the hard coded note time.
        while(nextNoteIndex < inputHandler.notes.Count && conductor.songPositionInBeats + 4f >= inputHandler.notes[nextNoteIndex].pressBeat)
        {
            SpawnNote(inputHandler.notes[nextNoteIndex]);
            nextNoteIndex++;
        }
    }

    //The thing that actually spawns the note.
    void SpawnNote(HoldNoteData data)
    {
        GameObject go = Instantiate(notePrefab, spawnPoint.position, Quaternion.identity);
        Note note = go.GetComponent<Note>();
        data.noteObject = note;

        //Assigning beats
        note.pressBeat = data.pressBeat;
        note.releaseBeat = data.releaseBeat;
        note.holdLengthBeats = data.releaseBeat - data.pressBeat;

        //Spawn note 4 beats before it needs to be hit
        note.spawnBeat = data.pressBeat - 4f; 
        note.spawnPosition = spawnPoint.position;
        note.hitPosition = hitPoint.position;

        //Link visual note to chart entry
        data.noteObject = note;
    }
}
