using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackRanged : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;

    public float fireRate = 2f;
    public float attackRange = 10f;
    public float nextFireTime;

    private Transform player;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
      player = FindAnyObjectByType<PlayerMovement>().transform;
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;

        float dist = Vector3.Distance(transform.position, player.position);

        anim.SetTrigger("Attack");
        if (dist <= attackRange && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab,firePoint.position,Quaternion.identity);

        Vector3 dir = (player.position - firePoint.position).normalized;

        projectile.GetComponent<EnemyProjectile>().SetDirection(dir);


        projectile.transform.forward = dir;
    }
}
