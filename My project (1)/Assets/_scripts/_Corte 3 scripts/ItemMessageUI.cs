using System.Collections;
using TMPro;
using UnityEngine;

public class ItemMessageUI : MonoBehaviour
{
    public static ItemMessageUI instance;

    [Header("UI")]
    public GameObject messagePanel;
    public TextMeshProUGUI messageText;

    private Coroutine currentRoutine;

    private void Awake()
    {
        instance = this;

        messagePanel.SetActive(false);
    }

    public void ShowMessage(string text, float duration)
    {
        if (currentRoutine != null)
        {
            StopCoroutine(currentRoutine);
        }

        currentRoutine = StartCoroutine(
            MessageRoutine(text, duration)
        );
    }

    IEnumerator MessageRoutine(string text, float duration)
    {
        messagePanel.SetActive(true);

        messageText.text = text;

        yield return new WaitForSeconds(duration);

        messagePanel.SetActive(false);
    }
}