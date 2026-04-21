using UnityEngine;

public class SecretDoorDisappear : MonoBehaviour
{
    // Referencia al componente Renderer
    private Renderer rend;
    // Referencia al Collider (lo que permite colisiones)
    private Collider col;

    void Start()
    {
        // Obtiene el componente Renderer del mismo objeto
        rend = GetComponent<Renderer>();
        // Obtiene el componente Collider del mismo objeto
        col = GetComponent<Collider>();
    }

    // Se ejecuta cuando otro objeto entra en el trigger
    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que entró tiene el tag "Player"
        if (other.CompareTag("Player"))
        {
            rend.enabled = false; // desactiva el renderer = invisible
            col.enabled = false;  // desactiva el collider = sin colisión
        }
    }
}