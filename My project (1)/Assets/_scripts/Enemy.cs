using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health;
    public GameObject blood;
    // Start is called before the first frame update
    void Update()
    {
        EnemyDeath();
    }

    public void Damage(float damage, Quaternion rot)
    {
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
}
