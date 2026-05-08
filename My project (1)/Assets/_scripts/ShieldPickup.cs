using UnityEngine;

public class ShieldPickup : MonoBehaviour
{
    public float shieldAmount = 20f;

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si quien tocó el objeto es el jugador
        if (other.CompareTag("Player"))
        {
            // Busca el script PlayerHealth dentro del jugador o sus hijos
            PlayerHealth player = other.GetComponentInChildren<PlayerHealth>();
            // Si existe el jugador y su escudo no está al máximo
            if (player != null && player.currentShield < player.maxShield)
            {
                // Restaura escudo al jugador
                player.HealShield(shieldAmount);
                // Destruye el pickup después de ser usado
                Destroy(gameObject);
            }
        }
    }
}