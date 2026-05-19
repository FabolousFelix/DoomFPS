using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Animator (opcional)")]
    public Animator animator;
    public string enterTrigger = "Hover";
    public string exitTrigger = "Normal";

    [Header("Fallback: Scale animation")]
    public bool useScaleFallback = true;
    public Vector3 hoverScale = new Vector3(1.1f, 1.1f, 1f);
    public float scaleDuration = 0.12f;

    RectTransform rect;
    Vector3 initialScale;
    Coroutine scaleRoutine;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
        if (rect == null)
            rect = gameObject.AddComponent<RectTransform>();
        initialScale = rect.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (animator != null)
        {
            animator.ResetTrigger(exitTrigger);
            animator.SetTrigger(enterTrigger);
            return;
        }

        if (useScaleFallback)
            StartScaleTo(hoverScale);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (animator != null)
        {
            animator.ResetTrigger(enterTrigger);
            animator.SetTrigger(exitTrigger);
            return;
        }

        if (useScaleFallback)
            StartScaleTo(initialScale);
    }

    void StartScaleTo(Vector3 target)
    {
        if (scaleRoutine != null) StopCoroutine(scaleRoutine);
        scaleRoutine = StartCoroutine(ScaleTo(target));
    }

    IEnumerator ScaleTo(Vector3 target)
    {
        Vector3 start = rect.localScale;
        float elapsed = 0f;
        // Usar Time.unscaledDeltaTime porque el menú inicializa Time.timeScale = 0
        while (elapsed < scaleDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            float t = Mathf.SmoothStep(0f, 1f, Mathf.Clamp01(elapsed / scaleDuration));
            rect.localScale = Vector3.Lerp(start, target, t);
            yield return null;
        }
        rect.localScale = target;
        scaleRoutine = null;
    }
}