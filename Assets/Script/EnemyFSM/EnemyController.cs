using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 2f;
    public float chaseRange = 6f;
    public float attackRange = 2f;

    [Header("Patrol Settings")]
    public Transform[] patrolPoints;

    private EnemyState currentState;

    [HideInInspector] public EnemyIdleState idleState;
    [HideInInspector] public EnemyPatrolState patrolState;
    [HideInInspector] public EnemyChaseState chaseState;
    [HideInInspector] public EnemyAttackState attackState;

    void Start()
    {
        idleState = new EnemyIdleState(this);
        patrolState = new EnemyPatrolState(this);
        chaseState = new EnemyChaseState(this);
        attackState = new EnemyAttackState(this);

        SwitchState(idleState);
    }

    void Update()
    {
        currentState.UpdateState();
    }

    public void SwitchState(EnemyState newState)
    {
        if (currentState != null) currentState.ExitState();
        currentState = newState;
        currentState.EnterState();
    }
}
