using Unity.VisualScripting;
using UnityEngine;

public class KeyPickup : MonoBehaviour
{


    private Inventory inventory;
    public GameObject keyText;
    public bool hasKey = false;


    // if collided with player, do AddKey method in inventory script, destroy the key and add it to inventory
    void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            inventory = other.GetComponent<Inventory>();
            inventory.AddKey();
            hasKey = true;
            Destroy(gameObject);
            keyText.SetActive(true);
            //Debug.Log(" picked up key ");
        }
    }






    void Start()
    {
        
        
        keyText.SetActive(false);


    }

    void Update()
    {
        
    }
}
