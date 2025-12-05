using UnityEngine;
using UnityEngine.AI;

public class NPCPathfinding : MonoBehaviour
{
    public Transform target;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(target.position);
    }

    void Update()
    {
        // Update target terus jika bergerak
        agent.SetDestination(target.position);
    }
}
