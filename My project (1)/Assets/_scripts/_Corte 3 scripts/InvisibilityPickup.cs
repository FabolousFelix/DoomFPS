using UnityEngine;

public class InvisibilityPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerPowerUps p = other.GetComponent<PlayerPowerUps>();

            if (p != null)
            {
                p.hasInvisibility = true;
                Debug.Log("Invisibilidad disponible (Q)");
            }

            Destroy(gameObject);
        }
    }
}