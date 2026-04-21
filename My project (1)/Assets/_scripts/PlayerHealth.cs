using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Vida")]
    public float maxHealth = 100f;
    public float currentHealth;

    public System.Action OnHealthChanged;

    [Header("Escudo")]
    public float maxShield = 50f;
    public float currentShield;

    void Start()
    {
        currentHealth = maxHealth;
        currentShield = maxShield;

        OnHealthChanged?.Invoke();
    }

    public void TakeDamage(float damage)
    {
        float remainingDamage = damage;

        //primero absorbe el escudo
        if (currentShield > 0)
        {
            float shieldDamage = Mathf.Min(currentShield, remainingDamage);
            currentShield -= shieldDamage;
            remainingDamage -= shieldDamage;
        }

        //luego afecta la vida
        if (remainingDamage > 0)
        {
            currentHealth -= remainingDamage;
            currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        }

        Debug.Log($"Escudo: {currentShield} | Vida: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }

        OnHealthChanged?.Invoke();
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        Debug.Log("Curación: " + amount + " | Vida: " + currentHealth);
    }

    public void HealShield(float amount)
    {
        currentShield = Mathf.Clamp(currentShield + amount, 0, maxShield);

        Debug.Log("Escudo curado: " + amount + " | Escudo actual: " + currentShield);

        OnHealthChanged?.Invoke();
    }

    void Die()
    {
        Debug.Log("Player muerto");
        //añadir luego la ui y respawn que si funcione esta vez xd

        // Ejemplo simple:
        gameObject.SetActive(false);
    }
}