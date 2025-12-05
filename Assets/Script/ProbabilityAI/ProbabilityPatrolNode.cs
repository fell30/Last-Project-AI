using UnityEngine;

public class ProbabilityPatrolNode : ProbabilityNode
{
    private int currentIndex = 0;

    public ProbabilityPatrolNode(ProbabilityAI ai) : base(ai)
    {
        // Fix: jika waypoint kosong
        if (ai.waypoints == null || ai.waypoints.Length == 0)
        {
            Debug.LogWarning("No Waypoints Assigned!");
            return;
        }

        currentIndex = 0;
    }

    public override void Execute()
    {
        if (ai.waypoints == null || ai.waypoints.Length == 0)
            return;

        Transform target = ai.waypoints[currentIndex];

        // ====== ROTATION (Smooth, no spinning) ======
        Vector3 direction = (target.position - ai.transform.position);
        direction.y = 0; // jaga supaya tidak goyang-goyang Y axis

        if (direction.magnitude > 0.1f)
        {
            Quaternion targetRot = Quaternion.LookRotation(direction);
            ai.transform.rotation = Quaternion.Slerp(
                ai.transform.rotation,
                targetRot,
                Time.deltaTime * 4f // speed rotasi
            );
        }

        // ====== MOVE FORWARD ======
        ai.transform.position += ai.transform.forward * ai.walkSpeed * Time.deltaTime;

        // ====== OBSTACLE AVOIDANCE ======
        if (Physics.Raycast(ai.transform.position, ai.transform.forward, 0.6f, ai.obstacleMask))
        {
            // belok kanan
            ai.transform.Rotate(0, 120f * Time.deltaTime, 0);
        }

        // ====== CHANGE WAYPOINT ======
        float dist = Vector3.Distance(ai.transform.position, target.position);
        if (dist < ai.waypointReachDistance)
        {
            currentIndex++;
            if (currentIndex >= ai.waypoints.Length)
                currentIndex = 0;
        }
    }
}
