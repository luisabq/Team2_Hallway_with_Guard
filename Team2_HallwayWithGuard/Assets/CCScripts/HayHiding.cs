using UnityEngine;

public class HayHiding : MonoBehaviour
{
    public GameObject ufo;
    private EnemyAI enemyai;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FPSController controller = other.GetComponent<FPSController>();
            if (controller != null)
                controller.isHidden = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FPSController controller = other.GetComponent<FPSController>();
            if (controller != null)
                controller.isHidden = false;
        }
    }

}
