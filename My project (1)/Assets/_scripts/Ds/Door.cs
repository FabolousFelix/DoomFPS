using UnityEngine;

public class Door : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip openSound;

    private bool isOpen = false;

    public void OpenDoor()
    {
        if (isOpen) return;

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