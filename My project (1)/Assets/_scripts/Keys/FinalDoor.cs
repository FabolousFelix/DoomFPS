using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalDoor : MonoBehaviour
{
    public KeyPedestal[] pedestals;

    public GameObject door;

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

        if (door != null)
        {
            Destroy(door);
        }
    }
}
