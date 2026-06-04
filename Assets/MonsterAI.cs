using UnityEngine;
using UnityEngine.AI;

public class MonsterAI : MonoBehaviour
{
    public Transform player;

    [Header("거리")]
    public float detectDistance = 10f;
    public float loseDistance = 15f;

    [Header("순찰")]
    public Transform[] patrolPoints;

    private NavMeshAgent agent;
    private int currentPoint = 0;
    private bool isChasing = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        GoToNextPoint();
    }

    void Update()
    {
        float distance =
            Vector3.Distance(
                transform.position,
                player.position
            );

        // 플레이어 발견
        if (distance <= detectDistance)
        {
            isChasing = true;
        }

        // 플레이어 놓침
        if (distance >= loseDistance)
        {
            isChasing = false;
        }

        if (isChasing)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }
    }

    void ChasePlayer()
    {
        agent.speed = 6f;

        agent.SetDestination(
            player.position
        );
    }

    void Patrol()
    {
        agent.speed = 3f;

        if (!agent.pathPending &&
            agent.remainingDistance < 0.5f)
        {
            GoToNextPoint();
        }
    }

    void GoToNextPoint()
    {
        if (patrolPoints.Length == 0)
            return;

        agent.SetDestination(
            patrolPoints[currentPoint].position
        );

        currentPoint++;

        if (currentPoint >= patrolPoints.Length)
        {
            currentPoint = 0;
        }
    }
}