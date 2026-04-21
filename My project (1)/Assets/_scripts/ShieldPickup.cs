using UnityEngine;

public class ShieldPickup : MonoBehaviour
{
    public float shieldAmount = 20f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth player = other.GetComponentInChildren<PlayerHealth>();

            if (player != null && player.currentShield < player.maxShield)
            {
                player.HealShield(shieldAmount);
                Destroy(gameObject);
            }
        }
    }
}