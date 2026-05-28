using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Variable pública que representa la vida del enemigo
    public float health;
    // Referencia a un prefab (efecto de sangre)
    public GameObject blood;

    // Clip de sonido de daño (asignar en el Inspector)
    public AudioClip damageClip;

    // Variable privada que indica si el enemigo(boss) es invulnerable
    private bool isInvulnerable = false;

    public int pointsOnDeath;

    private Animator animator;

    [Header("Resistencias")]
    public bool resistantToPhysical;
    public bool resistantToMagic;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        // Llama constantemente a la función que revisa si el enemigo debe morir
        EnemyDeath();
    }

    // Función pública para aplicar daño al enemigo
    public void Damage(float damage, DamageType damageType, Quaternion rot)
    {
        Debug.Log("ENTRÓ A DAMAGE");

        if (isInvulnerable)
        {
            return;
        }

        Debug.Log("VIDA ACTUAL: " + health);

        //audio seguro
        if (AudioManager.instance != null)
        {
            if (damageClip != null)
                AudioManager.instance.PlaySFX(damageClip);
            else
                AudioManager.instance.PlayEnemyDamage();
        }

        float finalDamage = damage;

        if (damageType == DamageType.Physical && resistantToPhysical)
        {
            finalDamage *= 0.5f;
            Debug.Log("Resistencia física");
        }

        if (damageType == DamageType.Magic && resistantToMagic)
        {
            finalDamage *= 0.5f;
            Debug.Log("Resistencia mágica");
        }

        health -= finalDamage;

        Debug.Log(
            "Tipo: " + damageType +
            " | Daño Base: " + damage +
            " | Daño Final: " + finalDamage +
            " | Vida restante: " + health);

        //sangre segura
        if (blood != null)
        {
            GameObject gunEffect =
                Instantiate(blood, transform.position, rot);

            Destroy(gunEffect, 0.5f);
        }
    }
    public void EnemyDeath()
    {
        // Si la vida es menor o igual a 0
        if (health <= 0)
        {
            ScoreManager.instance.AddScore(pointsOnDeath);
            // Notifica al EnemyManager que este enemigo debe eliminarse de la lista
            EnemyManager.instance.RemoveEnemy(this);

            animator.SetTrigger("Death");
            // Destruye el objeto del enemigo en la escena
            Destroy(gameObject);
        }
    }

    public void SetInvulnerable(bool value)  // Función para activar o desactivar la invulnerabilidad del enemigo
    {
        isInvulnerable = value; // Asigna el valor recibido
    }
}