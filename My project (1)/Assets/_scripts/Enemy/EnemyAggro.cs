
using UnityEngine;

public class EnemyAggro : MonoBehaviour
{
    public bool isAggro;

    public float distanceToAggro = 8f;

    [HideInInspector] public Transform playerTransform;

    //[HideInInspector] public Animator animator;

    private EnemyAttack enemyAttack;

    private void Start()
    {
        if (playerTransform == null)
            playerTransform = FindAnyObjectByType<PlayerMovement>().transform;

        enemyAttack = GetComponentInChildren<EnemyAttack>();
        //animator = GetComponent<Animator>();
        isAggro = false;
    }

    private void Update()
    {
        CheckEnemyAggro();
        //AnimationChange();
    }

    public void CheckEnemyAggro()
    {
        var dist = Vector3.Distance(transform.position, playerTransform.position);
        
        if (dist > distanceToAggro)
        {
            isAggro = false;
        }
        else
        {
            isAggro = true;
        }
    }
    /*
    public void AnimationChange()
    {
        animator.SetBool("isChasing", isAggro);
    }

    /
    public void EnemyDamage()
    {
        if (enemyAttack.isAttacking)
        {
            playerTransform.GetComponent<PlayerStats>().PlayerDamage();
            Debug.Log(playerTransform.GetComponent<PlayerStats>().health);
        }
    }*/
}
