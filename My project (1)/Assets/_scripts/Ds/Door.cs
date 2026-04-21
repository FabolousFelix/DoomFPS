using UnityEngine;

public class Door : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip openSound;

    // Evita que la puerta se abra más de una vez
    private bool isOpen = false;

    // Método público para abrir la puerta (puede llamarse desde otros scripts)
    public void OpenDoor()
    {
        // Si la puerta ya está abierta, no hace nada
        if (isOpen) return;
        // Marca la puerta como abierta
        isOpen = true;

        //reproducir sonido
        if (audioSource != null && openSound != null)
        {
            audioSource.PlayOneShot(openSound);
        }

        //desaparecer puerta
        gameObject.SetActive(false);
    }
}