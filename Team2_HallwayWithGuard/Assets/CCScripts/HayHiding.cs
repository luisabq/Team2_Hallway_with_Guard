using UnityEngine;

public class HayHiding : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip hideSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FPSController controller = other.GetComponent<FPSController>();
            if (controller != null)
            {
                controller.isHidden = true;
                Debug.Log("Player hiding");

                PlaySound();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FPSController controller = other.GetComponent<FPSController>();
            if (controller != null)
            {
                controller.isHidden = false;
                Debug.Log("Player revealed");
            }
        }
    }

    private void PlaySound()
    {
        if (audioSource != null && hideSound != null)
        {
            audioSource.PlayOneShot(hideSound);
        }
    }
}
