using UnityEngine;

public class SlowVisualEffects : MonoBehaviour
{
    public GameObject slowOverlay;

    private void Start()
    {
        slowOverlay.SetActive(false);
    }

    public void EnableEffect()
    {
        Debug.Log("ACTIVANDO EFECTO");

        slowOverlay.SetActive(true);
    }

    public void DisableEffect()
    {
        slowOverlay.SetActive(false);
    }
}