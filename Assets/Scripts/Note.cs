using UnityEngine;

public class Note : MonoBehaviour
{
    public float spawnBeat;
    public float hitBeat;
    public Transform[] beatPositions;
    public int totalSteps = 4;

    private Conductor conductor;
    private int currentStep = 0;
    private float nextBeatTrigger;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        conductor = FindObjectOfType<Conductor>();
        nextBeatTrigger = spawnBeat + 1f / totalSteps;
        transform.position = beatPositions[0].position;
    }

    // Update is called once per frame
    void Update()
    {
        if (conductor.songPositionInBeats >= nextBeatTrigger)
        {
            currentStep++;
            if(currentStep < beatPositions.Length)
            {
                transform.position = beatPositions[currentStep].position;
            }
            nextBeatTrigger = spawnBeat + (currentStep + 1f) * (hitBeat - spawnBeat) / totalSteps;
        }
        
        if(conductor.songPositionInBeats > hitBeat + 1f)
        {
            Destroy(gameObject);
        }
    }
}
