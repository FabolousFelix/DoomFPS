using UnityEngine;

public class HealItem : MonoBehaviour
{
    public float healAmount = 30f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth player = other.GetComponent<PlayerHealth>();

            if (player != null)
            {
                player.Heal(healAmount);
            }

            Destroy(gameObject);
        }
    }
}

