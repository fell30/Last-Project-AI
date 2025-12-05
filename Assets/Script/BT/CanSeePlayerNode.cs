using UnityEngine;

public class CanSeePlayerNode : BTNode
{
    private Transform enemy;
    private Transform player;
    private float visionRange;

    public CanSeePlayerNode(Transform enemy, Transform player, float range)
    {
        this.enemy = enemy;
        this.player = player;
        this.visionRange = range;
    }

    public override NodeState Evaluate()
    {
        float dist = Vector3.Distance(enemy.position, player.position);
        if (dist <= visionRange)
            return NodeState.Success;

        return NodeState.Failure;
    }
}
