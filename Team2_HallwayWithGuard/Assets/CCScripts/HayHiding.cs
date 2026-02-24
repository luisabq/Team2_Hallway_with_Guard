using UnityEngine;

public class HayHiding : MonoBehaviour
{
    public GameObject ufo;
    private EnemyAI enemyai;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        

    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enemyai = ufo.GetComponent<EnemyAI>();
            Debug.Log("Player entered the trigger zone!");
            EnemyAI.isFollowing = false;
        }
    }
}
