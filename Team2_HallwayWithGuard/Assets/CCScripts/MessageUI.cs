using UnityEngine;
using TMPro;
using System.Collections;

public class TopMessageUI : MonoBehaviour
{
    public static TopMessageUI Instance;

    public TextMeshProUGUI messageText;
    public CanvasGroup canvasGroup;

    public float fadeTime = 0.4f;
    public float messageDuration = 2.5f;

    private Coroutine currentMessage;

    void Awake()
    {
        Instance = this;

        messageText.text = "";
        canvasGroup.alpha = 0f;
    }

    public void ShowMessage(string message)
    {
        if (currentMessage != null)
            StopCoroutine(currentMessage);

        currentMessage = StartCoroutine(MessageRoutine(message));
    }

    IEnumerator MessageRoutine(string message)
    {
        messageText.text = message;

        float t = 0;
        while (t < fadeTime)
        {
            canvasGroup.alpha = Mathf.Lerp(0, 1, t / fadeTime);
            t += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 1;

        yield return new WaitForSeconds(messageDuration);

        t = 0;
        while (t < fadeTime)
        {
            canvasGroup.alpha = Mathf.Lerp(1, 0, t / fadeTime);
            t += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 0;
        messageText.text = "";
    }
}
