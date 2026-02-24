using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    public float followRange = 10f;
    public float followSpeed = 2f;

    private Transform player;
    private bool isFollowing = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }


    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= followRange)
        {
            isFollowing = true;
        }

        else if (distance > followRange * 1.5f)
        {
            isFollowing = false;
        }

        if (isFollowing)
        {
            FollowPlayer();
        }

        void FollowPlayer()
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += (Vector3)direction * followSpeed * Time.deltaTime;
        }
    }
}
