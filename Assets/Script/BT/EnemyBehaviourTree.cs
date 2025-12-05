using UnityEngine;
using System.Collections.Generic;

public class EnemyBehaviourTree : MonoBehaviour
{
    [Header("References")]
    public Transform player;

    [Header("Ranges")]
    public float visionRange = 6f;
    public float attackRange = 2f;

    [Header("Movement")]
    public float moveSpeed = 3f;

    [Header("Patrol Settings")]
    public Transform[] patrolPoints;

    private BTNode rootNode;

    void Start()
    {
        // ==== CONDITIONS ====
        var canSee = new CanSeePlayerNode(transform, player, visionRange);
        var isClose = new IsCloseNode(transform, player, attackRange);

        // ==== ACTIONS ====
        var chase = new ChasePlayerNode(transform, player, moveSpeed);
        var attack = new AttackNode();
        var patrol = new PatrolNode(transform, moveSpeed, patrolPoints);

        // ==== BEHAVIOUR TREE ====
        //
        // Root Selector:
        // 1. If player visible & close → Attack
        // 2. If player visible & far → Chase
        // 3. Else → Patrol
        //
        rootNode = new Selector(new List<BTNode>
        {
            // Attack Sequence
            new Sequence(new List<BTNode>
            {
                canSee,
                isClose,
                attack
            }),

            // Chase Sequence
            new Sequence(new List<BTNode>
            {
                canSee,
                chase
            }),

            // Patrol fallback
            patrol
        });
    }

    void Update()
    {
        rootNode.Evaluate();
    }
}
