using UnityEngine;
public class EnemyAggro : MonoBehaviour
{
    public bool isAggro; public float distanceToAggro = 8f;
    [HideInInspector] public Transform playerTransform; public float attackCooldown = 1.5f;
    private float nextAttackTime = 0f;
    [HideInInspector] public Animator animator;
    private EnemyAttack enemyAttack;

    //variable para acceder al boss
    private BossAttack bossAttack;
    private PlayerHealth playerHealth;
    private void Start()
    {
        if (playerTransform == null) playerTransform = FindAnyObjectByType<PlayerMovement>().transform;
        enemyAttack = GetComponentInChildren<EnemyAttack>();
        playerHealth = playerTransform.GetComponent<PlayerHealth>();

        //acceder al boss basicamente lol
        bossAttack = GetComponent<BossAttack>();
        //animator = GetComponent<Animator>();
        isAggro = false;
    }
    private void Update()
    {
        CheckEnemyAggro();
        //AnimationChange();
        EnemyDamage();
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
    public void EnemyDamage()
    {
        if (enemyAttack.isAttacking && Time.time >= nextAttackTime)
        {
            //si es boss usa su propio ataque
            if (bossAttack != null)
            {
                bossAttack.TryAttack(enemyAttack.isAttacking);
            }
            else
            {
                // enemigo normal con cambios a enemy attack pq la cague con el codigo de playerhealth xd
                playerHealth.TakeDamage(enemyAttack.damage);
            }

            nextAttackTime = Time.time + attackCooldown;
        }
        /* public void AnimationChange() { animator.SetBool("isChasing", isAggro); } */
    }
}