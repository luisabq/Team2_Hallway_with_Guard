using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Beam : MonoBehaviour
{

    public GameObject loseText;

    private float collisionTimer = 0f;
    private bool isColliding = false;



    // if colliding with player
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("colliding");
            isColliding = true;
            collisionTimer = 0f;
        }
    }

    // when player leaves collidion area
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("exit collisionj");

            isColliding = false;
            collisionTimer = 0f;
        }
    }

    // timer to go to lose screen after 2 seconds of colliding with beam
    void Update()
    {
        if (isColliding)
        {
            collisionTimer += Time.deltaTime;
            if (collisionTimer >= 2f)
            {


                Lose();
                //Debug.Log("collided for 2 seconds");
            }
        }
    }



    void Lose()
    {
        SceneManager.LoadScene("loseScreen");
    }    
}