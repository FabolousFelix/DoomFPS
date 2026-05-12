using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float speed = 10f;
    public float damage = 10f;
    public float lifeTime = 5f;

    private Vector3 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

    }
     public void SetDirection(Vector3 dir)
    {
        moveDirection = dir.normalized;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth ph = other.GetComponent<PlayerHealth>();

            if (ph != null)
            {
                ph.TakeDamage(damage);
            }

            Destroy(gameObject);
        }
        
    }
}
