using Unity.VisualScripting;
using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    private Inventory inventory;
    public GameObject keyText;
    public bool hasKey = false;
    public GameObject KeyUI;

    private AudioSource audisoSource;

    void Start()
    {
        KeyUI.SetActive(false);
        keyText.SetActive(false);
        audisoSource = GetComponent<AudioSource>();
    }

    // if collided with player, do AddKey method in inventory script, destroy the key and add it to inventory
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inventory = other.GetComponent<Inventory>();
            inventory.AddKey();
            hasKey = true;
            KeyUI.SetActive(true);
            keyText.SetActive(true);
            //Debug.Log(" picked up key ");

            audisoSource.Play();

            GetComponent<Collider>().enabled = false;
            
            foreach (Renderer r in GetComponentsInChildren<Renderer>())
            {
                r.enabled = false;
            }

            Destroy(gameObject, audisoSource.clip.length);
        }
    }
}
