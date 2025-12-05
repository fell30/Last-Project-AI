public abstract class ProbabilityNode
{
    protected ProbabilityAI ai;
    public ProbabilityNode(ProbabilityAI ai) { this.ai = ai; }

    public abstract void Execute();
}
