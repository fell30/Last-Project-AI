public abstract class EnemyState
{
    protected EnemyController enemy;

    public EnemyState(EnemyController enemy)
    {
        this.enemy = enemy;
    }

    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();
}
