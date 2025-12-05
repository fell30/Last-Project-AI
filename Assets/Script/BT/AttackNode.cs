using UnityEngine;

public class AttackNode : BTNode
{
    public override NodeState Evaluate()
    {
        // anim / serang / log
        Debug.Log("Enemy Attack!");
        return NodeState.Success;
    }
}
