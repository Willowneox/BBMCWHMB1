using UnityEngine;
using System.Collections;

public class ConveyorBeltScroller : MonoBehaviour
{
    public Conductor conductor;
    public Transform[] beltSegments;


    public float unitsPerBeat = 4.5f;
    public float snapDuration = 0.1f;
    public AnimationCurve snapEase = AnimationCurve.EaseInOut(0, 0, 1, 1);

    private float segmentWidth;
    private float lastBeat = -1;
    private Coroutine snapRoutine;

    void Start()
    {
        var sr = beltSegments[0].GetComponent<SpriteRenderer>();
        segmentWidth = sr.bounds.size.x;
    }

    void Update()
    {
        float beat = Mathf.Floor(conductor.songPositionInBeats);

        if (beat > lastBeat)
        {
            lastBeat = beat;
            MoveBelt();
        }
    }

    void MoveBelt()
    {
        Vector3[] oldPositions = new Vector3[beltSegments.Length];

        for (int i = 0; i < beltSegments.Length; i++)
        {
            oldPositions[i] = beltSegments[i].position;

            beltSegments[i].position += Vector3.left * unitsPerBeat;

            if (beltSegments[i].position.x <= -segmentWidth)
            {
                beltSegments[i].position += new Vector3(segmentWidth * beltSegments.Length, 0, 0);
            }
        }

        if (snapRoutine != null)
        {
            StopCoroutine(snapRoutine);
        }

        snapRoutine = StartCoroutine(SnapAnimation(oldPositions));
    }

    IEnumerator SnapAnimation(Vector3[] startPositions)
    {
        float elapsed = 0f;

        Vector3[] endPositions = new Vector3[beltSegments.Length];
        for (int i = 0; i < beltSegments.Length; i++)
            endPositions[i] = beltSegments[i].position;

        while (elapsed < snapDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / snapDuration);
            float easeT = snapEase.Evaluate(t);

            for (int i = 0; i < beltSegments.Length; i++)
            {
                beltSegments[i].position = Vector3.Lerp(
                    startPositions[i],
                    endPositions[i],
                    easeT
                );
            }

            yield return null;
        }

        for (int i = 0; i < beltSegments.Length; i++)
        {
            beltSegments[i].position = endPositions[i];
        }
    }
}
