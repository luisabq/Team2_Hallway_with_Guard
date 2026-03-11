using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    private Inventory inventory;
    private AudioSource audioSource;
    public GameObject keyUI;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        keyUI.SetActive(false);
    }

    // if collided with player, do AddKey method in inventory script, destroy the key and add it to inventory
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inventory = other.GetComponent<Inventory>();

            if (inventory != null)
                inventory.AddKey();
            keyUI.SetActive(true);
            TopMessageUI.Instance.ShowMessage("You picked up the key!");

            if (audioSource != null)
                audioSource.Play();

            GetComponent<Collider>().enabled = false;

            foreach (Renderer r in GetComponentsInChildren<Renderer>())
            {
                r.enabled = false;
            }

            Destroy(gameObject, audioSource != null ? audioSource.clip.length : 0.5f);
        }
    }
}