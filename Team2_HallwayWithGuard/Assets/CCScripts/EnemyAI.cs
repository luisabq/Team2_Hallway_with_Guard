using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    public float followRange = 10f;
    public float followSpeed = 2f;

    private Transform player;
    [SerializeField] public bool isFollowing = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }


    void Update()
    {
        //checking if player is hidden or not
        FPSController controller = player.GetComponent<FPSController>();
        if (controller != null && controller.isHidden)
        {
            isFollowing = false;
            return; 
        }


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
            Vector3 targetPosition = player.position;
            targetPosition.y = transform.position.y;
            Vector3 direction = (targetPosition - transform.position).normalized;
            transform.position += direction * followSpeed * Time.deltaTime;
        }
    }
}
