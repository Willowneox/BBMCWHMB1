using UnityEngine;
using TMPro;
using System.Collections;

public class AssemblyJudgement : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float displayDuration = 0.7f; // How long text stays visible
    Coroutine routine;

    public void ShowJudgement(string rating, float msOffset)
    {
        string timing = "";

        if (rating != "Miss") // Miss doesn't need early/late label
        {
            if (msOffset < 0)
                timing = $"{Mathf.Abs(msOffset):F0}ms EARLY";
            else
                timing = $"{msOffset:F0}ms LATE";
        }

        if (rating == "Miss")
            text.text = "MISS";
        else
            text.text = $"{rating}\n{timing}";

        // Cancel old routine if needed
        if (routine != null)
            StopCoroutine(routine);

        routine = StartCoroutine(ClearAfterDelay());
    }

    IEnumerator ClearAfterDelay()
    {
        yield return new WaitForSeconds(displayDuration);
        text.text = "";
    }
}