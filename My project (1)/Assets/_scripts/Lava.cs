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
        cooldown = 0;
    }

    private void Update()
    {
        Damage();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerMovement>().gravity = newGravity;
            isDamaging = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerMovement>().gravity = Physics.gravity.y;
            isDamaging = false;
        }
    }

    private void Damage()
    {
        if(isDamaging && Time.time > cooldown)
        {
            playerStats.PlayerDamage();
            cooldown = Time.time + timeBetweenDamage;
        }
    }
}
