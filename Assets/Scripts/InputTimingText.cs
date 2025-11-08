using UnityEngine;
using UnityEngine.UI;

public class InputTimingText : MonoBehaviour
{
    //Average Offset Variables
    public double totalOffset;
    public double beatsHit;


    /// <summary>
    /// The text component that belongs to the "InputTimingText" node in the editor.
    /// </summary>
    public Text text;
    /// <summary>
    /// The default string that another string can be added onto later.
    /// </summary>
    private string defaultText = "Hit Spacebar to the beat.\nYour hit time is:\n";
    /// <summary>
    /// The conductor node in the editor tree.
    /// </summary>
    public Conductor conductor;

    /// <summary>
    /// Start is called once before the first execution of Update after the MonoBehaviour is created
    /// </summary>
    void Start()
    {
        text.text = defaultText;
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        // TODO: Allow the user to change controls
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.X))
        {

            double hitTime = GetHitTime();
            Judgement judgement = Judgements.GetRating(hitTime);
            text.text = defaultText + hitTime + " ms. " + judgement + "\nAverage Offset: " + totalOffset/beatsHit;
        }
    }

    /// <summary>
    /// Return the hit time in milliseconds.
    /// </summary>
    /// <returns></returns>
    double GetHitTime()
    {
        // Modular division by quarterNote because there is a beat every quarterNote seconds.
        double hitTime = conductor.songPosition % conductor.quarterNote;
        // If the hit time is greater than half the time between beats, the hit is counted for the next beat instead.
        if (hitTime > conductor.quarterNote / 2)
        {
            hitTime -= conductor.quarterNote;
        }
        // Then convert seconds to milliseconds.
        hitTime *= 1000.0;

        //Tallies beat hit and then adds offset to the total offset
        beatsHit += 1;
        totalOffset += hitTime;
        return hitTime;
    }
}
