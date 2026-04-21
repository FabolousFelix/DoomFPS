using UnityEngine;
using System.Collections;

public class BossAttack : MonoBehaviour
{
    public float damage = 20f;

    [Header("Cooldown")]
    public float attackCooldown = 2f;
    private float nextAttackTime = 0f;

    [Header("Slow Effect")]
    public float slowAmount = 0.5f;
    public float slowDuration = 2f;

    private Transform player;
    private PlayerHealth playerHealth;
    private PlayerMovement playerMovement;

    public float invulDuration;
    public float invulCooldown;

    [Header("Efecto visual")]
    public ParticleSystem invulParticles;
    private Enemy enemy;

    private void Start()
    {
        // Busca al jugador en escena
        player = FindAnyObjectByType<PlayerMovement>().transform;
        // Obtiene componentes del jugador
        playerHealth = player.GetComponent<PlayerHealth>();
        playerMovement = player.GetComponent<PlayerMovement>();
        // Obtiene el script Enemy del boss
        enemy = GetComponent<Enemy>();
        // Inicia el ciclo de invulnerabilidad del boss
        StartCoroutine(InvulnerabilityCycle());
    }

    public void TryAttack(bool isAttacking)
    {
        // Si no está en estado de ataque, no hace nada
        if (!isAttacking) return;
        // Si aún está en cooldown, no puede atacar
        if (Time.time < nextAttackTime) return;

        //daño
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
        }

        //slow
        if (playerMovement != null)
        {
            playerMovement.ApplySlow(slowAmount, slowDuration);
        }

        Debug.Log("Boss atacó");

        nextAttackTime = Time.time + attackCooldown;
    }


    //corutina para el ciclo de invulnerabilidad
    IEnumerator InvulnerabilityCycle()
    {
        while (true)
        {
            yield return new WaitForSeconds(invulCooldown);

            //ACTIVAR INVULNERABILIDAD
            enemy.SetInvulnerable(true);
            Debug.Log("Boss INVULNERABLE");

            //activar partículas
            if (invulParticles != null)
                invulParticles.Play();

            yield return new WaitForSeconds(invulDuration);

            //DESACTIVAR INVULNERABILIDAD
            enemy.SetInvulnerable(false);
            Debug.Log("Boss VULNERABLE");

            //detener partículas
            if (invulParticles != null)
                invulParticles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }
    }
}
