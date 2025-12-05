using UnityEngine;

public class ProbabilityAI : MonoBehaviour
{
    [Header("Probability Settings")]
    [Range(0, 100)] public int runChance = 30;
    [Range(0, 100)] public int patrolChance = 50;
    [Range(0, 100)] public int idleChance = 20;

    [Header("Movement Settings")]
    public float walkSpeed = 2f;
    public float runSpeed = 4f;
    public float actionDuration = 2f;

    [Header("Waypoint Patrol")]
    public Transform[] waypoints;
    public float waypointReachDistance = 0.3f;

    [Header("Obstacle Avoidance")]
    public LayerMask obstacleMask;

    private ProbabilityNode currentNode;
    private float timer;

    private void Start()
    {
        ChooseAction();
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (currentNode != null)
            currentNode.Execute();

        if (timer <= 0)
            ChooseAction();
    }

    public void ChooseAction()
    {
        timer = actionDuration;

        int roll = Random.Range(0, 100);

        if (roll < runChance)
            currentNode = new RunNode(this);
        else if (roll < runChance + patrolChance)
            currentNode = new ProbabilityPatrolNode(this); // <-- gunakan kelas baru
        else
            currentNode = new IdleNodeProbability(this);

        Debug.Log("Selected Action = " + currentNode.GetType().Name);
    }

    // Expose some fields for nodes
    public float walkSpeedPublic => walkSpeed;
}
