using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class NoteSpawner : MonoBehaviour
{
    public Conductor conductor;
    public GameObject notePrefab;
    public Transform[] pathPositions;

    //This is ripped from InputHandler for hard coded notes
    public List<double> noteBeats = new List<double> { 1.0, 3.0, 5.0, 7.0, 9.0, 11.0 };
    private List<double> noteTimes = new List<double> { };
    private int nextNoteIndex = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        noteTimes = noteBeats.Select(noteBeats => noteBeats * conductor.quarterNote).ToList();
    }

    // Update is called once per frame
    void Update()
    {
        //Basically spawns a note at the hard coded note time.
        if(nextNoteIndex < noteTimes.Count && conductor.songPosition >= noteTimes[nextNoteIndex])
        {
            SpawnNote((float)noteBeats[nextNoteIndex]);
            nextNoteIndex++;
        }
    }

    //The thing that actually spawns the note.
    void SpawnNote(float spawnBeat)
    {
        GameObject noteObj = Instantiate(notePrefab, new Vector3(0, 0, 0), Quaternion.identity);
        Note note = noteObj.GetComponent<Note>();
        note.spawnBeat = spawnBeat;
        note.hitBeat = spawnBeat + 4f;
        note.beatPositions = pathPositions;
    }
}
