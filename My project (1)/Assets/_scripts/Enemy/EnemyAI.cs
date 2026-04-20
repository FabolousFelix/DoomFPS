
using UnityEngine;
using UnityEngine.AI;


public class EnemyAI : MonoBehaviour
{
    // Puntos por donde el enemigo debe pasar para patrullar
    public Transform[] points;

    // Índice del siguiente punto de destino en el array 'points'
    private int destPoint = 0;

    // Referencia al script que controla cuando el enemigo está en persiguiendo al jugador
    private EnemyAggro enemyAggro;

    // Transform del jugador para que el enemigo pueda moverse hacia él cuando esté en aggro
    private Transform playerTransform;

    // Componente NavMeshAgent que controla el movimiento sobre el NavMesh
    private NavMeshAgent navMeshAgent;

    private void Start()
    {
        // Obtener el componente EnemyAggro en el mismo GameObject
        enemyAggro = GetComponent<EnemyAggro>();

        // Si no se asignó el transform del jugador en inspector, buscar una instancia de PlayerMovement en la escena
        if (playerTransform == null )
        playerTransform = FindAnyObjectByType<PlayerMovement>().transform;

        // Obtener el NavMeshAgent que debe existir en el mismo GameObject para la navegación
        navMeshAgent = GetComponent<NavMeshAgent>();
        // Iniciar movimiento hacia el primer punto de patrulla
        NextPoint();
    }

    private void NextPoint()
    {
        // Si no hay puntos configurados, no hacer nada
        if (points == null || points.Length == 0)
            return;

        // Establece la posición de destino del NavMeshAgent al punto actual
        navMeshAgent.destination = points[destPoint].position;

        // Incrementa el índice de destino y usa el operador módulo para ciclar por los puntos (vuelve al inicio)
        destPoint = (destPoint + 1) % points.Length;
    }

    private void Update()
    {
        //se llama en update para que funcione el metodo
       EnemyMovement();
    }

    private void EnemyMovement()
    {
        // Si el EnemyAggro indica que el enemigo está en modo agresivo, perseguir al jugador
        if (enemyAggro.isAggro)
        {
            // Indicar al agente que se dirija a la posición del jugador
            navMeshAgent.SetDestination(playerTransform.position);
            // Aumentar la velocidad para la persecución
            navMeshAgent.speed = 6f;
            // Mantener una distancia de parada para no solaparse con el jugador
            navMeshAgent.stoppingDistance = 3;
        }
        else
        {
            // Comportamiento de patrulla: ninguna distancia de parada y velocidad más reducida
            navMeshAgent.stoppingDistance = 0;
            navMeshAgent.speed = 2f;

            // Si el agente no está calculando una ruta y está muy cerca del destino actual,pasar al siguiente punto de patrulla.
            // - pathPending: true si el agente aún está calculando la ruta
            // - remainingDistance: distancia restante hasta el destino actual
            if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f)
            {
                NextPoint();
            }
               
        }
    }

}
