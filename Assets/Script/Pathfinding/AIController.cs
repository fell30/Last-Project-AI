using UnityEngine;
using System.Collections.Generic;

public class AIController : MonoBehaviour
{
    public float speed = 5f;
    public Transform target;
    public Pathfinding pathfinding;

    private List<Node> currentPath;
    private int currentIndex = 0;

    void Update()
    {
        if (pathfinding != null && pathfinding.grid != null && pathfinding.grid.path != null)
        {
            currentPath = pathfinding.grid.path;
            MoveAlongPath();
        }
    }

    void MoveAlongPath()
    {
        if (currentPath == null || currentPath.Count == 0 || currentIndex >= currentPath.Count)
            return;

        Vector3 targetPos = currentPath[currentIndex].worldPosition;
        targetPos.y = transform.position.y; // Jaga agar AI tidak tenggelam atau terbang

        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPos) < 0.2f)
            currentIndex++;
    }
}
