using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health;
    public GameObject blood;

    private bool isInvulnerable = false;

    void Update()
    {
        EnemyDeath();
    }

    public void Damage(float damage, Quaternion rot)
    {
        if (isInvulnerable)
        {
            Debug.Log("Enemigo invulnerable");
            return;
        }
        Debug.Log("VIDA ACTUAL: " + health);
        AudioManager.instance.PlayEnemyDamage();

        health -= damage;

        GameObject gunEffect = Instantiate(blood, transform.position, rot);
        Destroy(gunEffect, 0.5f);
    }

    public void EnemyDeath()
    {
        if (health <= 0)
        {
            EnemyManager.instance.RemoveEnemy(this);
            Destroy(gameObject);
        }
    }

    public void SetInvulnerable(bool value)
    {
        isInvulnerable = value;
    }
}