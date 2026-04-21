using UnityEngine;

public class Lever : MonoBehaviour
{
    public Door door;

    public AudioSource audioSource;
    public AudioClip leverSound;

    private bool playerNearby = false;
    private bool used = false;

    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E) && !used)
        {
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            Debug.Log("Presiona E para usar palanca");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
        }
    }
}