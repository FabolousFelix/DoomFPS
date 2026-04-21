using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Key : MonoBehaviour
{
    public int keyType;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            switch (keyType)
            {
                case 0:
                    KeyManager.instance.redKeyImage.SetActive(true);
                    Stats.hasRedKey = true;
                    break;
                case 1:
                    KeyManager.instance.blueKeyImage.SetActive(true);
                    Stats.hasBlueKey = true;
                    break;
                case 2:
                    KeyManager.instance.purpleKeyImage.SetActive(true);
                    Stats.hasPurpleKey = true;
                    break;

                    default:
                    break;
            }
            Destroy(gameObject);
        }
    }
}
