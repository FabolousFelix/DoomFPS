using UnityEngine;

public class InvisibilityVisualEffect : MonoBehaviour
{
    public static InvisibilityVisualEffect instance;

    public GameObject invisibleOverlay;

    private void Awake()
    {
        instance = this;

        invisibleOverlay.SetActive(false);
    }

    public void EnableEffect()
    {
        invisibleOverlay.SetActive(true);
    }

    public void DisableEffect()
    {
        invisibleOverlay.SetActive(false);
    }
}