using UnityEngine;

public class Note : MonoBehaviour
{
    public float spawnBeat; //This is when the note spawns
    public float hitBeat; //This is when the note should be hit
    public Vector3 spawnPosition;
    public Vector3 hitPosition;

    public bool wasHit = false;
    public bool wasMissed = false;

    private Conductor conductor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        conductor = FindObjectOfType<Conductor>();
        transform.position = spawnPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if(wasHit || wasMissed) return;

        float currentBeat = conductor.songPositionInBeats;

        if (currentBeat < spawnBeat) return;

        float t = Mathf.InverseLerp(spawnBeat, hitBeat, currentBeat);

        transform.position = Vector3.Lerp(spawnPosition, hitPosition, t);

        if(currentBeat > hitBeat + 0.25f)
        {
            Miss();
        }
    }

    public void Hit()
    {
        if(wasHit || wasMissed) return;
        wasHit = true;
        gameObject.SetActive(false);
    }

    public void Miss()
    {
        if(wasHit || wasMissed) return;
        wasMissed = true;
        gameObject.SetActive(false);
    }
}
