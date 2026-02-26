using UnityEngine;
using UnityEngine.SceneManagement;

public class BarnDoor : MonoBehaviour
{

    private Inventory inventory;



    // if collidied with player, do UseKey method in inventory script
    void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {

            inventory = other.GetComponent<Inventory>();
            inventory.UseKey();

        }
    }




    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
