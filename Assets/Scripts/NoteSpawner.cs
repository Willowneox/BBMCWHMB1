using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class NoteSpawner : MonoBehaviour
{
    public Conductor conductor;
    public GameObject notePrefab;
    public List<double> noteBeats = new List<double> { 2.0, 4.0, 6.0, 8.0, 10.0 };
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
        if(nextNoteIndex < noteTimes.Count && conductor.songPosition >= noteTimes[nextNoteIndex])
        {
            Instantiate(notePrefab, new Vector3(0,0,0), Quaternion.identity);
            nextNoteIndex++;
        }
    }
}
