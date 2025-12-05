using UnityEngine;

public class EnemyPatrolState : EnemyState
{
    private int currentPoint = 0;

    public EnemyPatrolState(EnemyController enemy) : base(enemy) { }

    public override void EnterState()
    {
        if (enemy.patrolPoints.Length == 0)
        {
            Debug.LogWarning("Enemy tidak punya patrol points!");
        }
    }

    public override void UpdateState()
    {
        if (enemy.patrolPoints.Length == 0)
            return;

        Transform target = enemy.patrolPoints[currentPoint];

        // arah ke waypoint
        Vector3 dir = (target.position - enemy.transform.position).normalized;

        enemy.transform.position += dir * enemy.moveSpeed * Time.deltaTime;

        // smooth turning
        if (dir != Vector3.zero)
        {
            Quaternion lookRot = Quaternion.LookRotation(dir);
            enemy.transform.rotation =
                Quaternion.Slerp(enemy.transform.rotation, lookRot, 6f * Time.deltaTime);
        }

        // pindah waypoint (tanpa delay!)
        float distToPoint = Vector3.Distance(enemy.transform.position, target.position);
        if (distToPoint <= 0.5f)
        {
            currentPoint++;

            if (currentPoint >= enemy.patrolPoints.Length)
                currentPoint = 0; // loop
        }

        // Jika player masuk chase range
        float distToPlayer = Vector3.Distance(enemy.transform.position, enemy.player.position);
        if (distToPlayer <= enemy.chaseRange)
        {
            enemy.SwitchState(enemy.chaseState);
        }
    }

    public override void ExitState() { }
}
