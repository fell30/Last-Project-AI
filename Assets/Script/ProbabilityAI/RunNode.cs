using UnityEngine;

public class RunNode : ProbabilityNode
{
    private int dir;

    public RunNode(ProbabilityAI ai) : base(ai)
    {
        // pilih arah kiri (-1) atau kanan (1)
        dir = Random.Range(0, 2) == 0 ? -1 : 1;
    }

    public override void Execute()
    {
        // === 3D obstacle check ===
        if (Physics.Raycast(
            ai.transform.position + Vector3.up * 0.5f,   // sedikit ke atas
            ai.transform.right * dir,                   // arah AI
            out RaycastHit hit,
            1f,                                          // jarak cek
            ai.obstacleMask))                            // layer obstacle
        {
            dir *= -1; // balik arah jika menabrak
        }

        // === movement forward ===
        ai.transform.position += ai.transform.right * dir * ai.runSpeed * Time.deltaTime;

        // === rotate to direction ===
        Quaternion look = Quaternion.LookRotation(ai.transform.right * dir);
        ai.transform.rotation = Quaternion.Slerp(ai.transform.rotation, look, 10f * Time.deltaTime);
    }
}
