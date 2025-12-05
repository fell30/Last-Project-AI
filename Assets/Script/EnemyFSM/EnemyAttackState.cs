using UnityEngine;

public class EnemyAttackState : EnemyState
{
    private float attackCooldown = 1f;
    private float timer;

    public EnemyAttackState(EnemyController enemy) : base(enemy) { }

    public override void EnterState()
    {
        timer = attackCooldown;
    }

    public override void UpdateState()
    {
        float dist = Vector3.Distance(enemy.transform.position, enemy.player.position);

        // Jika terlalu jauh → kembali chase
        if (dist > enemy.attackRange)
        {
            enemy.SwitchState(enemy.chaseState);
            return;
        }

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Debug.Log("Enemy menyerang player!");
            timer = attackCooldown;
        }
    }

    public override void ExitState() { }
}
