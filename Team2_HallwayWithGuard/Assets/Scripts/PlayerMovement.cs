using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 5f;

    private CharacterController cc;
    private Vector3 velocity;

    void Start()
    {
        cc = GetComponent<CharacterController>();
    }




    void Update()
    {
        Vector3 move = transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical");
        cc.Move(move * speed * Time.deltaTime);


        if (cc.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }


        cc.Move(velocity * Time.deltaTime);
    }
}

