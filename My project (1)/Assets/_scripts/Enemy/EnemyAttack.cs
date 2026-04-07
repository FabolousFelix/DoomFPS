
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private EnemyAggro enemyAggro;
    [HideInInspector] public bool isAttacking;

    private void Start()
    {
        enemyAggro = GetComponentInParent<EnemyAggro>();
        isAttacking = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Target"))
        {
            //enemyAggro.animator.SetBool("Attack", true);
            isAttacking = true;
            Debug.Log("Is Attacking");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Target"))
        {
            //enemyAggro.animator.SetBool("Attack", false);
            isAttacking = false;
            Debug.Log("Is Not Attacking");
        }
    }

}
