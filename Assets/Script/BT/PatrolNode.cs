using UnityEngine;

public class PatrolNode : BTNode
{
    private Transform enemy;
    private float speed;
    private Transform[] waypoints;

    private int currentIndex = 0;

    public PatrolNode(Transform enemy, float speed, Transform[] waypoints)
    {
        this.enemy = enemy;
        this.speed = speed;
        this.waypoints = waypoints;

        if (waypoints == null || waypoints.Length == 0)
        {
            Debug.LogError("PatrolNode ERROR: Tidak ada waypoint!");
        }
    }

    public override NodeState Evaluate()
    {
        if (waypoints == null || waypoints.Length == 0)
            return NodeState.Failure;

        Transform target = waypoints[currentIndex];

        // Bergerak menuju waypoint
        enemy.position = Vector3.MoveTowards(
            enemy.position,
            target.position,
            speed * Time.deltaTime
        );

        // Jika sudah dekat, langsung pindah ke waypoint berikutnya
        if (Vector3.Distance(enemy.position, target.position) < 0.15f)
        {
            currentIndex++;
            if (currentIndex >= waypoints.Length)
                currentIndex = 0; // loop ulang
        }

        return NodeState.Running;
    }
}
