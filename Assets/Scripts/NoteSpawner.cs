using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class NoteSpawner : MonoBehaviour
{
    public Conductor conductor;
    public GameObject notePrefab;
    public Transform spawnPoint;
    public Transform hitPoint;


    //Hardcoded notes
    public List<double> noteBeats = new List<double> { 1.0, 3.0, 5.0, 7.0, 9.0, 11.0 };
    private int nextNoteIndex = 0;


    // Update is called once per frame
    void Update()
    {
        if(conductor.songPositionInBeats < 0) return;

        //Basically spawns a note at the hard coded note time.
        while(nextNoteIndex < noteBeats.Count && conductor.songPositionInBeats + 4f >= noteBeats[nextNoteIndex])
        {
            SpawnNote((float)noteBeats[nextNoteIndex]);
            nextNoteIndex++;
        }
    }

    //The thing that actually spawns the note.
    void SpawnNote(float hitBeat)
    {
        GameObject noteObj = Instantiate(notePrefab, transform.position, Quaternion.identity);
        Note note = noteObj.GetComponent<Note>();

        //Spawn note 4 beats before it needs to be hit
        note.spawnBeat = hitBeat - 4f; 
        note.hitBeat = hitBeat;
        note.spawnPosition = spawnPoint.position;
        note.hitPosition = hitPoint.position;
    }
}
