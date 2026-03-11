
using UnityEngine;

public class DoorLockedMessage : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TopMessageUI.Instance.ShowMessage("The door is locked! Find the key!");
        }
    }
}