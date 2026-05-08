using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalDoor : MonoBehaviour
{
    // Array de pedestales que deben activarse para abrir la puerta
    public KeyPedestal[] pedestals;
    // Referencia al objeto puerta
    public GameObject door;

    [Header("Audio")]
    public AudioSource audioSource;
    // Sonido que se reproduce al abrir la puerta
    public AudioClip openDoorSound;
    // Evita que la puerta se abra más de una vez
    private bool opened = false;

    void Update()
    {
        // Si ya se abrió la puerta, no hace nada
        if (opened) return;

        // Si todos los pedestales están activados, abre la puerta
        if (AllPedestalsActivated())
        {
            OpenDoor();
            opened = true;// evita que se repita
        }
    }
    // Revisa si todos los pedestales están activados
    bool AllPedestalsActivated()
    {
        // Recorre todos los pedestales del array
        foreach (var p in pedestals)
        {
            // Si alguno NO está activado, devuelve falso
            if (!p.isActivated)
                return false;
        }
        // Si todos están activados, devuelve verdadero
        return true;
    }

    void OpenDoor()
    {
        Debug.Log("PUERTA ABIERTA");

        if (audioSource != null && openDoorSound != null)
        {
            // Reproduce el sonido de apertura si todo está asignado
            audioSource.PlayOneShot(openDoorSound);
        }
        // Destruye la puerta después de 1.5 segundos
        // (para dejar tiempo al sonido antes de desaparecer)
        if (door != null)
        {
            Destroy(door, 1.5f); // delay para que el audio se escuche
        }
    }
}
