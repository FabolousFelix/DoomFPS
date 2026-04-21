using UnityEngine;

public class SecretDoorDisappear : MonoBehaviour
{
    private Renderer rend;
    private Collider col;

    void Start()
    {
        rend = GetComponent<Renderer>();
        col = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            rend.enabled = false; // invisible
            col.enabled = false;  // sin colisión
        }
    }
}