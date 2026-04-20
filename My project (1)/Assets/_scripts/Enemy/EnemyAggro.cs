
using UnityEngine;

public class EnemyAggro : MonoBehaviour
{
    public bool isAggro;

    public float distanceToAggro = 8f;

    [HideInInspector] public Transform playerTransform;

    public float attackCooldown = 1.5f;
    private float nextAttackTime = 0f;

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

        //se añadio un cooldown pal ataque
        if(enemyAttack.isAttacking && Time.time >= nextAttackTime)
    {
            //pequeño cambio por que doña pendeja decidio hacer el take damage de player health con float y casi se caga el codigo ya que daba problemas con el int lol 
            playerTransform.GetComponent<PlayerHealth>().TakeDamage(10f);


            //nose bro lol mentira, esto es pa que solo haga daño si ya paso el tiempo
            nextAttackTime = Time.time + attackCooldown;

            Debug.Log(playerTransform.GetComponent<PlayerHealth>().maxHealth);
        }
    }
    /*
    public void AnimationChange()
    {
        animator.SetBool("isChasing", isAggro);
    }
    */
}
