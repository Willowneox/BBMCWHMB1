using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class InstructionCard : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public TMP_Text instructionText;

    public float showTime = 1.0f;
    public float fadeTime = 0.1f;

    Coroutine routine;

    void Awake()
    {
        canvasGroup.alpha = 0f;
    }

    public void ShowMessage(string msg)
    {
        instructionText.text = msg;

        if (routine != null)
            StopCoroutine(routine);

        routine = StartCoroutine(ShowRoutine());
    }

    IEnumerator ShowRoutine()
    {
        // Fade in
        float t = 0f;
        while (t < fadeTime)
        {
            t += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, t / fadeTime);
            yield return null;
        }

        canvasGroup.alpha = 1f;

        // Stay visible
        yield return new WaitForSeconds(showTime);

        // Fade out
        t = 0f;
        while (t < fadeTime)
        {
            t += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, t / fadeTime);
            yield return null;
        }

        canvasGroup.alpha = 0f;
    }
}