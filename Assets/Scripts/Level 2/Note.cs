using UnityEngine;
using System.Collections;

public class Note : MonoBehaviour
{
    public float spawnBeat; //This is when the note spawns
    public float hitBeat; //This is when the note should be hit (tap notes only)
    public float pressBeat; //This is when the note should be hit
    public float releaseBeat; //This is when the note should be released
    

    //Dictates note path
    public Vector3 spawnPosition;
    public Vector3 hitPosition;

    public bool wasHit = false;
    public bool wasMissed = false;

    private Conductor conductor;

    private int totalSteps = 4;
    private int currentStep = -1;
    private float stepLength;
    private Coroutine moveRoutine;

    //Sprites
    public Sprite rawSprite;
    public Sprite perfectSprite;
    public Sprite goodSprite;
    public Sprite overcookedSprite;
    private SpriteRenderer sr;

    //Removing the note from the screen
    private bool slidingOff = false;
    public float slideSpeed = 10f;

    //Note scaling
    public float holdLengthBeats; //Length of hold in beats
    private Vector3 baseScale;

    [SerializeField] private float snapDuration = 0.1f;
    [SerializeField] private AnimationCurve snapEase = AnimationCurve.EaseInOut(0, 0, 1, 1);

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = rawSprite;
        baseScale = transform.localScale;
     
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        conductor = FindObjectOfType<Conductor>();

        stepLength = (pressBeat - spawnBeat) / totalSteps;
        transform.position = spawnPosition;

        transform.localScale = baseScale * holdLengthBeats;
    }

    // Update is called once per frame
    void Update()
    {
        if (slidingOff)
        {
            transform.position += Vector3.left * slideSpeed * Time.deltaTime;
            return;
        }

        if (wasHit || wasMissed) return;

        float currentBeat = conductor.songPositionInBeats;

        int newStep = Mathf.FloorToInt((currentBeat - spawnBeat) / stepLength);
        newStep = Mathf.Clamp(newStep, 0, totalSteps);

        if (newStep != currentStep)
        {
            currentStep = newStep;
            float t = currentStep / (float)totalSteps;

            Vector3 newPosition = Vector3.Lerp(spawnPosition, hitPosition, t);
            if (moveRoutine != null)
            {
                StopCoroutine(moveRoutine);
            }

            moveRoutine = StartCoroutine(SnapMove(transform.position, newPosition));
        }

        //Misses if note is ignored by player
        if (!wasHit && !wasMissed && currentBeat > releaseBeat + 0.25f)
        {
            Miss();
        }


    }

    private IEnumerator SnapMove(Vector3 startpos, Vector3 endPos)
    {
        float elapsed = 0f;
        while (elapsed < snapDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / snapDuration);
            float easeT = snapEase.Evaluate(t);
            transform.position = Vector3.Lerp(startpos, endPos, easeT);
            yield return null;
        }
        transform.position = endPos;
    }

    public void Hit()
    {
        if (wasHit || wasMissed) return;
        wasHit = true;

    }

    public void Miss()
    {
        if (wasHit) return; //Just in case if the note was already hit, it can't trigger a miss.

        if (wasHit || wasMissed) return;
        wasMissed = true;

        ApplyJudgement(Judgement.Miss);
        SlideOffScreen();

    }

    public void ApplyJudgement(Judgement result)
    {
        switch (result)
        {
            case Judgement.Perfect:
                sr.sprite = perfectSprite;
                break;
            case Judgement.Good:
                sr.sprite = goodSprite;
                break;

            case Judgement.Miss:
            default:
                sr.sprite = overcookedSprite;
                break;
        }


    }

    public void SlideOffScreen()
    {
        if(slidingOff) return; //Prevent multiple calls
        slidingOff = true;

        //Debug.Log("Starting slide off");
        Destroy(gameObject, 2f);
    }
}
