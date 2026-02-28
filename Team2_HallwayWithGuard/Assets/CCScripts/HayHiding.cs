using UnityEngine;

public class HayHiding : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FPSController controller = other.GetComponent<FPSController>();
            if (controller != null)
            {
                controller.isHidden = true;
                Debug.Log("Player hiding");
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
}
