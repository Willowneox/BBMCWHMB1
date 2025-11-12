using UnityEngine;

public class Note : MonoBehaviour
{
    public float hitBeat;
    public bool wasHit = false;
    public bool wasMissed = false;

    private Conductor conductor;
    private int currentStep = 0;

    public Transform[] beatPositions;
    public int totalSteps = 4;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        conductor = FindObjectOfType<Conductor>();

        if(beatPositions != null && beatPositions.Length > 0)
        {
            currentStep = 0;
            transform.position = beatPositions[0].position; 
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(wasHit || wasMissed) return;
        
        //Mises if player doesn't hit the note in time
        if(conductor.songPositionInBeats > hitBeat + 0.25f)
        {
            Miss();
            return;
        }
        
        //Moves the note to the beat
        if(beatPositions != null && beatPositions.Length > 1)
        {
            float beatsUntilHit = hitBeat - conductor.songPositionInBeats;
            float totalBeatsTravel = beatPositions.Length - 1;

            //See how many steps that have been moved
            int step = Mathf.Clamp(Mathf.FloorToInt(totalBeatsTravel - beatsUntilHit), 0, beatPositions.Length - 1);

            if (step != currentStep)
            {
                currentStep = currentStep;
                transform.position = beatPositions[currentStep].position;
            }
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
