using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostPickup : MonoBehaviour
{
    public float speedMultiplier = 1.5f;
        public float damageMultiplier = 2f;
    public float duration = 3f;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerPowerUps p = other.GetComponent<PlayerPowerUps>();

            if (p != null)
            {
                p.ActivateBoost(speedMultiplier, damageMultiplier, duration);
            }
            Destroy(gameObject);
        }
    }

}
