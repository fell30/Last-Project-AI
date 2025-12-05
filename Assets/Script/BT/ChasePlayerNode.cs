using UnityEngine;

public class ChasePlayerNode : BTNode
{
    private Transform enemy;
    private Transform player;
    private float speed;

    public ChasePlayerNode(Transform enemy, Transform player, float speed)
    {
        this.enemy = enemy;
        this.player = player;
        this.speed = speed;
    }

    public override NodeState Evaluate()
    {
        Vector3 dir = (player.position - enemy.position).normalized;
        enemy.position += dir * speed * Time.deltaTime;
        return NodeState.Running;
    }
}
