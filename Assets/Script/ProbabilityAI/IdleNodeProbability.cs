using UnityEngine;

public class IdleNodeProbability : ProbabilityNode
{
    public IdleNodeProbability(ProbabilityAI ai) : base(ai) { }

    public override void Execute()
    {
        // Do nothing
    }
}
