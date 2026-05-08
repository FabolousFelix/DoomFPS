
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    // Guarda la salud maxima del jugador
    public int maxHealth;

    // Se inicializa la variable que se modifica en el juego
    [HideInInspector] public int health;

    // Start is called before the first frame update
    void Start()
    {
        // Se le asigna a la variable el valor de salud maxima
        health = maxHealth;
    }

    public void PlayerDamage()
    {
        // Si tiene más de 1 de vida
        if (health > 1)
        {
            // Reduce la vida en 1
            health--; 
            Debug.Log(health);


        }
        // Si la vida está en 1, el siguiente golpe lo mata
        else if (health == 1)
        {
            Debug.Log("Personaje Muere");
        }
    }

    // Método para curar al jugador
    public void Heal(int amount)
    {
        // Suma la cantidad de vida
        health += amount;

        // evitar que se pase del máximo
        if (health > maxHealth)
        {

            health = maxHealth;
        }

        Debug.Log("Curado: " + amount + " | Vida actual: " + health);
    }

}
