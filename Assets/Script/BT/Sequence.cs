using System.Collections.Generic;

public class Sequence : BTNode
{
    private List<BTNode> nodes = new List<BTNode>();

    public Sequence(List<BTNode> nodes)
    {
        this.nodes = nodes;
    }

    public override NodeState Evaluate()
    {
        bool anyNodeRunning = false;

        foreach (var node in nodes)
        {
            switch (node.Evaluate())
            {
                case NodeState.Failure:
                    return NodeState.Failure;
                case NodeState.Running:
                    anyNodeRunning = true;
                    break;
            }
        }

        return anyNodeRunning ? NodeState.Running : NodeState.Success;
    }
}
