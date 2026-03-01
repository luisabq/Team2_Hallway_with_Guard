using UnityEngine;
using UnityEngine.AI;

public class UFOController : MonoBehaviour
{
    [Header("Areas")]
    public Transform area2Center;
    public Transform area3Center;

    [Header("Movement Settings")]
    public float roamRadius = 25f;
    public float patrolSpeed = 3f;
    public float followSpeed = 6f;
    public float followRange = 12f;

    [Header("Search Settings")]
    public float searchDuration = 3f;

    [Header("Teleport Settings")]
    public float zapCooldown = 15f;

    private NavMeshAgent agent;
    private Transform player;
    private FPSController playerController;

    private Transform currentAreaCenter;

    private float zapTimer = 0f;
    private float searchTimer = 0f;

    private bool isChasing = false;
    private bool searching = false;

    private Vector3 lastKnownPosition;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerController = player.GetComponent<FPSController>();

        currentAreaCenter = area2Center;

        agent.speed = patrolSpeed;

        GoToRandomPoint();
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        // team, this is when player is visible and in range
        if (!playerController.isHidden && distance <= followRange)
        {
            isChasing = true;
            searching = false;
            lastKnownPosition = player.position;
        }

        // hiding while being chased
        if (isChasing && playerController.isHidden)
        {
            isChasing = false;
            searching = true;
            searchTimer = 0f;
            lastKnownPosition = player.transform.position;
        }

        if (isChasing)
        {
            ChasePlayer();
        }
        else if (searching)
        {
            SearchLastKnownPosition();
        }
        else
        {
            Patrol();
            HandleZap();
        }
    }

    void Patrol()
    {
        agent.speed = patrolSpeed;

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GoToRandomPoint();
        }
    }

    void ChasePlayer()
    {
        agent.speed = followSpeed;

        Vector3 targetPosition = player.position;
        targetPosition.y = transform.position.y;

        agent.SetDestination(targetPosition);

        zapTimer = 0f;
    }

    void SearchLastKnownPosition()
    {
        agent.speed = patrolSpeed;
        agent.SetDestination(lastKnownPosition);

        zapTimer = 0f;

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            searchTimer += Time.deltaTime;

            if (searchTimer >= searchDuration)
            {
                searching = false;
                GoToRandomPoint();
            }
        }
    }

    void HandleZap()
    {
        zapTimer += Time.deltaTime;

        if (zapTimer >= zapCooldown)
        {
            Zap();
            zapTimer = 0f;
        }
    }

    void Zap()
    {
        if (currentAreaCenter == area2Center)
            currentAreaCenter = area3Center;
        else
            currentAreaCenter = area2Center;

        Vector3 randomDirection = Random.insideUnitSphere * roamRadius;
        randomDirection += currentAreaCenter.position;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, roamRadius, NavMesh.AllAreas))
        {
            agent.Warp(hit.position);
        }

        GoToRandomPoint();
    }

    void GoToRandomPoint()
    {
        Vector3 randomDirection = Random.insideUnitSphere * roamRadius;
        randomDirection += currentAreaCenter.position;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, roamRadius, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }
}