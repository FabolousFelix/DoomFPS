using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalDoor : MonoBehaviour
{
    public KeyPedestal[] pedestals;

    public GameObject door;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip openDoorSound;

    private bool opened = false;

    void Update()
    {
        if (opened) return;

        if (AllPedestalsActivated())
        {
            OpenDoor();
            opened = true;
        }
    }

    bool AllPedestalsActivated()
    {
        foreach (var p in pedestals)
        {
            if (!p.isActivated)
                return false;
        }
        return true;
    }

    void OpenDoor()
    {
        Debug.Log("PUERTA ABIERTA");

        if (audioSource != null && openDoorSound != null)
        {
            audioSource.PlayOneShot(openDoorSound);
        }

        if (door != null)
        {
            Destroy(door, 1.5f); // delay para que el audio se escuche
        }
    }
}
