using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowZone : MonoBehaviour
{
    public float slowMultiplier;

    public SlowVisualEffects slowEffect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement movement =
                other.GetComponent<PlayerMovement>();

            if (movement != null)
            {
                movement.speed =
                    movement.originalSpeed * slowMultiplier;
            }

            slowEffect.EnableEffect();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement movement =
                other.GetComponent<PlayerMovement>();

            if (movement != null)
            {
                movement.speed =
                    movement.originalSpeed;
            }

            slowEffect.DisableEffect();
        }
    }
}