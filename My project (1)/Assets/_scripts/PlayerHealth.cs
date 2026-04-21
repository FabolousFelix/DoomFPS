using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Vida")]
    public float maxHealth = 100f;
    public float currentHealth;

    public System.Action OnHealthChanged;
    public System.Action OnShieldChanged;
    public System.Action OnPlayerDeath;

    [Header("Escudo")]
    public float maxShield = 50f;
    public float currentShield;

    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI armorText;

    void Start()
    {
        currentHealth = maxHealth;
        currentShield = maxShield;
        UpdateHealthUI();
        UpdateArmorUI();

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
            UpdateArmorUI();
            OnShieldChanged?.Invoke();
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
        UpdateHealthUI(); // Actualiza el texto al curarse
        OnHealthChanged?.Invoke();
    }

    private void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = $"{currentHealth}"; // O usa el formato que prefieras
        }
    }
    private void UpdateArmorUI()
    {
        if (armorText != null)
        {
            armorText.text = $"{currentShield}"; // O usa el formato que prefieras
        }
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        Debug.Log("Curación: " + amount + " | Vida: " + currentHealth);
        UpdateHealthUI(); // Actualiza el texto al curarse
        OnHealthChanged?.Invoke();
    }

    public void HealShield(float amount)
    {
        currentShield = Mathf.Clamp(currentShield + amount, 0, maxShield);

        Debug.Log("Escudo curado: " + amount + " | Escudo actual: " + currentShield);
        UpdateArmorUI();
        OnShieldChanged?.Invoke();
        OnHealthChanged?.Invoke();
    }

    void Die()
    {
        Debug.Log("Player muerto");
        //añadir luego la ui y respawn que si funcione esta vez xd

        // Ejemplo simple:
        StartCoroutine(DeathSequence());
        gameObject.SetActive(false);
        OnPlayerDeath?.Invoke();
    }

    private System.Collections.IEnumerator DeathSequence()
    {
        // Aquí puedes reproducir una animación, sonido, etc.
        yield return new WaitForSeconds(1f);
        OnPlayerDeath?.Invoke();
    }
}