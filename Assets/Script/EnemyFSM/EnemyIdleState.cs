using UnityEngine;

public class EnemyIdleState : EnemyState
{
    private float idleTimer;

    public EnemyIdleState(EnemyController enemy) : base(enemy) { }

    public override void EnterState()
    {
        idleTimer = Random.Range(1f, 2f); // diam sebentar
    }

    public override void UpdateState()
    {
        idleTimer -= Time.deltaTime;

        if (idleTimer <= 0)
        {
            enemy.SwitchState(enemy.patrolState);
        }

        // Jika player dekat → langsung chase
        float dist = Vector3.Distance(enemy.transform.position, enemy.player.position);
        if (dist <= enemy.chaseRange)
        {
            enemy.SwitchState(enemy.chaseState);
        }
    }

    public override void ExitState() { }
}
