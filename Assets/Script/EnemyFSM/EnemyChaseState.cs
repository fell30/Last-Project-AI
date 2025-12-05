using UnityEngine;

public class EnemyChaseState : EnemyState
{
    public EnemyChaseState(EnemyController enemy) : base(enemy) { }

    public override void EnterState()
    {
    }

    public override void UpdateState()
    {
        Vector3 dir = (enemy.player.position - enemy.transform.position).normalized;

        // move
        enemy.transform.position += dir * enemy.moveSpeed * 1.2f * Time.deltaTime;

        // rotate
        if (dir != Vector3.zero)
        {
            Quaternion rot = Quaternion.LookRotation(dir);
            enemy.transform.rotation =
                Quaternion.Slerp(enemy.transform.rotation, rot, 10f * Time.deltaTime);
        }

        float dist = Vector3.Distance(enemy.transform.position, enemy.player.position);

        // === 1. Jika player terlalu jauh → balik patrol ===
        if (dist > enemy.chaseRange + 4f)
        {
            enemy.SwitchState(enemy.patrolState);
            return;
        }

        // === 2. Jika player tidak kelihatan (terhalang tembok) ===
        if (!CanSeePlayer())
        {
            enemy.SwitchState(enemy.patrolState);
            return;
        }

        // === 3. Masuk attack jika dekat ===
        if (dist <= enemy.attackRange)
        {
            enemy.SwitchState(enemy.attackState);
        }
    }

    bool CanSeePlayer()
    {
        Vector3 dir = enemy.player.position - enemy.transform.position;

        if (Physics.Raycast(enemy.transform.position + Vector3.up * 0.5f, dir, out RaycastHit hit))
        {
            return hit.transform == enemy.player;
        }

        return false;
    }

    public override void ExitState() { }
}
