using UnityEngine;

public class Lever : MonoBehaviour
{
    // Referencia a la puerta que se abrirá
    public Door door;
    // Fuente de audio
    public AudioSource audioSource;
    // Sonido de la palanca
    public AudioClip leverSound;
    // Indica si el jugador está dentro del rango de interacción
    private bool playerNearby = false;
    // Evita que la palanca se use más de una vez
    private bool used = false;

    void Update()
    {
        // Si el jugador está cerca, presiona E y no se ha usado aún
        if (playerNearby && Input.GetKeyDown(KeyCode.E) && !used)
        {
            // Marca la palanca como usada
            used = true;

            //sonido de palanca
            if (audioSource != null && leverSound != null)
            {
                audioSource.PlayOneShot(leverSound);
            }

            //abre puerta
            if (door != null)
            {
                door.OpenDoor();
            }
        }
    }
    // Detecta cuando el jugador entra en el área de la palanca
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Permite interacción
            playerNearby = true;
            Debug.Log("Presiona E para usar palanca");
        }
    }
    // Detecta cuando el jugador sale del área
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Desactiva interacción
            playerNearby = false;
        }
    }
}