using UnityEngine;

public class SimpleAI : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float changeDirectionTime = 2f;
    public float avoidDistance = 1f;
    public float turnSpeed = 5f;

    private Vector3 direction;
    private float timer;

    void Start()
    {
        PickNewDirection();
    }

    void Update()
    {
        // Raycast cek tembok berdasarkan ARAH movement (direction)
        if (Physics.Raycast(
            transform.position + Vector3.up * 0.5f,
            direction,
            avoidDistance
        ))
        {
            PickNewDirection();
        }

        // Smooth Rotate capsule
        if (direction != Vector3.zero)
        {
            Quaternion targetRot = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * turnSpeed);
        }

        // Gerak maju
        transform.position += direction * moveSpeed * Time.deltaTime;

        // Timer ganti arah
        timer -= Time.deltaTime;
        if (timer <= 0)
            PickNewDirection();
    }

    void PickNewDirection()
    {
        direction = new Vector3(
            Random.Range(-1f, 1f),
            0,
            Random.Range(-1f, 1f)
        ).normalized;

        timer = changeDirectionTime;
    }
}
