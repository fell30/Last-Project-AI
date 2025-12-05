using UnityEngine;

public class IsCloseNode : BTNode
{
    private Transform enemy;
    private Transform player;
    private float attackRange;

    public IsCloseNode(Transform enemy, Transform player, float range)
    {
        this.enemy = enemy;
        this.player = player;
        this.attackRange = range;
    }

    public override NodeState Evaluate()
    {
        return Vector3.Distance(enemy.position, player.position) <= attackRange
            ? NodeState.Success
            : NodeState.Failure;
    }
}
