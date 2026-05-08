using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    public float newGravity;
    public bool isDamaging;

    public float timeBetweenDamage;
    private float cooldown;

    public PlayerStats playerStats;

    private void Start()
    {
        // Inicializa el cooldown
        cooldown = 0;
    }

    private void Update()
    {
        // Llama constantemente al sistema de daño
        Damage();
    }

    // Cuando el jugador entra en la lava
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Cambia la gravedad del jugador
            other.GetComponent<PlayerMovement>().gravity = newGravity;
            // Activa el daño continuo
            isDamaging = true;

        }
    }
    // Cuando el jugador sale de la lava
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Restaura la gravedad normal
            other.GetComponent<PlayerMovement>().gravity = Physics.gravity.y;
            // Detiene el daño
            isDamaging = false;
        }
    }

    // Método que aplica daño con intervalo
    private void Damage()
    {
        // Si está activo el daño y ya pasó el cooldown
        if (isDamaging && Time.time > cooldown)
        {
            // Aplica daño al jugador
            playerStats.PlayerDamage();
            // Reinicia el cooldown
            cooldown = Time.time + timeBetweenDamage;
        }
    }
}
