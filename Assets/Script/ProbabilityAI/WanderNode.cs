using UnityEngine;

public class WanderNode : ProbabilityNode
{
    private int dir;

    public WanderNode(ProbabilityAI ai) : base(ai)
    {
        dir = Random.Range(0, 2) == 0 ? -1 : 1;
    }

    public override void Execute()
    {
        // obstacle check (3D raycast)
        if (Physics.Raycast(ai.transform.position + Vector3.up * 0.5f,
                            ai.transform.right * dir,
                            out RaycastHit hit,
                            1f,
                            ai.obstacleMask))
        {
            dir *= -1; // change direction
        }

        // movement (3D)
        ai.transform.position += ai.transform.right * dir * ai.walkSpeed * Time.deltaTime;

        // rotate towards direction
        Quaternion look = Quaternion.LookRotation(ai.transform.right * dir);
        ai.transform.rotation = Quaternion.Slerp(ai.transform.rotation, look, 5f * Time.deltaTime);
    }
}
