using System.Collections.Generic;

public class Selector : BTNode
{
    private List<BTNode> nodes = new List<BTNode>();

    public Selector(List<BTNode> nodes)
    {
        this.nodes = nodes;
    }

    public override NodeState Evaluate()
    {
        foreach (var node in nodes)
        {
            switch (node.Evaluate())
            {
                case NodeState.Success:
                    return NodeState.Success;
                case NodeState.Running:
                    return NodeState.Running;
                default:
                    continue;
            }
        }

        return NodeState.Failure;
    }
}
