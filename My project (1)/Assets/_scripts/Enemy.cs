using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Variable pública que representa la vida del enemigo
    public float health;
    // Referencia a un prefab (efecto de sangre)
    public GameObject blood;

    // Variable privada que indica si el enemigo(boss) es invulnerable
    private bool isInvulnerable = false;

    public int pointsOnDeath;

    void Update()
    {
        // Llama constantemente a la función que revisa si el enemigo debe morir
        EnemyDeath();
    }

    // Función pública para aplicar daño al enemigo
    public void Damage(float damage, Quaternion rot)
    {
        // Si el enemigo es invulnerable, no recibe daño
        if (isInvulnerable)
        {
          
            return;// Sale de la función
        }
        // Muestra la vida actual en consola
        Debug.Log("VIDA ACTUAL: " + health);

        // Reproduce un sonido de daño usando un AudioManager (patrón singleton)
        AudioManager.instance.PlayEnemyDamage();
        // Resta el daño recibido a la vida del enemigo
        health -= damage;
        // Instancia el efecto de sangre en la posición del enemigo con la rotación recibida
        GameObject gunEffect = Instantiate(blood, transform.position, rot);
        // Destruye el efecto después de 0.5 segundos para no saturar la escena
        Destroy(gunEffect, 0.5f);
    }

    public void EnemyDeath()
    {
        // Si la vida es menor o igual a 0
        if (health <= 0)
        {
            ScoreManager.instance.AddScore(pointsOnDeath);
            // Notifica al EnemyManager que este enemigo debe eliminarse de la lista
            EnemyManager.instance.RemoveEnemy(this);
            // Destruye el objeto del enemigo en la escena
            Destroy(gameObject);
        }
    }

    public void SetInvulnerable(bool value)  // Función para activar o desactivar la invulnerabilidad del enemigo
    {
        isInvulnerable = value; // Asigna el valor recibido
    }
}