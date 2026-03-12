using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class UFOController : MonoBehaviour
{
    [Header("Patrol Zones")]
    public Transform area2Center;
    public Transform area3Center;

    [Header("Movement")]
    public float roamRadius = 25f;
    public float patrolSpeed = 3f;
    public float followSpeed = 6f;
    public float followRange = 12f;

    [Header("Search")]
    public float searchDuration = 3f;

    [Header("TP")]
    public float zapCooldown = 15f;

    [Header("Zap SFX")]
    public AudioSource zapSource;

    [Header("Chase Music")]
    public AudioSource chaseMusic;
    public float chaseVolume = 1f;
    public float fadeSpeed = 2f;

    [Header("Chase Music")]
    public AudioSource chaseVoice;
    public float chaseVolume2 = 1f;
    public float fadeSpeed2 = 2f;

    private bool chaseMusicStarted = false;

    [Header("TP Charge")]
    public float teleportChargeTime = 0.5f;

    public ParticleSystem zapParticles;

    private NavMeshAgent agent;
    private Transform player;
    private FPSController playerController;

    private Transform currentAreaCenter;

    private float zapTimer = 0f;
    private float searchTimer = 0f;

    private bool isChasing = false;
    private bool searching = false;
    private bool isTeleporting = false;

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
            if (!isChasing)
            {
                StartChaseMusic(); 
            }

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

        HandleChaseMusic();

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
        if (isTeleporting) return;

        zapTimer += Time.deltaTime;

        if (zapTimer >= zapCooldown)
        {
            StartCoroutine(TeleportSequence());
            zapTimer = 0f;
        }
    }

    IEnumerator TeleportSequence()
    {
        if (isTeleporting) yield break;

        isTeleporting = true;

        // UFO should stop
        agent.isStopped = true;
        agent.velocity = Vector3.zero;

        // play zap particles immediately
        if (zapParticles != null)
            zapParticles.Play();

        // Zap SFX plays immediately
        if (zapSource != null)
            zapSource.Play();

        // wait for the charge time before tp
        yield return new WaitForSeconds(teleportChargeTime);

        // NOW you can tp
        PerformZap();

        // Resume patrol
        agent.isStopped = false;

        isTeleporting = false;
    }

    void PerformZap()
    {
        if (currentAreaCenter == area2Center)
            currentAreaCenter = area3Center;
        else
            currentAreaCenter = area2Center;

        Vector3 randomDirection = Random.insideUnitSphere * roamRadius;
        randomDirection.y = 0f;

        Vector3 targetPosition = currentAreaCenter.position + randomDirection;

        NavMeshHit hit;

        if (NavMesh.SamplePosition(targetPosition, out hit, roamRadius, NavMesh.AllAreas))
        {
            agent.ResetPath();
            agent.Warp(hit.position);
            agent.ResetPath();
        }
        else
        {
            agent.ResetPath();
            agent.Warp(currentAreaCenter.position);
            agent.ResetPath();
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

    void StartChaseMusic()
    {
        if (chaseMusic == null) return;

        if (!chaseMusicStarted)
        {
            chaseMusic.Play();
            chaseVoice.Play();
            chaseMusicStarted = true;
        }
    }

    void HandleChaseMusic()
    {
        if (chaseMusic == null || !chaseMusicStarted) return;

        float targetVolume = isChasing ? chaseVolume : 0f;

        chaseMusic.volume = Mathf.MoveTowards(
            chaseMusic.volume,
            targetVolume,
            fadeSpeed * Time.deltaTime
        );
        chaseVoice.volume = Mathf.MoveTowards(
            chaseVoice.volume,
            targetVolume,
            fadeSpeed * Time.deltaTime
        );
    }

}