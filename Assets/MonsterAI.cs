using UnityEngine;
using UnityEngine.AI;

public class MonsterAI : MonoBehaviour
{
    public Transform player;

    [Header("거리")]
    public float detectDistance = 10f;
    public float loseDistance = 15f;

    [Header("시야각")]
    public float viewAngle = 60f;

    [Header("추격 유지 시간")]
    public float chaseMemoryTime = 3f; // 시야에서 벗어나도 추격 유지 시간

    [Header("순찰")]
    public Transform[] patrolPoints;

    private NavMeshAgent agent;
    private int currentPoint = 0;
    private bool isChasing = false;
    private Animator anim;

    private float lastSeenTimer = 0f;        // 마지막으로 본 후 경과 시간
    private Vector3 lastSeenPosition;        // 마지막으로 본 위치
    private bool isSearching = false;        // 마지막 위치 탐색 중

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        GoToNextPoint();
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        float speed = agent.velocity.magnitude;

        if (distance <= detectDistance && CanSeePlayer())
        {
            isChasing = true;
            isSearching = false;
            lastSeenTimer = 0f;
            lastSeenPosition = player.position; // 위치 계속 갱신
        }
        else if (isChasing)
        {
            lastSeenTimer += Time.deltaTime;

            if (lastSeenTimer >= chaseMemoryTime)
            {
                // 추격 유지 시간 초과 → 마지막 위치로 이동
                isChasing = false;
                isSearching = true;
                agent.SetDestination(lastSeenPosition);
            }
        }
        else if (isSearching)
        {
            // 경로 못찾거나 도착했을 때 둘 다 처리
            if (!agent.pathPending &&
                (agent.remainingDistance < 1f || agent.pathStatus == NavMeshPathStatus.PathInvalid || agent.pathStatus == NavMeshPathStatus.PathPartial))
            {
                isSearching = false;
                agent.speed = 3f;
                GoToNextPoint();
                return;
            }
        }

        if (isChasing)
        {
            ChasePlayer();
            anim.SetBool("isRunning", true);
            anim.SetBool("isWalking", false);
        }
        else
        {
            if (!isSearching) Patrol();
            anim.SetBool("isRunning", false);
            anim.SetBool("isWalking", speed > 0.1f);
        }
    }

    bool CanSeePlayer()
    {
        Vector3 direction = player.position - transform.position;
        float angle = Vector3.Angle(transform.forward, direction);
        if (angle > viewAngle) return false;

        Vector3 eyePosition = transform.position + Vector3.up * 1.5f;
        Debug.DrawRay(eyePosition, direction.normalized * detectDistance, Color.red);

        RaycastHit hit;
        if (Physics.Raycast(eyePosition, direction.normalized, out hit, detectDistance))
        {
            if (hit.transform.CompareTag("Player"))
                return true;
        }

        return false;
    }

    void ChasePlayer()
    {
        agent.speed = 6f;
        agent.SetDestination(player.position);
    }

    void Patrol()
    {
        agent.speed = 3f;
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            GoToNextPoint();
    }

    void GoToNextPoint()
    {
        if (patrolPoints.Length == 0) return;
        agent.SetDestination(patrolPoints[currentPoint].position);
        currentPoint = (currentPoint + 1) % patrolPoints.Length;
    }
}